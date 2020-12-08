using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoSocketInteractor : MonoBehaviour
{
    [SerializeField] private bool emptySocket = true;
    [SerializeField] private bool block = false;
    [SerializeField] GameObject otherObj;
    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] Grabble grabble;

    public bool Block
    {
        get => block;
        set => block = value;
    }

    public GameObject OtherObj
    {
        get { return otherObj; }

        set { otherObj = value; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (layerMask == (1 << other.gameObject.layer))
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
        
        if (grabble)
        {
            if (otherObj)
            {
                if (otherObj == PicoGrabbleManager.Instance._controller0)
                    if ((Input.GetKeyDown(KeyCode.Q) ||
                         Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.A)))
                    {
                        if (grabble.InLeftHand)
                        {
                            ResetState();
                            return;
                        }
                    }

                if (otherObj == PicoGrabbleManager.Instance._controller1)
                    if ((Input.GetKeyDown(KeyCode.E) ||
                         Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.A)))
                    {
                        if (grabble.InRightHand)
                        {
                            ResetState();
                            return;
                        }
                    }
            }
        }

        if (otherObj)
        {
            if (emptySocket)
            {
                
                if (otherObj.GetComponent<Grabble>().InLeftHand)
                    if ((Input.GetKeyDown(KeyCode.Q) ||
                         Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.A)))
                    {
                        
                        grabble = otherObj.GetComponent<Grabble>();
                        if (block)
                            grabble.Interactable = false;
                        emptySocket = false;
                        return;
                    }

                if (otherObj.GetComponent<Grabble>().InRightHand)
                    if ((Input.GetKeyDown(KeyCode.E) ||
                         Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.A)))
                    {
                        grabble = otherObj.GetComponent<Grabble>();
                        if (block)
                            grabble.Interactable = false;
                        emptySocket = false;
                        return;
                    }
            }
        }

        if (grabble)
        {
            if (grabble.InLeftHand == false && grabble.InRightHand == false)
            {
                if (!emptySocket)
                {
                   
                    grabble.Grab(this.gameObject, 1f, 1f);
                }
            }
        }
    }

    public void ResetState()
    {
        otherObj = null;
        grabble = null;
        emptySocket = true;
    }
}