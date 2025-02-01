using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    //References
    //public GameObject motorcycle;
    public Rigidbody2D frame;

    //Input
    public float verticalInput;
    public float horizontalInput;

    // References to the wheels' Joints
    public HingeJoint2D rearWheelHingeJoint;
    public JointMotor2D rearWheelMotor;
    public float motorSpeed;

    //Changing Directions
    public int motorcycleDirection = 1;
    private float spaceHoldTime = 0f;
    public float spaceHoldDuration;
    private bool motorcycleTurnTrigger = false;

    // Rotation
    public float rotationTorque;

    private void Start()
    {
        // Frame = GetComponent<Rigidbody2D>();
        rearWheelMotor = rearWheelHingeJoint.motor;

    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (verticalInput != 0) rearWheelHingeJoint.useMotor = true;
        else rearWheelHingeJoint.useMotor = false;

        // Holding space key for a time before allowing to turn
        if (Input.GetKey(KeyCode.Space))
        {
            if (!motorcycleTurnTrigger) // Only track time if the action hasn't been triggered
            {
                spaceHoldTime += Time.deltaTime; // Increment hold time

                // Check if hold time exceeds the duration
                if (spaceHoldTime >= spaceHoldDuration)
                {
                    Turn();
                    motorcycleTurnTrigger = true; // Mark the action as triggered
                }
            }
        }
        else
        {
            // Reset when the key is released
            spaceHoldTime = 0f;
            motorcycleTurnTrigger = false;
        }
        
    }

    private void FixedUpdate()
    {
        HandleAcceleration();

        HandleRotation();
    }

    void HandleAcceleration()
    {
        if (verticalInput != 0)
        {
            if(verticalInput > 0) rearWheelMotor.motorSpeed = motorSpeed * verticalInput * motorcycleDirection;
            else rearWheelMotor.motorSpeed = motorSpeed * 0;

            rearWheelHingeJoint.motor = rearWheelMotor;
        }

    }

    void HandleRotation()
    {
        if (horizontalInput != 0)
        {
            frame.AddTorque(-horizontalInput * rotationTorque * Time.deltaTime, ForceMode2D.Force);
        }
    }

    void Turn()
    {
        motorcycleDirection = motorcycleDirection == 1 ? -1 : 1;

        frame.transform.localScale = new Vector3(motorcycleDirection, 1, 1);
    }

}
