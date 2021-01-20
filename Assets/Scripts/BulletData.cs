using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    public GameObject bullets;
    public BulletManager bulletManager;
    public List<GameObject> bulletList = new List<GameObject>();
    private NearestPoint nearestPoint = new NearestPoint();

    public void Awake()
    {
        bulletManager.OnAddBulletInMagazine += AddBulletInDataBase;
        
    }

    private void OnDestroy()
    {
        bulletManager.OnAddBulletInMagazine -= AddBulletInDataBase;
    }

    private void Start()
    {
        for (int i = 0; i < bullets.transform.childCount; i++)
        {
            if (bullets.transform.GetChild(i).GetComponent<Chamber>())
                bulletList.Add(bullets.transform.GetChild(i).gameObject);
                
        }
    }

    public void AddBulletInDataBase(GameObject bullet)
    {
        bulletList.Add(bullet);
    }

    public GameObject GetNearestBulletRelativeToPoint(Vector3 point)
    {
        return nearestPoint.GetNearestPoint(bulletList, point);
    }

    private void Update()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i].GetComponent<Grabble>().Grabbed)
                bulletList.Remove(bulletList[i]);
            if(bulletList[i] == null)
                bulletList.Remove(bulletList[i]);
        }
    }
}
