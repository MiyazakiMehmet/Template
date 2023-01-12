using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    PlayerControl playerControl;
    private Transform player;
    public Transform Firepoint;


    public float movementSpeed;
    public float maximumDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = PlayerControl.Instance;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= maximumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        }
    }

    void WeaponRotation()
    {
        Vector3 targetDirection = playerControl.transform.position - Firepoint.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    }
}
