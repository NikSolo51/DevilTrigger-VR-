using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabble : MonoBehaviour , IGrabble
{
    [SerializeField] bool interactable = true;
    [HideInInspector] Rigidbody rb;
    private const float G = 667.4f;
    [SerializeField]bool grabbed = false;
    [SerializeField]bool inLeftHand = false;
    [SerializeField]bool inRightHand = false;
    [SerializeField]Vector3 axis;
    [SerializeField]float offsetRotation;
    bool isLerping;
    
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
        StartCoroutine(WasCaptured());
        rb.useGravity = true;
        grabbed = false;
        inLeftHand = false;
        inRightHand = false;
    }
    
    public void Grab(GameObject grabber, float speed, float speedRotation)
    {
        /*
        if (Vector3.Distance(grabber.transform.position, transform.position) <= .1f)
        {
            transform.position = grabber.transform.position;
        }
         grabbed = true;
        rb.useGravity = false;
        float force = 6.67f *  (2 * rb.mass *  (Vector3.Distance(grabber.transform.position,this.transform.position))) / Mathf.Pow(0.2f, 2);
        rb.AddForce(force  * Vector3.Normalize(grabber.transform.position - transform.position));
        transform.rotation = Quaternion.Slerp(transform.rotation, grabber.transform.rotation, speedRotation);
        transform.Rotate(axis,offsetRotation);
        */
        grabbed = true;
        rb.velocity = (grabber.transform.position - transform.position) / Time.fixedDeltaTime;
        rb.maxAngularVelocity = 20;
        Quaternion deltaRot = grabber.transform.rotation * Quaternion.Inverse(transform.rotation);
        Vector3 eulerRot = new Vector3(Mathf.DeltaAngle(0,deltaRot.eulerAngles.x)
            ,Mathf.DeltaAngle(0,deltaRot.eulerAngles.y),Mathf.DeltaAngle(0,deltaRot.eulerAngles.z));
        eulerRot *= 0.95f;
        eulerRot *= Mathf.Deg2Rad;
        rb.angularVelocity = eulerRot / Time.fixedDeltaTime;
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