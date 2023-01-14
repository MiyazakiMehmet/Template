using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private int selectedWeaponIndex = 0;

    [SerializeField] private AmmoBarScript ammoBarScript;

    void Start()
    {
        SwitchWeapon();    
    }

    void Update()
    {
        int previousWeaponIndex = selectedWeaponIndex;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedWeaponIndex = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedWeaponIndex = 1;
        }

        if(previousWeaponIndex != selectedWeaponIndex)
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(selectedWeaponIndex == i)
            {
                weapon.gameObject.SetActive(true);
                ammoBarScript.CheckCurrentWeapon(weapon);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
