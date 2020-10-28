using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Gun
{

    #region Fields

    bool fireOnPreviousFrame = false;

    static new string name = "SniperRifle";

    #endregion

    #region Proporties

    public static string Name
    {
        get { return name; }
    }

    #endregion

    protected override void Start()
    { 
        coolDownSeconds = 1.5f;
        ammoType = AmmoType.Sniper;
        power = 100;
        range = 100f;
        scopeAble = true;
        base.Start();
        coolDownTimer = cam.GetComponent<Timer>();

    }


    protected override void Update()
    {
        base.Update();

        if (Input.GetAxis("Fire1") == 0)
        {
            fireOnPreviousFrame = false;
        }
    }

    protected override void Fire(AmmoType ammoType,float power)
    {


        if (fireOnPreviousFrame == false && coolDown)
        {
            fireOnPreviousFrame = true ;
            CastRay();
        }

        

        if (coolDownTimer.Finished)
        {
            coolDown = true;
        }

    }
}
