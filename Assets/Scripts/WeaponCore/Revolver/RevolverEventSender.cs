using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverEventSender : MonoBehaviour
{
    public delegate void UpdateBulletQueue();

    public event UpdateBulletQueue OnUpdateBulletQueue;

    public delegate void RotateCylinder();

    public event RotateCylinder OnRotateCylinder;

    public void UpdateBulletQueueEvent()
    {
        OnUpdateBulletQueue?.Invoke();
    }

    public void UpdateRotateCylinderEvent()
    {
        OnRotateCylinder?.Invoke();
    }
}
