using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CylinderScript : MonoBehaviour , IGrabble
{
    [SerializeField] private GameObject chambers;
    [SerializeField] private GameObject cylinder;
    private bool rotatingCyl = false;
    [SerializeField] private float endAngle = 60F;
    private NearestPoint nearestPoint = new NearestPoint();
    

    private int chambersQueue;
    [SerializeField] private List<GameObject> chamberList = new List<GameObject>();
    public GameObject bulletForShot;
    
    private void Start()
    {
        if (cylinder == null)
            cylinder = this.gameObject;


        for (int i = 0; i < chambers.transform.childCount; i++)
        {
            if (chambers.transform.GetChild(i).GetComponent<Chamber>())
                chamberList.Add(chambers.transform.GetChild(i).gameObject);
                
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Grabble bullet = other?.GetComponent<Grabble>();
        if(bullet)
        if (bullet.BeGrabbed)
        {
            GameObject nearestChamber = nearestPoint.GetNearestPoint(chamberList, other.gameObject);
            bullet.transform.SetParent(nearestChamber.transform);
            bullet.Grab(nearestChamber, 1, 1);
        }
        else
            bullet = null;
    }

    private void Update()
    {
        
        if (rotatingCyl)
        {
            RotateCyl();
        }
        
        if (chamberList[chambersQueue] != null)
            bulletForShot = chamberList[chambersQueue].transform.GetChild(0).gameObject;
        else
            bulletForShot = null;

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
            if(chambersQueue  < chambers.transform.childCount )
            chambersQueue++;
            if (chambersQueue == chambers.transform.childCount)
                chambersQueue = 0;

            rotatingCyl = false;
            endAngle += 60F;
        }
    }
    
    public GameObject GetGrabble(SenderInfo sender)
    {
        
        return nearestPoint.GetNearestPoint(chamberList, sender.senderRayHit.point);
    }

    
}