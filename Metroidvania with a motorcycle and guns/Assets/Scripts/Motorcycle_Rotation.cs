using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Motorcycle_Rotation : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D frame;

    public float rotationSpeed;

    [SerializeField]
    private float horizontalInput;

    public RearWheelGroundChecker groundChecker;

    [SerializeField]
    private bool isGrounded;

    public float stoppingForce;

    [SerializeField]
    public float firstAngleThreshold = 90f;
    private float previousAngle;
    public float currentAngle;

    private float prevToThreshold;
    private float currToThreshold;

    void Start()
    {
        previousAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = groundChecker.IsGrounded;

        currentAngle = frame.transform.eulerAngles.z;

        // Convert angles to [-180, 180] range
        prevToThreshold = Mathf.DeltaAngle(previousAngle, firstAngleThreshold);
        currToThreshold = Mathf.DeltaAngle(currentAngle, firstAngleThreshold);

        // Check if signs are different => crossed the threshold
        if (Mathf.Sign(prevToThreshold) != Mathf.Sign(currToThreshold))
        {
            Debug.Log("Crossed threshold angle (any direction)");
        }

        previousAngle = currentAngle;
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    float GetSignedAngleDifference(float from, float to)
    {
        return Mathf.DeltaAngle(from, to);
    }

    void HandleRotation()
    {
        if (horizontalInput != 0)
        {
            frame.AddTorque(-horizontalInput * rotationSpeed * Time.deltaTime, ForceMode2D.Force);

        }
        else if (horizontalInput == 0 && !isGrounded)
        {
            frame.angularVelocity = Mathf.Lerp(frame.angularVelocity, 0f, Time.deltaTime * stoppingForce);
        }


    }


}
