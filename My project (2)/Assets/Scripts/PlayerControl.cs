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
    Vector3 mousePos;
    public Transform Firepoint;
    public Transform ReferencePoint;

    //Bullet
    public GameObject bulletPrefab;
    public int bulletCount = 10;
    public float bulletSpeed;
    public float spread;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("recoil");
            GoDirection();
            Shoot();
        }

        //Mouse pos initializing
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - ReferencePoint.position;
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
        //Launchs amount of i bullets with spreading
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Firepoint.position, Firepoint.rotation);
            Rigidbody2D BulletRB = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = Firepoint.transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
            BulletRB.velocity = (dir + pdir) * bulletSpeed;
            Destroy(bullet, 1f);
        }
    }

}
