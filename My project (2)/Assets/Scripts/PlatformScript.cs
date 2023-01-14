using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] private Transform PlatformTarget; 
    public float platformSpeed;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlatformTarget.position,
            platformSpeed * Time.deltaTime);
    }
}
