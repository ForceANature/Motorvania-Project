using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy after 2 seconds
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Destroy(gameObject); // Destroy on impact
    }
}
