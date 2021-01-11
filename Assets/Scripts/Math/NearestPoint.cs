using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPoint 
{
    public GameObject GetNearestPoint(List<GameObject> objectsList, Vector3 point)
    {
        float distanceToClosestObject = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in objectsList)
        {
            if (obj)
            {
                float distanceToEnemy = (obj.transform.position - point).sqrMagnitude;

                if (distanceToEnemy < distanceToClosestObject)
                {
                    distanceToClosestObject = distanceToEnemy;
                    closestObject = obj;
                }
            }
        }

        return closestObject;
    }
    public GameObject GetNearestPoint(List<GameObject> objectsList, GameObject point)
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
