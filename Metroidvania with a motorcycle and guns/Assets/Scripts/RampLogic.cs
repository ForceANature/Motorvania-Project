using UnityEngine;

public class RampLogic : MonoBehaviour
{
    // References to the Collider2D
    //private Collider2D rampCollider;
    //[SerializeField]
    //private Collider2D groundCollider;
    //[SerializeField]
    //private Collider2D rearWheelCollider;
    //[SerializeField]
    //private Collider2D frontWheelCollider;
    [SerializeField]
    private Collider2D rearWheelCollider;
    [SerializeField]
    private Collider2D frontWheelCollider;
    [SerializeField]
    private Collider2D groundCollider;
    private Collider2D rampCollider;

    // References to the Rigidbody2D
    [SerializeField]
    private Rigidbody2D frame;

    //Ramp Direction
    public int rampDirection;

    private void Start()
    {
        // Get the Collider2D attached to the GameObject
        rampCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!rampCollider.isTrigger && !frontWheelCollider.IsTouching(rampCollider) && rearWheelCollider.IsTouching(groundCollider))
        {
            rampCollider.isTrigger = true;
            Debug.Log("The ramp is now a ghost!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wheels") && rampCollider.isTrigger/*GHOST*/ && frame.transform.localScale.x == rampDirection && Input.GetAxisRaw("Horizontal") == -rampDirection)
        {
            rampCollider.isTrigger = false;
            Debug.Log("The ramp is now solid!");
        }

        //if (other.CompareTag("Wheels") && rampCollider.isTrigger && frame.transform.localScale.x == rampDirection) Debug.Log("Passing by!       " + -rampDirection);
    }

}


