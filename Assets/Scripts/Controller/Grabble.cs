using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabble : MonoBehaviour , IGrabble
{
    [SerializeField] bool interactable = true;
    [HideInInspector] Rigidbody rb;

    [SerializeField]bool grabbed = false;
    [SerializeField]bool inLeftHand = false;
    [SerializeField]bool inRightHand = false;
    [SerializeField]Vector3 axis;
    [SerializeField]float offsetRotation;
    
    private bool beGrabbed;

    public bool BeGrabbed
    {
        get => beGrabbed;
        set => beGrabbed = value;
    }

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
        WasCaptured();
        grabbed = false;
        inLeftHand = false;
        inRightHand = false;
    }
    
    public void Grab(GameObject grabber, float speed, float speedRotation)
    {
        grabbed = true;
        rb.AddForce(((grabber.transform.position - transform.position) * 500f) * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, grabber.transform.rotation, speedRotation);
        transform.Rotate(axis,offsetRotation);
    }

    private IEnumerator WasCaptured()
    {
        beGrabbed = true;
        yield return  new WaitForSeconds(0.4f);
        beGrabbed = false;
    }


    public GameObject GetGrabble(SenderInfo sender)
    {
        return this.gameObject;
    }
    
}