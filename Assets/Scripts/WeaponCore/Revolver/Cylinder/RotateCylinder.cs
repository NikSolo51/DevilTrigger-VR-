using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCylinder : MonoBehaviour
{
    public RevolverEventSender RevolverEventSender;
    [SerializeField] private GameObject cylinder;
    private bool rotatingCyl = false;
    [SerializeField] private float endAngle = 60F;
    
    public void Awake()
    {
        RevolverEventSender.OnRotateCylinder += RotateCyl;
    }

    private void OnDestroy()
    {
        RevolverEventSender.OnRotateCylinder -= RotateCyl;
    }

    private void Start()
    {
        if (cylinder == null)
            cylinder = this.gameObject;
    }

    public void Update()
    {
        if (rotatingCyl)
        {
            RotateCyl();
        }
    }
    
    public void RotateCyl()
    {
        if (endAngle == 360F && cylinder.transform.localRotation.eulerAngles.y < 60F)
        {
            endAngle = 0F;
        }

        if (cylinder.transform.localRotation.eulerAngles.y < endAngle)
        {
            rotatingCyl = true;
            Quaternion target = Quaternion.Euler(0, endAngle, 0);
            cylinder.transform.localRotation = Quaternion.RotateTowards(cylinder.transform.localRotation, target, Time.deltaTime * 100F);
        }
        else
        {
            rotatingCyl = false;
            endAngle += 60F;
        }
        
    }
}
