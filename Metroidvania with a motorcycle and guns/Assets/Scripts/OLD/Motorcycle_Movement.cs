using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle_Movement : MonoBehaviour
{
    [SerializeField]
    private float verticalInput;

    public Motorcycle_Turn_Script directionChecker;

    private int facingRight;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rearWheel;
    [SerializeField]
    private Rigidbody2D frontWheel;



    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        facingRight = directionChecker.facingRight ? -1 : 1;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (verticalInput > 0)
        {
            rearWheel.angularVelocity = moveSpeed * facingRight * Time.fixedDeltaTime;

            //rearWheel.AddTorque(moveSpeed * verticalInput * (facingRight ? -1 : 1) * Time.fixedDeltaTime);
        }
        else if (verticalInput < 0)
        {
            rearWheel.angularVelocity = 0f;
            frontWheel.angularVelocity = 0f;
        }
    }
}
