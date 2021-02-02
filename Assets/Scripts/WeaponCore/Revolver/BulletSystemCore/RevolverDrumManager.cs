using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverDrumManager : MonoBehaviour , IGrabble
{
    [SerializeField] private ChambersData chambersData;
    private GameObject nearestChamber;
    private Collider otherObject;

    public delegate void AddBulletInMagazine(GameObject bullet, int index);
    public event  AddBulletInMagazine OnAddBulletInMagazine;
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.GetComponent<Grabble>() && !chambersData.bulletGOList.Contains(other.gameObject))
        {
            otherObject = other;
        }
           
    }

    private void FixedUpdate()
    {
        if(otherObject != null)
            DragTheBulletToAnEmptySlot(otherObject);
    }

    public void DragTheBulletToAnEmptySlot(Collider other)
    {
        Grabble bullet = other.GetComponent<Grabble>();
        
        if(chambersData.bulletGOList.Contains(bullet.gameObject))
            return;
        if (bullet)
                if (bullet.BeGrabbed)
                {
                    for (int i = 0; i < chambersData.chambersGOList.Count; i++)
                    {
                        if (chambersData.GetNearestChambersRelativeToPoint(bullet.gameObject.transform.position)
                            .transform.childCount == 0)
                            nearestChamber = chambersData.GetNearestChambersRelativeToPoint(bullet.gameObject.transform.position);
                        else
                            return;
                    }

                    bullet.transform.position = nearestChamber.transform.position;
                    bullet.transform.SetParent(nearestChamber.transform);
                    bullet.Rb.isKinematic = true;
                    OnAddBulletInMagazine?.Invoke(bullet.gameObject,nearestChamber.transform.GetSiblingIndex());
                }
                else
                    bullet = null;
    }

    public GameObject GetGrabble(SenderInfo sender)
    {
        GameObject nearestBullet = chambersData.GetNearestBulletRelativeToPoint(sender.senderRayHit.point);
        return nearestBullet;
    }
}
