using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;


public class PicoGrabble : MonoBehaviour
{
    [SerializeField] private Pvr_Controller PicoController;
    private float distanceToLeftHand;
    private float distanceToRightHand;
    private float grabDistance = 2;
    [HideInInspector]public bool inHand;
    private float distance;
    [HideInInspector]public GameObject firstController;
    [HideInInspector]public GameObject secondController;
    private int closestController = 2;

    private void Start()
    {
        if (PicoController == null)
            PicoController = GameObject.FindObjectOfType<Pvr_Controller>();
    }

    private void Update()
    {
        UnGrabbedUpdate();
    }

    private void UnGrabbedUpdate()
    {
        
        distanceToLeftHand = 1000f;
        distanceToRightHand = 1000f;
        
        distanceToLeftHand = (transform.position - PicoController.controller0.transform.position).magnitude;
        distanceToRightHand = (transform.position - PicoController.controller1.transform.position).magnitude;

        if (grabDistance > distanceToRightHand || grabDistance > distanceToLeftHand)
        {
            closestController = 2;
            
            distance = Mathf.Min(distanceToLeftHand, distanceToRightHand);

            if (distanceToLeftHand < distanceToRightHand)
            {
                closestController = 0;
                firstController = PicoController.controller0;
                secondController = PicoController.controller1;
            }
            
            if(distanceToRightHand < distanceToLeftHand)
            {
                closestController = 1;
                firstController = PicoController.controller1;
                secondController = PicoController.controller0;
            }

            if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(closestController, Pvr_KeyCode.TRIGGER))
            {
                inHand = true;
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                inHand = true;
            }
        }
        
    }
    
}