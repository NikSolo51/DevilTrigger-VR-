using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour , IGrabble
{
    private PicoGrabble _PicoGrabble;
    private IBullet _BulletType;
    private ICylinder _CylinderType;
    private ITrigger _TriggerType;
    private void Awake()
    {
        _PicoGrabble = GetComponent<PicoGrabble>();
    }

    private void Update()
    {
        GrabbedUpdate();
    }

    void GrabbedUpdate()
    {
        if(_PicoGrabble.inHand == true)
        Grab();
    }
    public void Grab()
    {
        if(_PicoGrabble.firstController != null)
            transform.SetPositionAndRotation(_PicoGrabble.firstController.transform.position, _PicoGrabble.firstController.transform.rotation);
    }
}
