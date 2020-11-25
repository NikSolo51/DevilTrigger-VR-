using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GetOtherObjectTrigger : MonoBehaviour
{
    [SerializeField]private GameObject _otherObj;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Grabble>())
            if (other.GetComponent<Grabble>().Interactable)
            {
                if (!(other.GetComponent<Grabble>().InLeftHand == false &&
                      other.GetComponent<Grabble>().InRightHand == false))
                {
                    _otherObj = other.gameObject;
                }
            }
            else
            {
                _otherObj = null;
            }

        if (other.GetComponent<PicoSocketInteractor>())
            _otherObj = other.GetComponent<PicoSocketInteractor>().OtherObj;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.GetComponent<Grabble>())
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

        if (other.GetComponent<PicoSocketInteractor>())
        {
            _otherObj = other.GetComponent<PicoSocketInteractor>().OtherObj;
        }
    }
    
    
    public GameObject ReturnOtherObject()
    {
        return _otherObj;
    }
}
