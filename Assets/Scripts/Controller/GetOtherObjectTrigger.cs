using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Object = System.Object;


public class GetOtherObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject otherObj;

    [SerializeField] private LayerMask layerMask;

    // if object in collider > 1 
    [SerializeField] private List<GameObject> otherObjectsList = new List<GameObject>();
    public GameObject OtherObj => otherObj;


    private void OnTriggerStay(Collider other)
    {
        otherObj = GetObjectFromPicoSocketInteractor(other);
        otherObj = GetOtherObject(other);
        DeleteUnselectedObjectsInTrigger();
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject exitObject;
        if (other.gameObject.GetComponent<PicoSocketInteractor>())
            exitObject = other.gameObject.GetComponent<PicoSocketInteractor>().ObjectInPicoIntrerafctor
                ? other.gameObject.GetComponent<PicoSocketInteractor>().ObjectInPicoIntrerafctor
                : other.gameObject;
        else
            exitObject = other.gameObject;


        SetOutLineOtherObj(exitObject, 0.001f);
        otherObjectsList.Remove(exitObject);

        otherObj = null;
    }

    public GameObject GetObjectFromPicoSocketInteractor(Collider other)
    {
        if (other.GetComponent<PicoSocketInteractor>())
            if (other.GetComponent<PicoSocketInteractor>().ObjectInPicoIntrerafctor)
            {
                GameObject objectInPicoIntrerafctor =
                    other.GetComponent<PicoSocketInteractor>().ObjectInPicoIntrerafctor;

                if ((layerMask.value &
                     (1 << objectInPicoIntrerafctor.layer)) > 0)
                {
                    if (!otherObjectsList.Contains(objectInPicoIntrerafctor))
                        otherObjectsList.Add(objectInPicoIntrerafctor);

                    if (otherObjectsList.Count > 1)
                    {
                        SetOutLineOtherObj(objectInPicoIntrerafctor, 3f);
                        return NearestPoint(otherObjectsList, this.gameObject);
                    }
                    else
                    {
                        SetOutLineOtherObj(objectInPicoIntrerafctor, 3f);
                        return objectInPicoIntrerafctor;
                    }
                }
            }

        return null;
    }

    public GameObject GetOtherObject(Collider other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.GetComponent<Grabble>())
        {
            if (otherObj.GetComponent<Grabble>().Interactable)
                if ((otherObj.GetComponent<Grabble>().InLeftHand == false &&
                     otherObj.GetComponent<Grabble>().InRightHand == false))
                {
                    if ((layerMask.value & (1 << otherObj.layer)) > 0)
                    {
                        if (!otherObjectsList.Contains(otherObj))
                            otherObjectsList.Add(otherObj);

                        if (otherObjectsList.Count > 1)
                        {
                            otherObj = NearestPoint(otherObjectsList, this.gameObject);
                            SetOutLineOtherObj(otherObj, 3f);
                            return otherObj;
                        }
                        else
                        {
                            SetOutLineOtherObj(otherObj,
                                3f);
                            return otherObj;
                        }
                    }
                }
        }

        return null;
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

    public void SetOutLineOtherObj(GameObject otheobj, float widthLine)
    {
        if (otheobj.GetComponentInChildren<Outline>())
            otheobj.GetComponentInChildren<Outline>().OutlineWidth = widthLine;
    }

    public void DeleteUnselectedObjectsInTrigger()
    {
        if (otherObjectsList.Count > 1)
        {
            IEnumerable<GameObject> notClose = otherObjectsList.Where(x =>
                x != otherObj);

            for (int i = notClose.Count() - 1; i >= 0; i--)
            {
                if (notClose.ElementAt(i).GetComponentInChildren<Outline>())
                    notClose.ElementAt(i).GetComponentInChildren<Outline>().OutlineWidth = 0.001f;
                otherObjectsList.Remove(notClose.ElementAt(i));
            }
        }
    }
}