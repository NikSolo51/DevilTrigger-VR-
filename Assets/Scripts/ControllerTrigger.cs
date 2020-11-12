using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class ControllerTrigger : MonoBehaviour
{
    [SerializeField] private PicoGrabble _picoGrabble;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Grabble>())
        {
            _picoGrabble.other = other.gameObject;
        }
        else
        {
            _picoGrabble.other = null;
        }
    }
}