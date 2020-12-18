using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoGrabble : MonoBehaviour
{
    [SerializeField] private bool empty = true;
    [SerializeField] GameObject otherObj;
    private Grabble grabble;
    [SerializeField]private GetOtherObjectTrigger getOtherObjectTrigger;

    private void Start()
    {
        if(!getOtherObjectTrigger)
        getOtherObjectTrigger = GetComponent<GetOtherObjectTrigger>();
    }

    public Grabble Grabble => grabble;

    private void Update()
    {
        if (empty)
        {
            otherObj = getOtherObjectTrigger.ReturnOtherObject();
        }
        else
        {
            otherObj = null;
        }

        if (grabble)
        {
            if (grabble.gameObject == null)
                ResetControllerState();

            if (grabble.InLeftHand)
            {
                grabble.Grab(PicoGrabbleManager.Instance.Controller0, 0.3f, 1f);

                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.Q))
                {
                    ResetControllerState();
                    return;
                }
            }

            if (grabble.InRightHand)
            {
                grabble.Grab(PicoGrabbleManager.Instance.Controller1, 0.3f, 1f);

                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.E))
                {
                    ResetControllerState();
                    return;
                }
            }
        }

        if (otherObj)
        {
            
            if (this.gameObject.Equals(PicoGrabbleManager.Instance.Controller0))
            {
                if (!otherObj.GetComponent<Grabble>().InRightHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.A) ||
                        Input.GetKeyDown(KeyCode.Q))
                    {
                        grabble = otherObj.GetComponent<Grabble>();
                        empty = false;
                        grabble.InLeftHand = true;
                        ActiveMeshRender(false, this.gameObject);
                    }
            }

            if (this.gameObject.Equals(PicoGrabbleManager.Instance.Controller1))
            {
                if (!otherObj.GetComponent<Grabble>().InLeftHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.A) ||
                        Input.GetKeyDown(KeyCode.E))
                    {
                        grabble = otherObj.GetComponent<Grabble>();
                        empty = false;
                        grabble.InRightHand = true;
                        ActiveMeshRender(false, this.gameObject);
                    }
            }
        }
    }

    void ResetControllerState()
    {
        ActiveMeshRender(true, this.gameObject);
        grabble.ResetState();
        empty = true;
        otherObj = null;
        grabble = null;
    }

    private void ActiveMeshRender(bool active, GameObject controller)
    {
        foreach (Renderer r in controller.GetComponentsInChildren<Renderer>())
            r.enabled = active;
    }
}