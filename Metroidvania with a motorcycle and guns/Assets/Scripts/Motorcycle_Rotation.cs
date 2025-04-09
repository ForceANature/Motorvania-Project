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
    private Gun flipReloader;

    [SerializeField] 
    float tolerance = 5f;

    private bool hasPassedZero = false;
    public float zRotation;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = groundChecker.IsGrounded;

        zRotation = NormalizeAngle(frame.transform.eulerAngles.z);

        // Check for 0° cross
        if (!hasPassedZero && IsWithinTolerance(zRotation, 0f, tolerance))
        {
            hasPassedZero = true;
            Debug.Log("Passed 0°!");
        }

        // Check for 180° cross
        if (hasPassedZero && IsWithinTolerance(zRotation, 180f, tolerance))
        {

            hasPassedZero = false;
            flipReloader.LoadMagazine();
            Debug.Log("Passed 180°!");
        }
    }

    bool IsWithinTolerance(float angle, float target, float tolerance)
    {
        return Mathf.Abs(Mathf.DeltaAngle(angle, target)) <= tolerance;
    }

    float NormalizeAngle(float angle)
    {
        // Normalize to 0–360°
        return (angle + 360f) % 360f;
    }

    private void FixedUpdate()
    {
        HandleRotation();
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
