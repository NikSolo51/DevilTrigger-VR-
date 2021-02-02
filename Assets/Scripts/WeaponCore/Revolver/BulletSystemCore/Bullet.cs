using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public enum BylletTypeEnum
{
    defaultbullet,
    firebullet
}

public class Bullet : MonoBehaviour
{
    public PrefabBulletData BulletData;
    public BylletTypeEnum BylletTypeEnum;
    private GameObject bulletPrefab;

    public GameObject BulletPrefab
    {
        get => bulletPrefab;
        set => bulletPrefab = value;
    }

    private void Start()
    {
        SetBulletType(BylletTypeEnum);
    }

    public void SetBulletType(BylletTypeEnum typeBullet)
    {
        if (typeBullet == BylletTypeEnum.defaultbullet)
        {
            bulletPrefab = BulletData.bulletList.Where(x => x.GetComponent<DefaultBullet>()).ToList()[0];
        }


        if (BylletTypeEnum == BylletTypeEnum.firebullet)
        {
            bulletPrefab = BulletData.bulletList.Where(x => x.GetComponent<FireBullet>()).ToList()[0];
        }
    }

   
}