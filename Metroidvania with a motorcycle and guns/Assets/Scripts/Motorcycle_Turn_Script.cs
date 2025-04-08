using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Motorcycle_Turn_Script : MonoBehaviour
{
    

    public KeyCode key = KeyCode.Space;// Key to press
    public float holdTime = 0.5f;// Time needed to be held

    [SerializeField]
    private float holdTimer = 0f;// Timer

    private bool hasTriggered = false;// Saveguard for a single activation
    public bool facingRight = true;// Motorcycle direction

    [SerializeField]
    private GameObject frame;
    [SerializeField]
    public GameObject swingArm;
    [SerializeField]
    public GameObject rearWheel;

    [SerializeField]
    private WheelJoint2D frontWheelJoint;


    void Update()
    {
        if (Input.GetKey(key))
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTime && !hasTriggered)
            {
                Motorcycle_Turn();
                hasTriggered = true;
            }
        }
        else
        {
            holdTimer = 0f;
            hasTriggered = false;
        }
    }

    void Motorcycle_Turn()
    {
        frame.transform.localScale = new Vector3(facingRight ? -1 : 1, 1, 1);

        JointSuspension2D suspension = frontWheelJoint.suspension;
        suspension.angle = 120f * (facingRight ? -1 : 1);
        frontWheelJoint.suspension = suspension;

        frame.GetComponent<Joints_Handler>().UpdateJoints();

        swingArm.GetComponent<Joints_Handler>().UpdateJoints();

        rearWheel.GetComponent<Joints_Handler>().UpdateJoints();

        facingRight = !facingRight;
    }
}