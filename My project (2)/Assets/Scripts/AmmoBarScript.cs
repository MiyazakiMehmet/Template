using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarScript : MonoBehaviour
{
    public Slider AmmoSlider;

    [SerializeField] private WeaponSwitch WeaponHolder;
 
    void Update()
    {
        AmmoSlider = GetComponent<Slider>();

    }

    public void SetCurrentAmmo(int currentAmmo, int maxAmmo)
    {
        AmmoSlider.maxValue = maxAmmo;
        AmmoSlider.value = currentAmmo;
    }

    public void CheckCurrentWeapon(Transform weapon)
    {       
        if(weapon.tag == "Shotgun")
        {
            gameObject.SetActive(true);      
        }
        else
        {
            gameObject.SetActive(false); 
        }
    }   
}

