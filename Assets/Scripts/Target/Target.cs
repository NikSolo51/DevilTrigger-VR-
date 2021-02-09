using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip hitSound;

    private void Start()
    {
        if (GetComponent<AudioSource>() != null)
            Source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yeaah");
        if (other.GetComponent<Bullet>() != null)
        {
            if (Source != null && hitSound != null)
                Source.PlayOneShot(hitSound);
        }
            
    }
}
