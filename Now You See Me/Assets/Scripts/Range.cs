using UnityEngine;

public class RangeVisualizer : MonoBehaviour
{
    public float range1 = 15f;
    public float range2 = 10f;
    public float range3 = 5f;

    void OnDrawGizmos()
    {
        // Set color for range1 and draw sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range1);

        // Set color for range2 and draw sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range2);

        // Set color for range3 and draw sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range3);
    }
}
