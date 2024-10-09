using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door")) 
        {
            gameManager.ShowWinPanel(); 
        }
    }
}
