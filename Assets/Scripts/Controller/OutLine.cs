using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    [SerializeField] 
    [Range(1.0f, 5.0f)]float startOutLineWidth = 1f;

    private void Start()
    {
        SetOutlineWidth(startOutLineWidth);
    }

    public  void SetOutlineWidth(float outLineWidth)
    {
        if (transform.GetComponent<Renderer>())
            transform.GetComponent<Renderer>().material.SetFloat("_OutLineWidth" , outLineWidth);
        
        if(transform.GetComponent<SkinnedMeshRenderer>())
            transform.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutLineWidth" , outLineWidth);
        
        if(transform.GetComponent<MeshRenderer>())
            transform.GetComponent<MeshRenderer>().material.SetFloat("_OutLineWidth" , outLineWidth);
        
        foreach (Renderer r in transform.GetComponentsInChildren<Renderer>())
            r.material.SetFloat("_OutLineWidth" , outLineWidth);
        
        foreach (Renderer r in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
            r.material.SetFloat("_OutLineWidth" , outLineWidth);
        
        foreach (Renderer r in transform.GetComponentsInChildren<MeshRenderer>())
            r.material.SetFloat("_OutLineWidth" , outLineWidth);
    }
    
}
