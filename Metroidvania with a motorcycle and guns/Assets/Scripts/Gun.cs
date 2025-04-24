using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    public int magazineSize = 4;
    private int magazine;

    [SerializeField] private float slowTimeScale = 0.2f;
    [SerializeField] private float slowTimeDuration = 3f;
    [SerializeField] private float normalTimeScale = 1f;

    private bool isAiming = false;
    private float slowTimeTimer = 0f;

    public Motorcycle_Turn_Script directionChecker;

    private int facingRight;


    private void Start()
    {
        LoadMagazine();
    }
    
    public void LoadMagazine()
    {
        magazine = magazineSize;
        Debug.Log("Gun is Loaded!");
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !isAiming && magazine > 0)
        {
            EnterSlowTime();
        }

        if (isAiming)
        {
            slowTimeTimer -= Time.unscaledDeltaTime;

            if (Input.GetButtonUp("Fire1") || slowTimeTimer <= 0f)
            {
                ExitSlowTimeAndFire();
            }
        }
    }

    void EnterSlowTime()
    {
        isAiming = true;
        slowTimeTimer = slowTimeDuration;

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void ExitSlowTimeAndFire()
    {
        isAiming = false;
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f;

        Shoot();
    }

    void Shoot()
    {
        facingRight = directionChecker.facingRight ? 1 : -1;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletForce * facingRight * firePoint.right, ForceMode2D.Impulse);
        magazine--;

    }
}
