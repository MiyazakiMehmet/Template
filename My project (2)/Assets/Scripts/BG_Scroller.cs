using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    [Range(-1.2f, 1.2f)] public float shiftSpeedX, shiftSpeedY;
    private float offsetX, offsetY;
    private Material mat;

    //Restricts
    public Transform maxPosY;
    public Transform minPosY;
    public Transform maxPosX;

    private float cooldownY;
    private float cooldownX;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        shiftSpeedX = 0.2f;
        cooldownY = 0f;
        cooldownX = 0f;
    }

    // Update is called once per frame  
    void Update()
    {
        offsetX += (Time.deltaTime * shiftSpeedX) / 10f;
        offsetY += (Time.deltaTime * shiftSpeedY) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));

        //Y Axis
        if(playerControl.transform.position.y >= maxPosY.position.y)
        {
            cooldownY = 0f;
            shiftSpeedY = 1.2f;
        }
        else if(playerControl.transform.position.y <= minPosY.position.y)
        {
            cooldownY = 0f;
            shiftSpeedY = -1.2f;
        }
        else
        {
            //Slowly decreases the amount of shifting speed
            if(shiftSpeedY != 0)
            {
                cooldownY += Time.deltaTime / 2f;
                float shiftCooldown = Mathf.Lerp((shiftSpeedY > 0f ? 1.2f : -1.2f) , 0f, cooldownY);;
                shiftSpeedY = shiftCooldown;
            }
            else
            {
                shiftSpeedY = 0f;
            }
        }

        //X Axis
        if (playerControl.transform.position.x >= maxPosX.position.x)
        {
            cooldownX = 0f;
            shiftSpeedX = 1.2f;
        }
        else
        {
            //Slowly decreases the amount of shifting speed
            if (shiftSpeedX != 0.2f)
            {
                cooldownX += Time.deltaTime / 2f;
                float shiftCooldown = Mathf.Lerp(1.2f, 0.2f, cooldownX);
                shiftSpeedX = shiftCooldown;
            }
            else
            {
                shiftSpeedX = 0.2f;
            }
        }
    }
}
