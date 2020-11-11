using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDefault : IBullet
{
    private int baseDamage = 7;
    private int modificator = 0;
    private float speed;
    
    public float GetDamage()
    {
        return baseDamage + modificator;
    }
}
