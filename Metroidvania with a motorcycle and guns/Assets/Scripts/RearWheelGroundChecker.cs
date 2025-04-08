using UnityEngine;

public class RearWheelGroundChecker : MonoBehaviour
{
    public bool IsGrounded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            //Debug.Log("OnCollisionEnter2D");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            //Debug.Log("OnCollisionStay2D");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
            //Debug.Log("OnCollisionExit2D");
        }
    }
}