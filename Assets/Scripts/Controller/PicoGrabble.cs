using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;


public class PicoGrabble : MonoBehaviour
{
    public static PicoGrabble Instance;
    [SerializeField] public Pvr_Controller PicoController;
    [SerializeField] public GetOtherObjectInTrigger _getOtherObjectLeftController;
    [SerializeField] public GetOtherObjectInTrigger _getOtherObjectRightController;
    bool inLeftHand = false;
    bool inRightHand = false;
    Grabble grabbleInLeftHand;
    Grabble grabbleInRightHand;
    private GameObject otherLeft;
    private GameObject otherRight;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
            
    }

    private void Start()
    {
        if (PicoController == null)
            PicoController = GetComponent<Pvr_Controller>();
        if (PicoController == null)
            PicoController = GameObject.FindObjectOfType<Pvr_Controller>();
    }

    private void Update()
    {
        if(_getOtherObjectLeftController.GetObject() && _getOtherObjectLeftController.GetObject().GetComponent<Grabble>())
            otherLeft = _getOtherObjectLeftController.GetObject();
        if (_getOtherObjectLeftController.GetObject() == null)
            otherLeft = null;
        if(_getOtherObjectRightController.GetObject() && _getOtherObjectRightController.GetObject().GetComponent<Grabble>())
            otherRight = _getOtherObjectRightController.GetObject();
        if (_getOtherObjectRightController.GetObject() == null)
            otherRight = null;
            
        
        if (inLeftHand)
        {
            grabbleInLeftHand.Grab(PicoController.controller0,0.1f,0.1f);
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("leftOff");
                ActiveMeshRender(true, PicoController.controller0);
                grabbleInLeftHand.grabbed = false;
                grabbleInLeftHand.rb.isKinematic = false;
                grabbleInLeftHand = null;
                otherLeft = null;
                inLeftHand = false;
                return;
            }
        }

        if (inRightHand)
        {
            grabbleInRightHand.Grab(PicoController.controller1, 0.1f,0.1f);
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("rightOff");
                ActiveMeshRender(true, PicoController.controller1);
                grabbleInRightHand.grabbed = false;
                grabbleInRightHand.rb.isKinematic = false;
                grabbleInRightHand = null;
                otherRight = null;
                inRightHand = false;
                return;
            }
        }

        if (otherLeft != null)
            if (!otherLeft.GetComponent<Grabble>().grabbed)
                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("GrabLeft");
                    ActiveMeshRender(false, PicoController.controller0);
                    grabbleInLeftHand = otherLeft.GetComponent<Grabble>();
                    grabbleInLeftHand.grabbed = true;
                    inLeftHand = true;
                }
            
       if(otherRight != null)
            if (!otherRight.GetComponent<Grabble>().grabbed)
                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("GrabRight");
                    ActiveMeshRender(false, PicoController.controller1);
                    grabbleInRightHand = otherRight.GetComponent<Grabble>();
                    grabbleInRightHand.grabbed = true;
                    inRightHand = true;
                }
        
    }

    private void ActiveMeshRender(bool active,GameObject controller)
    {
        foreach (Renderer r in controller.GetComponentsInChildren<Renderer>())
            r.enabled = active;
    }

   
}