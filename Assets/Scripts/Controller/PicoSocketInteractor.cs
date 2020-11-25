using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoSocketInteractor : MonoBehaviour
{
    [SerializeField]private bool emptySocket = true;
    [SerializeField]GameObject _otherObj;
    Grabble _grabble;
    
    public GameObject OtherObj
    {
        get => _otherObj;
        set => _otherObj = value;
    }
    
    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<Grabble>())
                if (other.gameObject.GetComponent<Grabble>().Interactable)
                {
                    if (!emptySocket)
                        return;
                    _otherObj = other.gameObject;
                    emptySocket = false;
                }
        }
    
    private void OnTriggerExit(Collider other)
    {
        _otherObj = null;
        emptySocket = true; 
    }
    

    private void Update()
    {
        if (_grabble)
        {
            if ((Input.GetKeyDown(KeyCode.Q) ||
                 Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)) )
            {
                // _grabble.Grabbed = true;
                _otherObj = null;
                _grabble = null;
                emptySocket = true;
                return;
            }

            //if(_otherObj.GetComponent<Grabble>() == PicoGrabbleManager.Instance.rightHand)
            if ((Input.GetKeyDown(KeyCode.E) ||
                 Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER)) )
            {
                    
                //_grabble.Grabbed = true;
                _otherObj = null;
                _grabble = null;
                emptySocket = true;
                return;
            }
        }

        if (_otherObj)
        {
            if (!emptySocket)
            {
                //if(_otherObj.GetComponent<Grabble>() == PicoGrabbleManager.Instance.leftHand)
                if ((Input.GetKeyDown(KeyCode.Q) ||
                     Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)))
                {
                    // _grabble.Grabbed = true;
                    _grabble = _otherObj.GetComponent<Grabble>();
                    return;
                }

                //if(_otherObj.GetComponent<Grabble>() == PicoGrabbleManager.Instance.rightHand)
                if ((Input.GetKeyDown(KeyCode.E) ||
                     Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER)))
                {
                    //_grabble.Grabbed = true;
                    _grabble = _otherObj.GetComponent<Grabble>();
                    return;
                }
            }
        }

        if (_grabble)
        {
            if (_grabble.InLeftHand == false && _grabble.InRightHand == false)
            {
                if (!emptySocket)
                {
                    _grabble.Grab(this.gameObject, 0.7f, 1f);
                } 
            }
        }
    }

    public void ResetState()
    {
        emptySocket = true;
        _otherObj = null;
        _grabble = null;
    }
}