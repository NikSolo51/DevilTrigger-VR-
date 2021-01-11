using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoSocketInteractor : MonoBehaviour
{
    [SerializeField] private bool emptySocket = true;
    [SerializeField] GameObject otherObj;
    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] Grabble grabble;
    private bool triggerEmpty;
    
    public GameObject ObjectInPicoIntrerafctor
    {
        get
        {
            return otherObj;
        }

        set { otherObj = value; }
    }
    
    private void OnTriggerStay(Collider other)
    {
        triggerEmpty = false;
        
        if ((layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (other.gameObject.GetComponent<Grabble>())
            {
                if (other.gameObject.GetComponent<Grabble>().Interactable)
                {
                    otherObj = other.gameObject;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        
        if (!emptySocket)
        {
            if(grabble)
            if (grabble.InLeftHand || grabble.InRightHand)
            {
                Debug.Log("IsResetState" + this.gameObject);
                ResetState();
                return;
            }
        }
        
        if (otherObj)
            if (emptySocket)
            {
                if(otherObj.GetComponent<Grabble>().InLeftHand)
                {
                    if (otherObj.GetComponent<Grabble>().InLeftHand)
                        if (Input.GetKeyDown(KeyCode.Q) ||
                             Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.A))
                        {
                            grabble = otherObj.GetComponent<Grabble>();
                            emptySocket = false;
                            return;
                        }
                }
                

                if (otherObj.GetComponent<Grabble>().InRightHand)
                    if (Input.GetKeyDown(KeyCode.E) ||
                         Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.A))
                    {
                        grabble = otherObj.GetComponent<Grabble>();
                        emptySocket = false;
                        return;
                    }
            }

        if (grabble)
        {
            if (!emptySocket)
            {
                if(!grabble.InLeftHand && !grabble.InRightHand)
                grabble.Grab(this.gameObject,1,1);
            }
        }
        
        triggerEmpty = true;
        if (triggerEmpty)
        {
            otherObj = null;
        }
    }

    public void ResetState()
    {
        if(otherObj)
        otherObj.transform.SetParent(null);
        otherObj = null;
        grabble = null;
        emptySocket = true;
    }
}