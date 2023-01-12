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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GoDirection();
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }   

    void FixedUpdate()
    {
        Vector3 targetDirection = mousePos - Firepoint.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void GoDirection()
    {
        rb.AddForce(-Firepoint.up * dashSpeed, ForceMode2D.Impulse);
    }

}
