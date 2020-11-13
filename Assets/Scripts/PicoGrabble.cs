using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;


public class PicoGrabble : MonoBehaviour
{
    [SerializeField] public Pvr_Controller PicoController;
    public bool inLeftHand = false;
    public bool inRightHand = false;
    public Grabble grabbleInLeftHand;
    public Grabble grabbleInRightHand;
    public GameObject other;

    private void Start()
    {
        if (PicoController == null)
            PicoController = GetComponent<Pvr_Controller>();
        if (PicoController == null)
            PicoController = GameObject.FindObjectOfType<Pvr_Controller>();
    }

    private void Update()
    {
        
        if (inLeftHand)
        {
            grabbleInLeftHand.Grab(PicoController.controller0);
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("leftOff");
                grabbleInLeftHand.grabbed = false;
                grabbleInLeftHand.rb.isKinematic = false;
                grabbleInLeftHand = null;
                inLeftHand = false;
                return;
            }
        }

        if (inRightHand)
        {
            grabbleInRightHand.Grab(PicoController.controller1);
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("rightOff");
                grabbleInRightHand.grabbed = false;
                grabbleInRightHand.rb.isKinematic = false;
                grabbleInRightHand = null;
                inRightHand = false;
                return;
            }
        }
        
        if (other)
        {
            if (!other.GetComponent<Grabble>().grabbed)
                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("GrabLeft");
                    grabbleInLeftHand = other.GetComponent<Grabble>();
                    grabbleInLeftHand.rb.isKinematic = true;
                    grabbleInLeftHand.grabbed = true;
                    inLeftHand = true;
                }
            
            if (!other.GetComponent<Grabble>().grabbed)
                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("GrabRight");
                    grabbleInRightHand = other.GetComponent<Grabble>();
                    grabbleInRightHand.rb.isKinematic = true;
                    grabbleInRightHand.Grab(PicoController.controller1);
                    grabbleInRightHand.grabbed = true;
                    inRightHand = true;
                }
        }
        
        
      
    }

   
}