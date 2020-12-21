using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CylinderScript : MonoBehaviour
{
    [SerializeField] private GameObject Chambers;
    [SerializeField] private GameObject Cylinder;
    [SerializeField] private bool rotatingCyl = false;
    [SerializeField] private float endAngle = 60F;
    
    private Dictionary<int, PicoSocketInteractor> chambersDictionary = new Dictionary<int, PicoSocketInteractor>();

    private int chambersQueue;
    [SerializeField] private List<GameObject> bulletList = new List<GameObject>();
    private List<PicoSocketInteractor> picoSocketsInteractors = new List<PicoSocketInteractor>();
    public GameObject bulletForShot;
    // Duplicates OtheObjects In PicoSocketInteractors
    public List<PicoSocketInteractor> dublicatesOtheObj;
   
    

    private void Start()
    {
        if (Cylinder == null)
            Cylinder = this.gameObject;

        for (int i = 0; i < Chambers.transform.childCount; i++)
        {
            bulletList.Add(new GameObject());
            picoSocketsInteractors.Add(new PicoSocketInteractor());
            Destroy(bulletList[i]);
            chambersDictionary.Add(i, Chambers.transform.GetChild((Chambers.transform.childCount -1 ) - i).GetComponent<PicoSocketInteractor>());
        }
    }
    
    private void Update()
    {
        if (rotatingCyl)
        {
            RotateCyl();
        }
        
        for (int i = 0; i < chambersDictionary.Values.Count; i++)
        {
            picoSocketsInteractors[i] = chambersDictionary.Values.ElementAt(i);
            bulletList[i] = picoSocketsInteractors[i].ObjectInPicoIntrerafctor;
        }
        
        
        IEnumerable<PicoSocketInteractor> duplicates = picoSocketsInteractors.GroupBy(s => s.ObjectInPicoIntrerafctor).SelectMany(grp => grp.Skip(1));
        dublicatesOtheObj = duplicates.ToList();
        
        for (int i = 0; i < dublicatesOtheObj.Count; i++)
        {
           // dublicatesOtheObj[i].ResetState();    
        }
        

        if (bulletList[chambersQueue] != null)
            bulletForShot = bulletList[chambersQueue];
        else
            bulletForShot = null;

    }

    public void RotateCyl()
    {
        if (endAngle == 360F && Cylinder.transform.localRotation.eulerAngles.y < 60F)
        {
            endAngle = 0F;
        }

        if (Cylinder.transform.localRotation.eulerAngles.y < endAngle)
        {
            rotatingCyl = true;
            Quaternion target = Quaternion.Euler(0, endAngle, 0);
            Cylinder.transform.localRotation = Quaternion.RotateTowards(Cylinder.transform.localRotation, target, Time.deltaTime * 100F);
        }
        else
        {
            if(chambersQueue  < Chambers.transform.childCount )
            chambersQueue++;
            if (chambersQueue == Chambers.transform.childCount)
                chambersQueue = 0;

            rotatingCyl = false;
            endAngle += 60F;
        }
    }

    public void OpenCylinder(bool open)
    {
        if (open)
        {
            for (int i = 0; i < picoSocketsInteractors.Count; i++)
            {
                if (picoSocketsInteractors[i].ObjectInPicoIntrerafctor)
                {
                    picoSocketsInteractors[i].ObjectInPicoIntrerafctor.GetComponent<Grabble>().gameObject.layer = 0;
                }
                    
            }
        }
        else
        {
            for (int i = 0; i < picoSocketsInteractors.Count; i++)
            {
                if (picoSocketsInteractors[i].ObjectInPicoIntrerafctor)
                {
                    picoSocketsInteractors[i].ObjectInPicoIntrerafctor.GetComponent<Grabble>().gameObject.layer = 8;
                }
            }
        }
    }
}