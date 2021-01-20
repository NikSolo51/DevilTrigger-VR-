using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    private GameObject nearestBullet ;

    public delegate void AddBulletInMagazine(GameObject bullet);
    public event  AddBulletInMagazine OnAddBulletInMagazine;
    
    private void OnTriggerStay(Collider other)   
    {
        DragTheBulletToAnEmptySlot(other);
    }
   
    public void DragTheBulletToAnEmptySlot(Collider other)
    {
        Grabble bullet = other?.GetComponent<Grabble>();
        if (bullet)
            if (bullet.BeGrabbed)
            {
                nearestBullet = bulletData.GetNearestBulletRelativeToPoint(other.gameObject.transform.position);
                bullet.transform.SetParent(nearestBullet.transform);
                bullet.Grab(nearestBullet, 1, 1);
                OnAddBulletInMagazine?.Invoke(nearestBullet);
            }
            else
                bullet = null;
    }

    public GameObject GetGrabble(SenderInfo sender)
    {
        return bulletData.GetNearestBulletRelativeToPoint(sender.senderRayHit.point).transform.GetChild(0).gameObject;
    }
}
