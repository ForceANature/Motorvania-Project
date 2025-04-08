using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotorcycleController : MonoBehaviour
{
    [SerializeField] private GameObject frame;
    [SerializeField] private Rigidbody2D frameRB;
    [SerializeField] private Rigidbody2D rearWheel;
    [SerializeField] private Rigidbody2D frontWheel;
    [SerializeField] private float wheelSpeed;
    [SerializeField] private float groundRotationTorque;
    [SerializeField] private float airbourneRotationTorque;
    [SerializeField] private float rotationStopSpeed;
    public float horizontalInput;
    public float verticalInput;
    [SerializeField] private float spaceHoldTime;
    [SerializeField] private float spaceHoldDuration;

    [Tooltip("Other Scripts")]
    public RearWheelGroundChecker groundChecker;
    public Motorcycle_Turn_Script directionChecker;

    public bool isGrounded;
    public bool facingRight;


    void Update()
    {
        isGrounded = groundChecker.IsGrounded;
        facingRight = directionChecker.facingRight;
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        if (horizontalInput != 0 && isGrounded)
        {
            frameRB.AddTorque(-horizontalInput * groundRotationTorque * Time.deltaTime, ForceMode2D.Force);
            
        }
        else if (horizontalInput != 0 && !isGrounded)
        {
            frameRB.AddTorque(-horizontalInput * airbourneRotationTorque * Time.deltaTime, ForceMode2D.Force);
        }
        else if (horizontalInput == 0 && !isGrounded)
        {
            frameRB.angularVelocity = Mathf.Lerp(frameRB.angularVelocity, 0f, Time.deltaTime * rotationStopSpeed);
        }
    }
}
