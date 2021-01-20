using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForShot : MonoBehaviour
{
    public RevolverEventSender revolverEventSender;
    public BulletData bulletData;
    private int bulletsQueue;
    private GameObject bulletForShot;

    public GameObject BulletForShoot
    {
        get => bulletForShot;
        set => bulletForShot = value;
    }

    private void Awake()
    {
        revolverEventSender.OnUpdateBulletQueue += UpdateBulletQueue;
    }

    private void OnDestroy()
    {
        revolverEventSender.OnUpdateBulletQueue -= UpdateBulletQueue;
    }

    private void UpdateBulletQueue()
    {
        if(bulletsQueue  < bulletData.bullets.transform.childCount )
            bulletsQueue++;
        if (bulletsQueue == bulletData.bullets.transform.childCount)
            bulletsQueue = 0;

        UpdateBulletForShot();

    }

    private void UpdateBulletForShot()
    {
        if (bulletData.bulletList[bulletsQueue] != null)
            bulletForShot = bulletData.bulletList[bulletsQueue].transform.GetChild(0).gameObject;
        else
            bulletForShot = null;
    }
}
