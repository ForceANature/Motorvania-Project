using UnityEngine;

public class RampLogicWheels : MonoBehaviour
{
    // References to the Collider2D
    public Collider2D rampCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!rampCollider.isTrigger && collision.gameObject.CompareTag("Ground"))
        {
            rampCollider.isTrigger = true;
            Debug.Log("The ramp is now a ghost!");
        }
    }

}
