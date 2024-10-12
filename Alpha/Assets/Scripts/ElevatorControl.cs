using UnityEngine;

public class ElevatorControl : MonoBehaviour
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

                if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("ElevatorDoorOpen"))
                {
                    doorAnimator1.SetTrigger("Open");
                }
                else if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("ElevatorDoorClose"))
                {
                    doorAnimator1.SetTrigger("Close");
                }
            }
        }
    }
}
