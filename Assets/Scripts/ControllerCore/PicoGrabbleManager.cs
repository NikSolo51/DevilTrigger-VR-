using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PicoGrabbleManager : MonoBehaviour
{
    public static PicoGrabbleManager Instance;
    public GameObject controller0, controller1;
    [HideInInspector] public PicoGrabble leftHand;
    [HideInInspector] public PicoGrabble rightHand;
    
    public GameObject Controller0 => controller0;

    public GameObject Controller1 => controller1;
    
    public PicoGrabble LeftHand
    {
        get => leftHand;
    }

    public PicoGrabble RightHand
    {
        get => rightHand;
    }

     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

     void Start()
    {
        leftHand = controller0.GetComponent<PicoGrabble>();
        rightHand = controller1.GetComponent<PicoGrabble>();
    }
}
