using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    #region Fields

    static new string  name = "Pistol";

    bool fireOnPreviousFrame;

    #endregion

    #region Proporties

    public static string Name
    {
        get { return name; }
    }

    #endregion

    // Start is called before the first frame update
    protected override void Start()
    {
        fireOnPreviousFrame = false;
        coolDownSeconds = 0.5f;
        power = 30;
        range = 30f;
        ammoType = AmmoType.Pistol;
        coolDownTimer = GetComponent<Timer>();
        base.Start();
    }


    protected override void Update()
    {
        if (Input.GetAxis("Fire1") == 0)
        {
            fireOnPreviousFrame = false;
        }
        base.Update();
    }

    protected override void Fire(AmmoType ammoType, float power)
    {
        {
            if (fireOnPreviousFrame == false && coolDown)
            {
                fireOnPreviousFrame = true;
                CastRay();
            }

            if (coolDownTimer.Finished)
            {
                coolDown = true;
            }
        }
    }
}
