using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Teleport,
    HeadControll
}

public class Movement : MonoBehaviour
{
    /*
    [SerializeField] MovementType _movementType;
    public static Movement Instance;
    [Header("TeleportSetting")] public LineRenderer laserLeftController;
    public LineRenderer laserRightController;
    public int laserSteps = 20;
    public float laserSegmentDistance = 1f, dropPerSegment = .1f;
    public Transform head;
    public int collisionLayer;
    private IMovement leftController;
    private IMovement rightController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (_movementType == MovementType.Teleport)
        {
            leftController = new Teleport();
            leftController.Initialize(0);
            rightController = new Teleport();
            rightController.Initialize(1);
        }
    }


    private void Update()
    {
        UpdateTeleport(leftController, rightController);
    }

    void UpdateTeleport(IMovement leftController, IMovement rightController)
    {
        leftController.Tick();
        rightController.Tick();
    }
    */
}