using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform arm;

    public Sprite[] pointerSprites;

    public SpriteRenderer spriteRenderer;

    public float angle;

    public int spriteIndex;

    public Motorcycle_Turn_Script directionChecker;

    private int facingRight;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {

        facingRight = directionChecker.facingRight ? 1 : -1;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - arm.position;

        angle = Mathf.Atan2(direction.y, direction.x * facingRight) * Mathf.Rad2Deg;

        UpdateSprite(angle);

        arm.rotation = Quaternion.Euler(0f, 0f, angle * facingRight);

    }

    private void UpdateSprite(float angle)
    {
        angle = (angle + 382) % 360;

        spriteIndex = Mathf.FloorToInt(angle / 45f);
        spriteIndex = Mathf.Clamp(spriteIndex, 0, pointerSprites.Length - 1);

        spriteRenderer.sprite = pointerSprites[spriteIndex];

    }
}