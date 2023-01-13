using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider HealthSlider;
    public Color High;
    public Color Low;

    void Start()
    {
        HealthSlider = GetComponent<Slider>();
    }

    //It sets the healthbar values
    public void SetCurrentHealth(int currentHealth, int maxHealth)
    {
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = currentHealth;
        //It finds the fillRect image from the children class
        HealthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, HealthSlider.normalizedValue);
    }
}
