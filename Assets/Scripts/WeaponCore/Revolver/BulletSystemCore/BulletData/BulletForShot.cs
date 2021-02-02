using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForShot : MonoBehaviour
{
    public RevolverEventSender RevolverEventSender;
    public ChambersData chambersData;
    private int bulletsQueue;
    private GameObject bulletForShot;

    public delegate void GetBulletForShot(Bullet bulletForShot);
    public event GetBulletForShot OnGetBulletForShot;

    private void Start()
    {
        UpdateBulletForShot();
       
    }

    public GameObject BulletForShoot
    {
        get => bulletForShot;
        set => bulletForShot = value;
    }

    private void Awake()
    {
        RevolverEventSender.OnUpdateBulletQueue += UpdateBulletQueue;
    }

    private void OnDestroy()
    {
        RevolverEventSender.OnUpdateBulletQueue -= UpdateBulletQueue;
    }

    private void UpdateBulletQueue()
    {
        if(bulletsQueue  < 6 )
            bulletsQueue++;
        if (bulletsQueue == 6)
            bulletsQueue = 0;

        UpdateBulletForShot();

    }

    private void UpdateBulletForShot()
    {
        if (chambersData.bulletGOList[bulletsQueue] != null)
        {
            bulletForShot = chambersData.bulletGOList[bulletsQueue];
            OnGetBulletForShot?.Invoke(bulletForShot.GetComponent<Bullet>());
        }
        else
            bulletForShot = null;
    }
}
