using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //Instance
    PlayerControl playerControl;

    //Transforms
    private Transform player;
    public Transform Aim;
    public Transform Firepoint;

    //Bullet Attributes
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float bulletCounter = 0;
    public float bulletCooldown;

    //Enemy Movement Attributees
    public float movementSpeed;
    public float maximumDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = PlayerControl.Instance;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //Player Follow
        if(Vector2.Distance(transform.position, player.transform.position) <= maximumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        }

        //Shooting
        Shoot();
    }

    void FixedUpdate()
    {
        WeaponRotation();
    }

    void WeaponRotation()
    {
        Vector3 targetDirection = playerControl.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Aim.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        if (Time.time > bulletCounter + bulletCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab, Firepoint.position, Firepoint.rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(Firepoint.right * bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, 2f);
            bulletCounter = Time.time;
        }
    }
}
