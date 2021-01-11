using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoGrabble : MonoBehaviour
{
    [SerializeField] private bool empty = true;
    [SerializeField] private GameObject otherObj;
    [SerializeField] private LayerMask _layerMask;
    private Grabble grabble;
    private Ray ray;
    private RaycastHit hit;
    private SenderInfo sender = new SenderInfo();

    private void Start()
    {
        sender.senderObject = this.gameObject;
        sender.senderRay = ray;
    }

    public Grabble Grabble => grabble;

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.white);

        if (Physics.Raycast(ray, out hit,10))
        {
            if(hit.collider.GetComponent<IGrabble>()?.GetGrabble(sender))
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

        DropObject();
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

    public void DropObject()
    {
        if (grabble)
        {
            if (grabble.gameObject == null)
                ResetControllerState();

            if (grabble.InLeftHand)
            {
                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.Left) ||
                    Input.GetKeyDown(KeyCode.Q))
                {
                    ResetControllerState();
                    return;
                }
            }

            if (grabble.InRightHand)
            {
                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.Right) ||
                    Input.GetKeyDown(KeyCode.E))
                {
                    ResetControllerState();
                    return;
                }
            }
        }
    }

    public void TakeObject()
    {
        if (otherObj)
        {
            if (this.gameObject == PicoGrabbleManager.Instance.Controller0)
            {
                if (!otherObj.GetComponent<Grabble>().InRightHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.Left) ||
                        Input.GetKeyDown(KeyCode.Q))
                    {
                        grabble = otherObj.GetComponent<Grabble>();
                        empty = false;
                        grabble.InLeftHand = true;
                        ActiveMeshRender(false, this.gameObject);
                    }
            }

            if (this.gameObject == PicoGrabbleManager.Instance.Controller1)
            {
                if (!otherObj.GetComponent<Grabble>().InLeftHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.Right) ||
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

    public void SetOutLineWidth(float width)
    {
        if (otherObj)
            if (otherObj.GetComponentInChildren<Outline>())
                otherObj.GetComponentInChildren<Outline>().OutlineWidth = width;
    }
}