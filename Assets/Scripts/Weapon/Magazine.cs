using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;
public class Magazine : MonoBehaviour
{
    private GameObject _magzine;
    private bool setMagazine;
    private Grabble _Grabble;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GetOtherObjectInTrigger getOther;

    private void Start()
    {
        if (getOther == null)
            getOther = GetComponent<GetOtherObjectInTrigger>();
        if(getOther == null)
            throw  new Exception("You need add GetOtherObjectInTrigger in this GameObject if you want use MagazineScript");
    }

    private void Update()
    {
        if(!weapon.GetComponent<Grabble>().grabbed)
                return;


        if (getOther.GetObject() != null)
            if (getOther.GetObject().gameObject.GetComponent<Grabble>().grabbed)
            {
                if (getOther.GetObject().GetComponent<Ammo>())
                {
                    _magzine = getOther.GetObject();
                    _Grabble = _magzine.gameObject.GetComponent<Grabble>();
                }
            }

        if (getOther.objectExit)
        {
            _magzine = null;
            _Grabble = null;
            return;
        }
            
        if (getOther.objectEnter )
        {
            if(_magzine && _Grabble)
            if(PicoGrabble.Instance._getOtherObjectLeftController.GetObject() == _magzine.gameObject)
                if ((Input.GetKeyDown(KeyCode.Q) || Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)))
                {
                    if (!_Grabble.grabbed)
                    {
                        Debug.Log("0");
                        setMagazine = true;
                    }
                }
            
            if(_magzine && _Grabble)
                if(PicoGrabble.Instance._getOtherObjectRightController.GetObject() == _magzine.gameObject)
                if ((Input.GetKeyDown(KeyCode.E) || Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER)))
                {
                    if (!_Grabble.grabbed)
                    {
                        Debug.Log("1");
                        setMagazine = true;
                    }
                }
        }
        
        if (setMagazine)
        {
            _magzine.GetComponent<Grabble>().Grab(this.gameObject,0.1f,1f);
        }
    }
}
