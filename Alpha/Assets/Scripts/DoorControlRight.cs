using UnityEngine;

public class DoorControllerRight : MonoBehaviour
{
    public Animator doorAnimator1; 
    public Transform player1;      
    public float interactionDistance1 = 3f; 

    void Update()
    {
        float distance = Vector3.Distance(player1.position, transform.position);

        if (distance < interactionDistance1)  
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
               
                if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("DoorCloseRight"))
                {
                    doorAnimator1.SetTrigger("Open");  
                }
                else if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenRight"))
                {
                    doorAnimator1.SetTrigger("Close"); 
                }
            }
        }
    }
}
