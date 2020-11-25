using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PicoGrabbleManager : MonoBehaviour
{
    public static PicoGrabbleManager Instance;
    [SerializeField] GameObject _controller0, _controller1;
    [HideInInspector] public PicoGrabble leftHand;
    [HideInInspector] public PicoGrabble rightHand;
    
    public GameObject Controller0 => _controller0;

    public GameObject Controller1 => _controller1;
    
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
        leftHand = _controller0.GetComponent<PicoGrabble>();
        rightHand = _controller1.GetComponent<PicoGrabble>();
    }
}
