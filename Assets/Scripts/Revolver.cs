using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    [Header("Prefab Refrences")] [HideInInspector]
    public GameObject bulletPrefab;

    [HideInInspector] public GameObject casingPrefab;

    [Header("Location Refrences")] 
    [SerializeField] private CylinderScript Cylinder;
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
    private Bullet bullet;
    [SerializeField]private bool cylinderOpen = false;
    

    void Start()
    {
        if (revolverGrabble == null)
            revolverGrabble = GetComponent<Grabble>();
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
        
        Cylinder.OpenCylinder(false);
    }

    void Update()
    {
        if (!revolverGrabble)
            return;
        if (!revolverGrabble.Grabbed)
            return;

        if (cylinderOpen)
        {
            
            if ((revolverGrabble.InLeftHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.B)) ||
                Input.GetKeyDown(KeyCode.R))
            {
                
                gunAnimator.SetBool("OpenCylinder", false);
                cylinderOpen = false;
                Cylinder.OpenCylinder(false);
                return;
            }

            if ((revolverGrabble.InRightHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.B)) ||
                Input.GetKeyDown(KeyCode.R))
            {
                
                gunAnimator.SetBool("OpenCylinder", false);
                cylinderOpen = false;
                Cylinder.OpenCylinder(false);
                return;
            }
        }

        if ((revolverGrabble.InLeftHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.B)) ||
            Input.GetKeyDown(KeyCode.R))
        {
            
            gunAnimator.SetBool("OpenCylinder", true);
            cylinderOpen = true;
            Cylinder.OpenCylinder(true);
            return;
        }

        if ((revolverGrabble.InRightHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.B)) ||
            Input.GetKeyDown(KeyCode.R))
        {
            
            gunAnimator.SetBool("OpenCylinder", true);
            cylinderOpen = true;
            Cylinder.OpenCylinder(true);
            return;
        }
        
        if(cylinderOpen)
            return;
        
        if (Cylinder.bulletForShot)
        {
            if (Cylinder.bulletForShot.GetComponent<Bullet>())
                bullet = Cylinder.bulletForShot.GetComponent<Bullet>();
            else 
                bullet = null;
        }
        else
        {
            bullet = null;
            Cylinder.bulletForShot = null;
        }
        
            
        
        if((revolverGrabble.InLeftHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)) || Input.GetButtonDown("Fire1"))
        {
            gunAnimator.SetTrigger("Shot1");
            RotateCyl();
            
            if(Cylinder.bulletForShot)
            DestroyBulletInCylinderRevolver();
        }

        if ((revolverGrabble.InRightHand && Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER)) ||
            Input.GetButtonDown("Fire1"))
        {
            gunAnimator.SetTrigger("Shot1");
            RotateCyl();
            
            if (Cylinder.bulletForShot)
                DestroyBulletInCylinderRevolver();
        }
    }

    void DestroyBulletInCylinderRevolver()
    {
        if (bullet)
            Destroy(bullet.gameObject);
    }

    void Shoot()
    {
        if (!bulletPrefab)
            return;
        if (!bullet)
            return;

        // Create a bullet and add force on it in direction of the barrel
        GameObject _bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        _bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        Destroy(_bullet, destroyTimer);
    }

    void RotateCyl() =>  Cylinder.RotateCyl();

    public void PlayShoot()
    {
        if (!bulletPrefab)
            return;
        if (!bullet)
            return;
        
        audio.PlayOneShot(shoot) ;
    } 
    
}