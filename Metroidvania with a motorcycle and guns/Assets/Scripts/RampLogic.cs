using UnityEngine;

public class RampLogic : MonoBehaviour
{
    [SerializeField]
    private Collider2D rearWheelCollider;
    [SerializeField]
    private Collider2D frontWheelCollider;
    [SerializeField]
    private Collider2D groundCollider;
    private Collider2D rampCollider;

    [SerializeField]
    private Rigidbody2D frame;
    private PolygonCollider2D frameCollider;

    public int rampDirection;

    public float horizobtalInput;

    private void Start()
    {
        frameCollider = frame.GetComponent<PolygonCollider2D>();
        rampCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        horizobtalInput = Input.GetAxisRaw("Horizontal");

        if (!rampCollider.isTrigger && !frontWheelCollider.IsTouching(rampCollider) && rearWheelCollider.IsTouching(groundCollider))
        {
            rampCollider.isTrigger = true;
            Debug.Log("The ramp is now a ghost!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wheels") && !frameCollider.IsTouching(rampCollider) && rampCollider.isTrigger/*GHOST*/ && frame.transform.localScale.x == rampDirection && horizobtalInput == -rampDirection)
        {
            rampCollider.isTrigger = false;
            Debug.Log("The ramp is now solid!");
        }

    }

}


