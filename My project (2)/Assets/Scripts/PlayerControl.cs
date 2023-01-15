using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    void Awake()
    {
        Instance = this;
    }

    public Camera cam;

    //Rigidbody of Character
    public Rigidbody2D rb;
    public Animator animator;
    public float dashSpeed;
    public float maxDashSpeed;

    //Positions
    public Vector3 mousePos;
    public Transform Firepoint;
    public Transform FirepointPistol;

    //Bullet
    public GameObject bulletPrefab;
    public int bulletCount = 10;
    public float bulletSpeedShotgun;
    public float bulletSpeedPistol;
    public float spread;

    //Ammo
    [SerializeField] private AmmoBarScript ammoBarScript;
    public int maxAmmo;
    public int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        //Ammo
        currentAmmo = maxAmmo;
        ammoBarScript.SetCurrentAmmo(currentAmmo, maxAmmo);
    }

    void Update()
    {
        //Mouse pos initializing
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (isReloading)
        {
            return;
        }

        //If there is enough ammo
        if (currentAmmo > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("recoil");
                GoDirection();
                Shoot();
            }
        }

        //Reload
        else
        {
            StartCoroutine(Reload());
            return;
        }
    }   

    void FixedUpdate()
    {
        //Aim follows the cursor
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void GoDirection()
    {
        //Character goes the opposite direction of where clicked
        
        rb.AddForce(-Firepoint.right * dashSpeed, ForceMode2D.Impulse);

        //If player's speed goes over the max speed it keeps speed at the max speed
        if (rb.velocity.magnitude > maxDashSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxDashSpeed;
        }
    }

    void Shoot()
    {
        if (ammoBarScript.isShotgun)
        {
            //Launchs amount of i bullets with spreading
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, Firepoint.position, Firepoint.rotation);
                Rigidbody2D BulletRB = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = Firepoint.transform.rotation * Vector2.right;
                Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
                BulletRB.velocity = (dir + pdir) * bulletSpeedShotgun;
                Destroy(bullet, 2f);
            }
        }
        else if (ammoBarScript.isPistol)
        {
            GameObject pistolBullet = Instantiate(bulletPrefab, FirepointPistol.position, FirepointPistol.rotation);
            Rigidbody2D pistolBulletRB = pistolBullet.GetComponent<Rigidbody2D>();
            pistolBulletRB.AddForce(FirepointPistol.right * bulletSpeedPistol, ForceMode2D.Impulse);
            Destroy(pistolBullet, 2f);
        }

        //Ammo (This is seperate from others because we want to reduce ammo just 1 on the up there will be redu-
        //ced more than 1 (This applies to Shotgun)
        if (currentAmmo > 0)
        {
            currentAmmo--;
            ammoBarScript.SetCurrentAmmo(currentAmmo, maxAmmo);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2.0f);
        currentAmmo = maxAmmo;
        ammoBarScript.SetCurrentAmmo(currentAmmo, maxAmmo);
        isReloading = false;
    }
}
