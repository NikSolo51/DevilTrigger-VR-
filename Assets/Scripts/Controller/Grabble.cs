using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabble : MonoBehaviour
{
    [SerializeField] bool interactable = true;
    
    [HideInInspector] Rigidbody rb;

    [SerializeField]bool grabbed = false;
    [SerializeField]bool inLeftHand = false;
    [SerializeField]bool inRightHand = false;
    [SerializeField]Vector3 axis;
    [SerializeField]float offsetRotation;
    public bool Grabbed
    {
        get => grabbed;
        set => grabbed = value;
    }

    public Rigidbody Rb
    {
        get => rb;
        set => rb = value;
    }

    public bool InLeftHand
    {
        get => inLeftHand;
        set => inLeftHand = value;
    }

    public bool InRightHand
    {
        get => inRightHand;
        set => inRightHand = value;
    }
    
    public bool Interactable
    {
        get => interactable;
        set => interactable = value;
    }


    private void Start()
    {
        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
    }
    
    public void ResetState()
    {
        rb.isKinematic = false;
        grabbed = false;
        inLeftHand = false;
        inRightHand = false;
    }
    
    public void Grab(GameObject grabber, float percentPosition, float percentRotation)
    {
        rb.isKinematic = true; 
        grabbed = true;
        transform.position = Vector3.Lerp(transform.position, grabber.transform.position, percentPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, grabber.transform.rotation, percentRotation);
        transform.Rotate(axis,offsetRotation);
    }
}