using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetOtherObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _otherObj;


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PicoSocketInteractor>())
        {
            _otherObj = other.GetComponent<PicoSocketInteractor>().OtherObj;
        }
        
        
        
        if (other.GetComponent<Grabble>())
        {
            if (other.GetComponent<Grabble>().Interactable)
            {
                if ((other.GetComponent<Grabble>().InLeftHand == false &&
                     other.GetComponent<Grabble>().InRightHand == false))
                    {
                        _otherObj = other.gameObject;
                    }
            }
            else
            {
                _otherObj = null;
            }
        }
        

       
    }

    private void OnTriggerExit(Collider other)
    {
        _otherObj = null;
    }


    public GameObject ReturnOtherObject()
    {
        return _otherObj;
    }
}