using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChambersData : MonoBehaviour
{
    [SerializeField] private  RevolverDrumManager revolverDrumManager;
    public GameObject chambers;
    public List<GameObject> chambersGOList = new List<GameObject>();
    public List<GameObject> bulletGOList = new List<GameObject>();
    private NearestPoint nearestPoint = new NearestPoint();
    
    public void Awake()
    {
        revolverDrumManager.OnAddBulletInMagazine += AddBulletInDataBase;
        InitializeChambers();
        InitializeBullet();
    }

    private void OnDestroy()
    {
        revolverDrumManager.OnAddBulletInMagazine -= AddBulletInDataBase;
    }

    private void InitializeChambers()
    {
        for (int i = 0; i < chambers.transform.childCount; i++)
        {
            chambersGOList.Add(chambers.transform.GetChild(i).gameObject);
        }
    }
    
    private void InitializeBullet()
    {
        for (int i = 0; i < chambers.transform.childCount; i++)
        {
            bulletGOList.Add(chambers.transform.GetChild(i).GetChild(0).gameObject);
        } 
    }
    
    public void AddBulletInDataBase(GameObject bullet, int index)
    {
        bulletGOList[index] = bullet;
    }

    public GameObject GetNearestBulletRelativeToPoint(Vector3 point)
    {
        return nearestPoint.GetNearestPoint(bulletGOList, point);
    }
    
    
    public GameObject GetNearestChambersRelativeToPoint(Vector3 point)
    {
        return nearestPoint.GetNearestPoint(chambersGOList, point);
    }
    
    private void Update()
    {
        for (int i = 0; i < bulletGOList.Count; i++)
        {
            if(bulletGOList[i])
                if (bulletGOList[i].GetComponent<Grabble>().Grabbed)
                    bulletGOList[i] = null;
        }
    }
}
