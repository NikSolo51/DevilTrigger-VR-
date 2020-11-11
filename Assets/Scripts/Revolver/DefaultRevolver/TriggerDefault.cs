using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDefault : MonoBehaviour , ITrigger
{
    private float shotFrequency = 10;
        
    
    public float GetShotFrequency()
    {
        return shotFrequency;
    }
}
