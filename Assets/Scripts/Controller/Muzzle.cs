using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    private GetOtherObjectTrigger getOtherObjectTrigger;
    private Bullet bullet;

    public Bullet Bullet
    {
        get => bullet;
        set => bullet = value;
    }

    private void Start()
    {
        if (getOtherObjectTrigger == null)
            getOtherObjectTrigger = gameObject.GetComponent<GetOtherObjectTrigger>();
    }

    private void Update()
    {
        GetCurrentBulletInRevolverCylinder();
    }

    private void GetCurrentBulletInRevolverCylinder()
    {
        if(getOtherObjectTrigger.OtherObj)
            if (getOtherObjectTrigger.OtherObj.GetComponent<Bullet>())
                bullet = getOtherObjectTrigger.OtherObj.GetComponent<Bullet>();        
    }
    
}
