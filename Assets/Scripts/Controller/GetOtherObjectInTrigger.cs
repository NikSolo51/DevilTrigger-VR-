using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOtherObjectInTrigger : MonoBehaviour
{
    private GameObject otherObj;
    public bool objectEnter;
    public bool objectExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Grabble>())
        {
            objectExit = false;
            objectEnter = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Grabble>())
        {
            otherObj = other.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Grabble>())
        {
            objectExit = true;
            objectEnter = false;
            otherObj = null;
        }
    }
    
    public GameObject GetObject()
    {
        return otherObj;
    }
}
