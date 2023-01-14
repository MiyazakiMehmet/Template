using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarScript : MonoBehaviour
{
    public Slider AmmoSlider;


    void Update()
    {
        AmmoSlider = GetComponent<Slider>();
    }

    public void SetCurrentAmmo(int currentAmmo, int maxAmmo)
    {
        AmmoSlider.maxValue = maxAmmo;
        AmmoSlider.value = currentAmmo;
    }
}
