using UnityEngine;

public class GhostAudioController : MonoBehaviour
{
    public float range1 = 30f;
    public float range2 = 25f;
    public float range3 = 20f;
    public float range4 = 15f;  // Radius for first range
    public float range5 = 10f; // Radius for second range
    public float range6 = 5f; // Radius for third range

    private AudioSource audioSourceRange1;
    private AudioSource audioSourceRange2;
    private AudioSource audioSourceRange3;
    private AudioSource audioSourceRange4;
    private AudioSource audioSourceRange5;
    private AudioSource audioSourceRange6;

    private GameObject targetGhost;

    void Start()
    {
        // Get all AudioSource components on this GameObject
        AudioSource[] audioSources = GetComponents<AudioSource>();
        
        if (audioSources.Length >= 6)
        {
            audioSourceRange1 = audioSources[0];
            audioSourceRange2 = audioSources[1];
            audioSourceRange3 = audioSources[2];
            audioSourceRange4 = audioSources[3];
            audioSourceRange5 = audioSources[4];
            audioSourceRange6 = audioSources[5];

        }
        else
        {
            Debug.LogError("Not enough AudioSources attached to this GameObject. You need at least 3.");
        }
        StopAllAudios();
    }

    void Update()
    {
        // Find the closest "Ghost" object
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        targetGhost = FindClosestGhost(ghosts);

        if (targetGhost != null)
        {
            float distance = Vector3.Distance(transform.position, targetGhost.transform.position);
            PlayAudioForRange(distance);
        }
        else
        {
            StopAllAudios();
        }
    }

    GameObject FindClosestGhost(GameObject[] ghosts)
    {
        GameObject closestGhost = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject ghost in ghosts)
        {
            float distance = Vector3.Distance(transform.position, ghost.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestGhost = ghost;
            }
        }

        return closestGhost;
    }

    void PlayAudioForRange(float distance)
    {
        // Determine which range the ghost is in and play the corresponding audio
        if (distance <= range1 && distance > range2)
        {
            if (!audioSourceRange1.isPlaying)
            {
                StopAllAudios();
                Debug.Log("Playing audio for range 1");
                audioSourceRange1.Play();
            }
        }
        else if (distance <= range2 && distance > range3)
        {
            if (!audioSourceRange2.isPlaying)
            {
                StopAllAudios();
                Debug.Log("Playing audio for range 2");
                audioSourceRange2.Play();
            }
        }
        else if (distance <= range3 && distance > range4)
        {
            if (!audioSourceRange3.isPlaying)
            {
                StopAllAudios();
                Debug.Log("Playing audio for range 3");
                audioSourceRange3.Play();
            }
        }
        else if (distance <= range4 && distance > range5)
        {
            if (!audioSourceRange4.isPlaying)
            {
                StopAllAudios();
                Debug.Log("Playing audio for range 4");
                audioSourceRange4.Play();
            }
        }
        else if (distance <= range5 && distance > range6)
        {
            if (!audioSourceRange5.isPlaying)
            {
                StopAllAudios();
                Debug.Log("Playing audio for range 5");
                audioSourceRange5.Play();
            }
        }
        else if (distance <= range6)
        {
            if (!audioSourceRange6.isPlaying)
            {
                StopAllAudios();
                Debug.Log("Playing audio for range 6");
                audioSourceRange6.Play();
            }
        }
        else
        {
            // Stop all audios if the ghost is out of all ranges
            StopAllAudios();
            Debug.Log("No ghosts in range");
        }
    }

    void StopAllAudios()
    {
        // Stop all audio sources
        if (audioSourceRange1.isPlaying) audioSourceRange1.Stop();
        if (audioSourceRange2.isPlaying) audioSourceRange2.Stop();
        if (audioSourceRange3.isPlaying) audioSourceRange3.Stop();
        if (audioSourceRange4.isPlaying) audioSourceRange4.Stop();
        if (audioSourceRange5.isPlaying) audioSourceRange5.Stop();
        if (audioSourceRange6.isPlaying) audioSourceRange6.Stop();

    }
}