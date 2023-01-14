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

    public void CheckCurrentWeapon()
    {       
        foreach(Transform weapon in WeaponHolder.transform)
        {
            if(weapon.tag == "Shotgun")
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
