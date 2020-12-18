using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;


public class GetOtherObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject otherObj;

    [SerializeField] private LayerMask layerMask;

    // if object in collider > 1 
    [SerializeField] private List<GameObject> otherObjectsList = new List<GameObject>();

   



    private void OnTriggerStay(Collider other)
    {
        
        
        if (other.GetComponent<PicoSocketInteractor>())
        {
            if(other.GetComponent<PicoSocketInteractor>().OtherObj)
            if ((layerMask.value & (1 << other.GetComponent<PicoSocketInteractor>().OtherObj.layer)) > 0)
            {
                if (!otherObjectsList.Contains(other.gameObject.GetComponent<PicoSocketInteractor>().OtherObj))
                    otherObjectsList.Add(other.gameObject.GetComponent<PicoSocketInteractor>().OtherObj);

                if (otherObjectsList.Count > 1)
                {
                    otherObj = NearestPoint(otherObjectsList, this.gameObject);
                    
                    if (otherObj.GetComponent<OutLine>())
                        otherObj.GetComponent<OutLine>().SetOutlineWidth(1.1f);
                }
                else
                {
                    otherObj = other.GetComponent<PicoSocketInteractor>().OtherObj;
                    
                    if (otherObj.GetComponent<OutLine>())
                        otherObj.GetComponent<OutLine>().SetOutlineWidth(1.1f);
                }
            }
        }

        if (other.GetComponent<Grabble>())
        {
            if (other.GetComponent<Grabble>().Interactable)
            {
                if ((other.GetComponent<Grabble>().InLeftHand == false &&
                     other.GetComponent<Grabble>().InRightHand == false))
                {
                    if ((layerMask.value & (1 << other.gameObject.layer)) > 0)
                    {
                        if (!otherObjectsList.Contains(other.gameObject))
                            otherObjectsList.Add(other.gameObject);

                        if (otherObjectsList.Count > 1)
                        {
                            otherObj = NearestPoint(otherObjectsList, this.gameObject);
                            
                            if (otherObj.GetComponent<OutLine>())
                            {
                                otherObj.GetComponent<OutLine>().SetOutlineWidth(1.1f);
                            }
                            
                        }
                        else
                        {
                            otherObj = other.gameObject;
                            if (otherObj.GetComponent<OutLine>())
                                otherObj.GetComponent<OutLine>().SetOutlineWidth(1.1f);
                        }
                    }
                }
            }
            else
            {
                otherObj = null;
            }
        }

        if (otherObjectsList.Count > 1)
        {
            IEnumerable<GameObject> notClose = otherObjectsList.Where(x =>
                x != otherObj);
            
                for (int i = notClose.Count() - 1; i >= 0; i--)
                {
                    if(notClose.ElementAt(i).GetComponent<OutLine>())
                        notClose.ElementAt(i).GetComponent<OutLine>().SetOutlineWidth(1);
                    otherObjectsList.Remove(notClose.ElementAt(i));
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       

        if (other.GetComponent<PicoSocketInteractor>())
            if (other.GetComponent<PicoSocketInteractor>().OtherObj)
            {
                if (other.GetComponent<PicoSocketInteractor>().OtherObj.GetComponent<OutLine>())
                    other.GetComponent<PicoSocketInteractor>().OtherObj.GetComponent<OutLine>().SetOutlineWidth(1.0f);
                
                otherObjectsList.Remove(other.GetComponent<PicoSocketInteractor>().OtherObj);
            }
        
        if (other.GetComponent<OutLine>())
            other.GetComponent<OutLine>().SetOutlineWidth(1.0f);
        
        otherObjectsList.Remove(other.gameObject);
        otherObj = null;
    }


    public GameObject ReturnOtherObject()
    {
        
        return otherObj;
    }

    public GameObject NearestPoint(List<GameObject> objectsList, GameObject point)
    {
        float distanceToClosestObject = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in objectsList)
        {
            if (obj)
            {
                float distanceToEnemy = (obj.transform.position - point.transform.position).sqrMagnitude;

                if (distanceToEnemy < distanceToClosestObject)
                {
                    distanceToClosestObject = distanceToEnemy;
                    closestObject = obj;
                }
            }
        }

        return closestObject;
    }
}