using UnityEngine;

public class Motorcycle_Movement_2 : MonoBehaviour
{
    public float verticalInput;

    public Motorcycle_Turn_Script directionChecker;

    public int facingRight;

    public WheelJoint2D frontWheel;
    public HingeJoint2D rearWheel;
    public float motorSpeed = 0f;
    public float motorTorque = 0f;

    private JointMotor2D frontMotor;
    private JointMotor2D rearMotor;
    void Start()
    {
        frontMotor = new JointMotor2D { maxMotorTorque = motorTorque };
        rearMotor = new JointMotor2D { maxMotorTorque = motorTorque };
    }

    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        facingRight = directionChecker.facingRight ? -1 : 1;
    }

    void FixedUpdate()
    {
        HandleMovement();

    }

    void HandleMovement()
    {
        if (verticalInput > 0)
        {
            frontMotor.motorSpeed = -motorSpeed * facingRight;
            rearMotor.motorSpeed = motorSpeed * facingRight;
        }
        else if (verticalInput < 0) {
            frontMotor.motorSpeed = 0;
            rearMotor.motorSpeed = 0;
        }

        if (verticalInput != 0)
        {
            frontWheel.useMotor = true;
            rearWheel.useMotor = true;
            frontWheel.motor = frontMotor;
            rearWheel.motor = rearMotor;
        }
        else
        {
            frontWheel.useMotor = false;
            rearWheel.useMotor = false;
        }
    }
}
