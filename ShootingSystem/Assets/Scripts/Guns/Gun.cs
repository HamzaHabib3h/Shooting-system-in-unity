using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Gun : EventInvoker
{
    #region BulletInitialization Fields

    protected string BulletSuffix = "Ammo";

    [SerializeField]
    protected GameObject GunEndPoint;

    #endregion

    #region Cams And HUD


    protected Camera cam;

    protected LayerMask target = 8;

    [SerializeField]
    Animator anim;

    public static bool isScoped;

    [SerializeField]
    protected GameObject scopeImage;

    [SerializeField]
    Image crossHair;

    [SerializeField]
    protected GameObject weaponCam;

    public static float normalZoom;

    float scopedZoom = 10f;

    #endregion

    #region InheritedFields

    protected bool coolDown = true;
    protected Timer coolDownTimer;
    [SerializeField]
    protected float range;
    protected AmmoType ammoType;
    protected float power;
    protected bool scopeAble = false;
    protected float coolDownSeconds;
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        cam = Camera.main;
        events.Add(EventNames.GunFiredEvent, new GunFiredEvent());
        EventManager.AddInvoker(EventNames.GunFiredEvent, this);
        EventManager.AddListener(EventNames.GunFiredEvent, Fire);
        EventManager.AddListener(EventNames.WeaponSwitchedEvent,Unscoped);
        anim = GetComponentInParent<Animator>();
        coolDownTimer = GetComponent<Timer>();
        coolDownTimer.Duration = coolDownSeconds;
    }



    // Update is called once per frame
    protected virtual void Update()
    {
        #region AimColor

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range) && hit.transform.GetComponent<Rigidbody>() != null)
        {
            crossHair.color = Color.red;
        }
        else
            crossHair.color = Color.white;

        #endregion

        #region FireCode

        if (Input.GetAxis("Fire1") != 0)
        {
            events[EventNames.GunFiredEvent].Invoke(ammoType, power);
        }

        #endregion

        #region Scope

        if (Input.GetButtonDown("Fire2") && scopeAble == true)
        {
            isScoped = !isScoped;
            anim.SetBool("Scoped", isScoped);
            if (isScoped)
            {
                StartCoroutine("Scope");
            }
            else
                Unscoped(AmmoType.Sniper,0);
        }

        #endregion
    }



    #region Fire
    protected abstract void Fire(AmmoType ammoType, float power);

    #endregion



    #region SnipingMethods

    IEnumerator Scope()
    {
        yield return new WaitForSeconds(.25f);
        scopeImage.SetActive(true);
        weaponCam.SetActive(false);
        normalZoom = cam.fieldOfView;
        cam.fieldOfView = scopedZoom;
        
    } 

    void Unscoped(AmmoType unused, float nouse)
    {
        anim.SetBool("Scoped", false);
        scopeImage.SetActive(false);
        weaponCam.SetActive(true);
        cam.fieldOfView = normalZoom;
    }
    #endregion


    /// <summary>
    /// To Cast a ray from camera to shoot target
    /// </summary>
    protected virtual void CastRay()
    {
        coolDown = false;
        coolDownTimer.Run();
        GetComponent<AudioSource>().Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            GetComponent<AudioSource>().Play();
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-hit.normal * power, ForceMode.Impulse);
            }
        }
        GameObject.Instantiate((GameObject)Resources
            .Load("Bullets/" + ammoType.ToString() + BulletSuffix), GunEndPoint.transform.position,
            GunEndPoint.transform.rotation);
    }
}
