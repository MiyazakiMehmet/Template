using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D rb;
    public float dashSpeed;
    Vector3 mousePos;
    public Transform Firepoint;
    public Transform ReferencePoint;
    public GameObject particle;

    //Bullet
    public int bulletCount = 10;
    public float bulletSpeed;
    public float spread;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GoDirection();
            Shoot();
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) -ReferencePoint.position;
    }   

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void GoDirection()
    {
        rb.AddForce(-Firepoint.right * dashSpeed, ForceMode2D.Impulse);    }

    void Shoot()
    {
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(particle, Firepoint.position, Firepoint.rotation);
            Rigidbody2D BulletRB = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = Firepoint.transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
            BulletRB.velocity = (dir + pdir) * bulletSpeed;
            Destroy(bullet, 1f);
        }
    }

}
