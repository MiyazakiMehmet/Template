using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    public GameObject pistolBulletPrefab;
    public int bulletCountShotgun = 10;
    public float bulletSpeedShotgun;
    public float bulletSpeedPistol;
    public float spread;


    //Ammo
    [SerializeField] private AmmoBarScript ammoBarScript;
    public int maxAmmoShotgun;
    public int currentAmmoShotgun;

    public int maxAmmoPistol;
    public int currentAmmoPistol;
    private bool isReloading = false;

    //Platform
    [SerializeField] private Transform GateBorder;
    [SerializeField] private Transform ObstacleRight;
    [SerializeField] private CinemachineVirtualCamera vcam;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        //Ammo
        currentAmmoShotgun = maxAmmoShotgun;
        ammoBarScript.SetCurrentAmmo(currentAmmoShotgun, maxAmmoShotgun);
        currentAmmoPistol = maxAmmoPistol;
    }

    void Update()
    {
        IEnumerator co = Reload();

        //Mouse pos initializing
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (isReloading)
        {
            //if weapon has changed while reloading it will stop reloading
            if (currentAmmoShotgun <= 0 && !ammoBarScript.isShotgun)
            {
                isReloading = false;
            }
            else if (currentAmmoPistol <= 0 && !ammoBarScript.isPistol)
            {
                isReloading = false;
            }
            return;
        }

        //If there is enough ammo
        if (currentAmmoShotgun > 0 && ammoBarScript.isShotgun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("recoil");
                GoDirection();
                Shoot();
            }
        }

        else if(currentAmmoPistol > 0 && ammoBarScript.isPistol)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        //Reload
        else
        {
            StartCoroutine(co);
            return;
        }

        //Gate
        Gate();
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
            for (int i = 0; i < bulletCountShotgun; i++)
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
            GameObject pistolBullet = Instantiate(pistolBulletPrefab, FirepointPistol.position, FirepointPistol.rotation);
            Rigidbody2D pistolBulletRB = pistolBullet.GetComponent<Rigidbody2D>();
            pistolBulletRB.AddForce(FirepointPistol.right * bulletSpeedPistol, ForceMode2D.Impulse);
            Destroy(pistolBullet, 2f);
        }

        //Ammo (This is seperate from others because we want to reduce ammo just 1 on the up there will be redu-
        //ced more than 1 (This applies to Shotgun)
        if (ammoBarScript.isShotgun && currentAmmoShotgun > 0)
        {
            currentAmmoShotgun--;
            ammoBarScript.SetCurrentAmmo(currentAmmoShotgun, maxAmmoShotgun);
        }
        else if(ammoBarScript.isPistol && currentAmmoPistol > 0)
        {
            currentAmmoPistol--;
        }
    }
    IEnumerator Reload()
    {
        if (currentAmmoShotgun <= 0 && ammoBarScript.isShotgun)
        {
            isReloading = true;
            yield return new WaitForSeconds(2.0f);
            //if weapon has changed before reloading it will cancel
            if (isReloading)
            {
                currentAmmoShotgun = maxAmmoShotgun;
                ammoBarScript.SetCurrentAmmo(currentAmmoShotgun, maxAmmoShotgun);
                isReloading = false;
            }
        }
        if (currentAmmoPistol <= 0 && ammoBarScript.isPistol)
        {
            isReloading = true;
            yield return new WaitForSeconds(1.6f);
            //if weapon has changed before reloading it will cancel
            if (isReloading)
            {
                currentAmmoPistol = maxAmmoPistol;
                isReloading = false;
            }
        }
    }

    void Gate()
    {
        if(transform.position.x >= GateBorder.transform.position.x)
        {
            ObstacleRight.gameObject.SetActive(false);
            vcam.gameObject.SetActive(true);
        }
    }
}
