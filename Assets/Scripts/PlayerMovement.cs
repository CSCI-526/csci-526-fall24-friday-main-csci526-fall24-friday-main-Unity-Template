using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ⁠ https://www.youtube.com/watch?v=mldjoVDhKc4 ⁠ Reference
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public SceneRotation sceneRotation;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForceLandscape = 6.0f; 
    [SerializeField] private float fallSpeedLandscape = 0.09f; 
    [SerializeField] private float jetpackForce = 3.0f; 
    [SerializeField] private float normalFallSpeed = 0.05f; 
    private Rigidbody2D rb;
    private bool useJet;
    public CameraMovement cameraMovement;
    private Health health;
    private int healthDamage = 1;
    private Vector3 playerOriginalPosition;
    public GameObject cam;
    public Vector3 CameraOriginalPosition;
    public GameObject scene;
    public Vector3 sceneOriginalPosition; 

    private bool isGameStarted = false;

    private HashSet<GameObject> damagedSpikes = new HashSet<GameObject>();

    // private bool isTouchWall = false;
    private float stayOnWallTime = 0.0f;

    private float stayOnSpikeTime = 0.0f;

    private bool isWallDamage = false;

    private float damageCoolDown = 0.5f;
    private bool isGameStated = false;
    public Image ProgressBarImg;
    private float ProgressBarWidth;
    private RectTransform rt;
    private int currentLevel = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        isGameStarted = false;
        ProgressBarImg = GameObject.Find("Progress").GetComponent<Image>();
        // ProgressBarPosition = GameObject.Find("Progress").transform.position;
        rt = ProgressBarImg.GetComponent<RectTransform>();

        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(0, 0.5f);

        // Set the new width (keeping the height the same)
        ProgressBarWidth = rt.rect.width;
        rt.sizeDelta = new Vector2(1, rt.sizeDelta.y);
    }

    void Update()
    {
        if(isGameStarted){
            float moveLR = Input.GetAxis("Horizontal"); // Left/Righ Movement
            // if space bar is down, speed change to jetForce; 
            // otherwise fall normally
            rb.velocity = new Vector2(moveLR * speed, Input.GetKey(KeyCode.Space) ? jetForce : rb.velocity.y - normalFallSpeed);
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision) // If collided with spike
    {
        if (collision.gameObject.CompareTag("Spike") && !sceneRotation.isRotating) //check if it is spike
        {
            if (!damagedSpikes.Contains(collision.gameObject))
            {
                damagedSpikes.Add(collision.gameObject);

                playerOriginalPosition = gameObject.transform.position;

                cam = GameObject.Find("Main Camera");
                CameraOriginalPosition = cam.transform.position;

                scene = GameObject.Find("GameView");
                sceneOriginalPosition = scene.transform.position;
                Die();
            }
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            stayOnSpikeTime += Time.deltaTime;
            if (stayOnSpikeTime >= damageCoolDown)
            {
                Die();
                stayOnSpikeTime = 0.0f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            damagedSpikes.Remove(collision.gameObject);
            stayOnSpikeTime = 0.0f;
        }
    }

    public void StartGame(){
        isGameStarted = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndPhase1") // If it is winTrigger
        {
            print("pass phase 0");
            rt.sizeDelta = new Vector2(ProgressBarWidth / 3, rt.sizeDelta.y);
        }
        if (collision.gameObject.name == "EndPhase2") // If it is winTrigger
        {
            print("pass phase 1");
            rt.sizeDelta = new Vector2(2 * ProgressBarWidth / 3, rt.sizeDelta.y);
        }
        if (collision.gameObject.CompareTag("WinTrigger")) // If it is winTrigger
        {
            rt.sizeDelta = new Vector2(ProgressBarWidth, rt.sizeDelta.y);
            Win();
            cameraMovement.StopCamera();
            sceneRotation.StopRotation();
        }
        if (collision.gameObject.CompareTag("LevelTrigger")) // If it is winTrigger
        {
            Debug.Log("Leveled up");
            Destroy(collision.gameObject);
            currentLevel++;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            stayOnWallTime += Time.deltaTime;

            if (stayOnWallTime < damageCoolDown && !isWallDamage)
            {
                Die();
                isWallDamage = true;
            }
            else if (stayOnWallTime >= damageCoolDown)
            {
                stayOnWallTime = 0.0f;
                isWallDamage = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // isTouchWall = false;
            stayOnWallTime = 0.0f;
        }
    }

    void Die()
    {
        Debug.Log("Lost one heart");
        health.TakeDamage(healthDamage);
        if (health.currentHealth <= 0) 
        {
            AnalyticsManager.trackProgress(currentLevel, false);
            Debug.Log("You died at level " + currentLevel);
        }
    }

    void Win()
    {
        FindObjectOfType<EventControl>().ShowWinPanel();  // Notify EventControl
        Debug.Log("Winner Winner Chicken Dinner!"); //print win
        AnalyticsManager.trackProgress(currentLevel, true);
    }
}
