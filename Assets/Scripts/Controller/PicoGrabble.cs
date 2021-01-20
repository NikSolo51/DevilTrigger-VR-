using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoGrabble : MonoBehaviour
{
    [SerializeField] private GameObject otherObj;
    private Grabble grabble;
    private Ray ray;
    private RaycastHit hit;
    private SenderInfo sender = new SenderInfo();
    private bool keepInLeftController;
    private bool keepInRightController;

    private void Start()
    {
        sender.senderObject = this.gameObject;
        sender.senderRay = ray;
    }
    
    private void Update()
    {
        GetAnObjectUsingRaycast();

        if (grabble)
            if (TryingToThrowAnObjectWithTheLeftController() || TryingToThrowAnObjectWithTheRightController())
                DropObject();
        if (TryingToTakeTheLeftController() || TryingToTakeTheRightController())
            TakeObject();
    }

    private void FixedUpdate()
    {
        if (grabble)
        {
            if (grabble.InLeftHand)
                grabble.Grab(PicoGrabbleManager.Instance.Controller0, 1, 1);
            else
                grabble.Grab(PicoGrabbleManager.Instance.Controller1, 1, 1);
        }
    }

    private void GetAnObjectUsingRaycast()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.white);

        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.collider.GetComponent<IGrabble>()?.GetGrabble(sender))
                Debug.Log(hit.collider.GetComponent<IGrabble>().GetGrabble(sender));
            if (hit.transform.GetComponent<IGrabble>()?.GetGrabble(sender) != null)
            {
                sender.senderRayHit = hit;
                if (!hit.transform.GetComponent<IGrabble>().GetGrabble(sender).GetComponent<Grabble>().Grabbed)
                {
                    otherObj = hit.transform.GetComponent<IGrabble>().GetGrabble(sender);
                    SetOutLineWidth(3f);
                }
            }
            else
            {
                SetOutLineWidth(0.001f);
                otherObj = null;
            }
        }
    }

    private void DropObject()
    {
        if (grabble.gameObject == null)
            ResetControllerState();

        ResetControllerState();
    }

    private bool TryingToThrowAnObjectWithTheLeftController()
    {
        if (grabble.InLeftHand)
        {
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.Left) ||
                Input.GetKeyDown(KeyCode.Q))
            {
                return true;
            }
        }

        return false;
    }

    private bool TryingToThrowAnObjectWithTheRightController()
    {
        if (grabble.InRightHand)
        {
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.Right) ||
                Input.GetKeyDown(KeyCode.E))
            {
                return true;
            }
        }

        return false;
    }

    private void TakeObject()
    {
        grabble = otherObj.GetComponent<Grabble>();
        ActiveMeshRender(false, this.gameObject);

        if (keepInLeftController)
            grabble.InLeftHand = true;
        else
            grabble.InRightHand = true;
    }

    private bool TryingToTakeTheLeftController()
    {
        if (otherObj)
        {
            if (this.gameObject == PicoGrabbleManager.Instance.Controller0)
            {
                if (!otherObj.GetComponent<Grabble>().InRightHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.Left) ||
                        Input.GetKeyDown(KeyCode.Q))
                    {
                        keepInLeftController = true;
                        return true;
                    }
            }
        }

        return false;
    }

    private bool TryingToTakeTheRightController()
    {
        if (otherObj)
        {
            if (this.gameObject == PicoGrabbleManager.Instance.Controller1)
            {
                if (!otherObj.GetComponent<Grabble>().InLeftHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.Right) ||
                        Input.GetKeyDown(KeyCode.E))
                    {
                        keepInRightController = true;
                        return true;
                    }
            }
        }

        return false;
    }


    private void ResetControllerState()
    {
        ActiveMeshRender(true, this.gameObject);
        grabble.ResetState();
        otherObj = null;
        grabble = null;
        keepInLeftController = false;
        keepInRightController = false;
    }

    private void ActiveMeshRender(bool active, GameObject controller)
    {
        foreach (Renderer r in controller.GetComponentsInChildren<Renderer>())
            r.enabled = active;
    }

    public void SetOutLineWidth(float width)
    {
        if (otherObj)
            if (otherObj.GetComponentInChildren<Outline>())
                otherObj.GetComponentInChildren<Outline>().OutlineWidth = width;
    }
}