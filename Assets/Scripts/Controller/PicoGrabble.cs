using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class PicoGrabble : MonoBehaviour
{
    [SerializeField] private bool _empty = true;
    [SerializeField] GameObject _otherObj;
    private Grabble _grabble;
    [Header("GetOtherObjectUseControllerTrigger")]
    [SerializeField] private GetOtherObjectTrigger _getOtherObjectSelfController;
    [Header("GetOtherObjectUseLowDistanceTrigger")]
    [SerializeField] private GetOtherObjectTrigger _getOtherObjectLowDistance;
    public Grabble Grabble => _grabble;
    
    private void Update()
    {
        if (_empty)
            _otherObj = _getOtherObjectLowDistance.ReturnOtherObject() != null ? _getOtherObjectLowDistance.ReturnOtherObject() : _getOtherObjectSelfController.ReturnOtherObject();
        else
            _otherObj = null;

        if (_grabble)
        {
            if (_grabble.InLeftHand)
            {
                _grabble.Grab(PicoGrabbleManager.Instance.Controller0, 0.1f, 0.1f);

                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) ||
                    Input.GetKeyDown(KeyCode.Q))
                {
                    ActiveMeshRender(true, this.gameObject);
                    _grabble.ResetState();
                    _empty = true;
                    _otherObj = null;
                    _grabble = null;
                    return;
                }
            }

            if (_grabble.InRightHand)
            {
                _grabble.Grab(PicoGrabbleManager.Instance.Controller1, 0.1f, 0.1f);

                if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) ||
                    Input.GetKeyDown(KeyCode.E))
                {
                    ActiveMeshRender(true, this.gameObject);
                    _grabble.ResetState();
                    _empty = true;
                    _otherObj = null;
                    _grabble = null;
                    return;
                }
            }
        }

        if (_otherObj)
        {
            if (this.gameObject.Equals(PicoGrabbleManager.Instance.Controller0))
            {
                if (!_otherObj.GetComponent<Grabble>().InRightHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) ||
                        Input.GetKeyDown(KeyCode.Q))
                    {
                        _grabble = _otherObj.GetComponent<Grabble>();
                        _empty = false;
                        _grabble.InLeftHand = true;
                        ActiveMeshRender(false, this.gameObject);
                    }
            }

            if (this.gameObject.Equals(PicoGrabbleManager.Instance.Controller1))
            {
                if (!_otherObj.GetComponent<Grabble>().InLeftHand)
                    if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TRIGGER) ||
                        Input.GetKeyDown(KeyCode.E))
                    {
                        _grabble = _otherObj.GetComponent<Grabble>();
                        _empty = false;
                        _grabble.InRightHand = true;
                        ActiveMeshRender(false, this.gameObject);
                    }
            }
        }
    }

    private void ActiveMeshRender(bool active, GameObject controller)
    {
        foreach (Renderer r in controller.GetComponentsInChildren<Renderer>())
            r.enabled = active;
    }
    
}