using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }
    public SessionData sessionData { get; private set; }
    public PlayerMovement playerController;
    public List<Checkpoint> allCheckpoints = new List<Checkpoint>();
    private float lastCheckpointTime;
    public bool isDataPosted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sessionData = new SessionData();
        Debug.Log("Session ID: " + sessionData.sessionID);

        SceneManager.sceneLoaded += OnSceneLoaded;
        lastCheckpointTime = Time.time;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignPlayerController();
    }

    private void AssignPlayerController()
    {
        playerController = FindObjectOfType<PlayerMovement>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
    }

    public void AddCheckpoint(string checkpointID)
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned.");
            return;
        }

        float currentTime = Time.time;
        float timeTaken = currentTime - lastCheckpointTime;
        lastCheckpointTime = currentTime;

        sessionData.AddCheckpoint(checkpointID, playerController.hitCount, timeTaken);
    }

    public void PostSessionDataToFireBase()
    {
        if (isDataPosted) return;
        sessionData.PostToFireBase();
        isDataPosted = true;
    }

    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        if (!allCheckpoints.Contains(checkpoint))
        {
            allCheckpoints.Add(checkpoint);
        }
    }

    public Checkpoint GetClosestUpcomingCheckpoint(Vector3 playerPosition)
    {
        Checkpoint closestCheckpoint = null;
        float closestDistance = float.MaxValue;

        if (sessionData == null)
        {
            Debug.LogError("SessionData is null.");
            return null;
        }

        if (sessionData.crossedCheckpoints == null)
        {
            Debug.LogWarning("crossedCheckpoints is null. Initializing it.");
            sessionData.crossedCheckpoints = new List<CheckpointEntry>();
        }

        foreach (var checkpoint in allCheckpoints)
        {
            if (checkpoint == null)
            {
                Debug.LogWarning("Encountered a null checkpoint in allCheckpoints list.");
                continue;
            }

            if (!sessionData.crossedCheckpoints.Exists(c => c.checkpointID == checkpoint.checkpointID))
            {
                float distance = Vector3.Distance(playerPosition, checkpoint.transform.position);
                bool isToTheRight = checkpoint.transform.position.x > playerPosition.x;
                if (FlipManager.IsFlipped)
                {
                    isToTheRight = checkpoint.transform.position.x < playerPosition.x;
                }

                if (isToTheRight && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCheckpoint = checkpoint;
                }
            }
        }

        if (closestCheckpoint == null)
        {
            Debug.LogWarning("No upcoming checkpoint found.");
        }

        return closestCheckpoint;
    }
}