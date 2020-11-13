﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabble : MonoBehaviour 
{
    public bool grabbed;
    [HideInInspector]public Rigidbody rb;

    private void Start()
    {
        if(GetComponent<Rigidbody>())
        rb = GetComponent<Rigidbody>();
        else if(rb == null)
        {
            this.gameObject.AddComponent<Rigidbody>();
            rb = GetComponent<Rigidbody>();
            Debug.Log("You need add RigidBody to " + gameObject.name);
        }
    }

    public void Grab(GameObject controller)
    {
        transform.position = Vector3.Lerp(transform.position,controller.transform.position,0.1f);
        transform.rotation = Quaternion.Slerp(transform.rotation, controller.transform.rotation, 1f);
        //transform.SetPositionAndRotation(controller.transform.position, controller.transform.rotation);
    }
}
