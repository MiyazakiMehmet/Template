using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    [Range(-1f, 1f)] public float shiftSpeedX, shiftSpeedY;
    private float offsetX, offsetY;
    private Material mat;

    //Restricts
    public Transform maxPos;
    public Transform minPos;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame  
    void Update()
    {
        offsetX += (Time.deltaTime * shiftSpeedX) / 10f;
        offsetY += (Time.deltaTime * shiftSpeedY) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
        
        if(playerControl.transform.position.y >= maxPos.position.y)
        {
            shiftSpeedY = 1.2f;
        }
        else if(playerControl.transform.position.y <= minPos.position.y)
        {
            shiftSpeedY = -1.2f;
        }
        else
        {
            shiftSpeedY = 0f;
        }
    }
}
