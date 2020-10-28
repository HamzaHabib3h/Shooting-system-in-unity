
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : EventInvoker
{

    #region Fields
    [SerializeField]
    Text gunName;

    [SerializeField]
    GameObject[] guns = new GameObject[3];

    int activatedWeapon = 0;

    float switchWeapon = 0;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        events.Add(EventNames.WeaponSwitchedEvent, new WeaponSwitchedEvent());
        EventManager.AddInvoker(EventNames.WeaponSwitchedEvent, this);
        guns[0].SetActive(true);
        gunName.text = "Activated Gun : " + guns[activatedWeapon].name;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switchWeapon = -1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
            switchWeapon = 1;
        else
            switchWeapon = 0;

        if (switchWeapon != 0)
        {
            if (SniperRifle.isScoped)
            { 
                events[EventNames.WeaponSwitchedEvent].Invoke(AmmoType.Sniper, 34);
            }
            SwitchWeapon(switchWeapon);
            
        }
    }


    void SwitchWeapon(float axis)
    {
            if (activatedWeapon == guns.Length - 1)
            {
                activatedWeapon = 0;
            }
            else if (activatedWeapon == 0 && axis < 0)
            {
                activatedWeapon = guns.Length - 1;
            }
            else
            {
                activatedWeapon += (int)axis;
            }


            foreach (GameObject gun in guns)
            {
                gun.SetActive(false);
            }
            guns[(int)activatedWeapon].SetActive(true);
            
            gunName.text = guns[activatedWeapon].name;
    }
}
