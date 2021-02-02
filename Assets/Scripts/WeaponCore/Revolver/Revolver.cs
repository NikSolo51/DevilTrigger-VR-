using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    [Header("ScriptLinks")] 
    public BulletForShot bulletForShot;

    public RevolverEventSender revolverEventSender;
    
    private Bullet bullet;

    [HideInInspector] public GameObject casingPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip shoot;

    [Header("Settings")] [Tooltip("Specify time to destory the casing object")] [SerializeField]
    private float destroyTimer = 2f;

    [Tooltip("Bullet Speed")] [SerializeField]
    private float shotPower = 500f;

    [Tooltip("Casing Ejection Speed")] [SerializeField]
    private float ejectPower = 150f;
    
    private Grabble revolverGrabble;
    
    private bool cylinderOpen = false;

    [Header("TimeToReload")] public float reloadTime = 3.0f;
    private float timeToReload;
    private void Awake()
    {
        bulletForShot.OnGetBulletForShot += UpdateBulletForShot;
    }

    private void OnDestroy()
    {
        bulletForShot.OnGetBulletForShot -= UpdateBulletForShot;
    }

    private void UpdateBulletForShot(Bullet bullet)
    {
        this.bullet = bullet;
    }
    
    void Start()
    {
        if (revolverGrabble == null)
            revolverGrabble = GetComponent<Grabble>();
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        timeToReload -= Time.deltaTime;
        
        if (!revolverGrabble)
            return;
        if (!revolverGrabble.Grabbed)
            return;

        if (cylinderOpen)
        {
            if (TryingCloseRevolverCylinder())
            {
                CloseRevolverCylinder();
                return;
            }
        }
        
        if (TryingOpenRevolverCylinder())
            OpenRevolverCylinder();
        
        if(cylinderOpen)
            return;
        
        if(TryingToShot())
            Shot();
    }

    private bool TryingToShot()
    {
        if ((revolverGrabble.InLeftHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)) ||
            Input.GetButtonDown("Fire1"))
        {
            return true;
        }

        if ((revolverGrabble.InRightHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER)) ||
            Input.GetButtonDown("Fire1"))
        {
            return true;
        }

        return false;
    }
    

    private bool TryingCloseRevolverCylinder()
    {
        if ((revolverGrabble.InLeftHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.B)) ||
            Input.GetKeyDown(KeyCode.R))
        {

            return true;
        }

        if ((revolverGrabble.InRightHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.B)) ||
            Input.GetKeyDown(KeyCode.R))
        {
            
            return true;
        }

        return false;
    }

    public void CloseRevolverCylinder()
    {
        gunAnimator.SetBool("OpenCylinder", false);
        cylinderOpen = false;
        
    }

    private bool TryingOpenRevolverCylinder()
    {
        if ((revolverGrabble.InLeftHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.B)) ||
            Input.GetKeyDown(KeyCode.R))
        {
            
            return true;
        }

        if ((revolverGrabble.InRightHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.B)) ||
            Input.GetKeyDown(KeyCode.R))
        {
            
            return true;
        }

        return false;
    }

    private void OpenRevolverCylinder()
    {
        Debug.Log("OpenRevolverCylinder");
        gunAnimator.SetBool("OpenCylinder", true);
        cylinderOpen = true;
    }

    void DestroyBulletInCylinderRevolver()
    {
        
        if (bullet)
            Destroy(bullet.gameObject);
    }

    void Shot()
    {
        if(timeToReload > 0 )
            return;

        if (!bullet)
        {
            revolverEventSender.UpdateBulletQueueEvent();
            revolverEventSender.UpdateRotateCylinderEvent();
            return;
        }
        
        if (!bulletForShot)
            return;
        
        gunAnimator.SetTrigger("Shot1");
        
    
        // Create a bullet and add force on it in direction of the barrel
        GameObject _bullet = Instantiate(bullet.BulletPrefab, barrelLocation.position, barrelLocation.rotation);
        _bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        
        Destroy(_bullet, destroyTimer);
        DestroyBulletInCylinderRevolver();

        timeToReload = reloadTime;
    }
    
    public void PlayShot()
    {
        audio.PlayOneShot(shoot) ;
    }

}