using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour, IBullet
{
    public float GetDamage()
    {
        return 10f;
    }
}