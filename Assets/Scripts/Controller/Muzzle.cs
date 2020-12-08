using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    private GetOtherObjectTrigger _otherObject;
    private Bullet bullet;

    public Bullet Bullet
    {
        get => bullet;
        set => bullet = value;
    }

    private void Start()
    {
        if (_otherObject == null)
            _otherObject = gameObject.GetComponent<GetOtherObjectTrigger>();
    }

    private void Update()
    {
        GetCurrentBulletInRevolverCylinder();
    }

    private void GetCurrentBulletInRevolverCylinder()
    {
        if(_otherObject.ReturnOtherObject())
            if (_otherObject.ReturnOtherObject().GetComponent<Bullet>())
                bullet = _otherObject.ReturnOtherObject().GetComponent<Bullet>();        
    }
    
}
