using System;
using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class Teleport : MonoBehaviour , IMovement
{
        LineRenderer laser;
        int laserSteps = 20;
        float laserSegmentDistance = 1f, dropPerSegment = .1f;
        Transform head;
        int collisionLayer;
        Vector3 targetPos;
        int _controller;

        private LineRenderer Laser
        {
            get => laser;
            set => laser = value;
        }

        private int LaserSteps
        {
            get => laserSteps;
            set => laserSteps = value;
        }

        private float LaserSegmentDistance
        {
            get => laserSegmentDistance;
            set => laserSegmentDistance = value;
        }

        private float DropPerSegment
        {
            get => dropPerSegment;
            set => dropPerSegment = value;
        }

        private Transform Head
        {
            get => head;
            set => head = value;
        }
        
        private int CollisionLayer
        {
            get => collisionLayer;
            set => collisionLayer = value;
        }

        private Vector3 TargetPos
        {
            get => targetPos;
            set => targetPos = value;
        }

        private bool TargetAcquired
        {
            get => targetAcquired;
            set => targetAcquired = value;
        }

        

        bool targetAcquired = false;

        private void Awake()
        {
            laser.positionCount = laserSteps;
        }

        public void Initialize(int controller)
        {
            if (controller == 0)
            {
                Laser = Movement.Instance.laserLeftController;
            }
            else if( controller == 1)
            {
                Laser = Movement.Instance.laserRightController;
            }
            else
            {
                throw new  Exception("You need pass ot function 0 or 1 (0 left hand) : (1 right hand) ");
                return;
            }

            _controller = controller;
            LaserSteps = Movement.Instance.laserSteps;
            LaserSegmentDistance = Movement.Instance.laserSegmentDistance;
            DropPerSegment = Movement.Instance.dropPerSegment;
            Head = Movement.Instance.head;
            CollisionLayer = Movement.Instance.collisionLayer;
        }
        

        public void Tick()
        {
            if (Pvr_UnitySDKAPI.Controller.UPvr_GetAxis2D(_controller).y > .8f || 
                (_controller == 0 && Input.GetKey(KeyCode.C)) ||(_controller == 1 && Input.GetKey(KeyCode.V)))
            {
                TryToGetTeleportTarget();
            }
            else if (targetAcquired == true && Pvr_UnitySDKAPI.Controller.UPvr_GetAxis2D(1).y  <.2f || 
                     (_controller == 0 && Input.GetKeyUp(KeyCode.C)) || (_controller == 1 && Input.GetKeyUp(KeyCode.V)))
            {
                TryTeleport();
            }
            else if (targetAcquired == false && Pvr_UnitySDKAPI.Controller.UPvr_GetAxis2D(1).y  < .2f)
            {
                ResetLaser();
            }
        }

        private void TryToGetTeleportTarget()
        {
            targetAcquired = false;
            RaycastHit hit;
            Vector3 origin = _controller == 0 ? Pvr_Controller.Instance.controller0.transform.position: Pvr_Controller.Instance.controller1.transform.position;
            laser.SetPosition(0, origin);

            for (int i = 0; i < laserSteps-1; i++)
            {
                Vector3 forward  = _controller == 0 ? Pvr_Controller.Instance.controller0.transform.forward: Pvr_Controller.Instance.controller1.transform.forward;
                Vector3 offset = (forward+ (Vector3.down * dropPerSegment * i)).normalized * laserSegmentDistance;

                if (Physics.Raycast(origin, offset, out hit, laserSegmentDistance))
                {
                    for(int j = i+1; j < laser.positionCount; j++)
                    {
                        laser.SetPosition(j, hit.point);
                    }

                    if (hit.transform.gameObject.layer == collisionLayer)
                    {
                        laser.startColor = laser.endColor = Color.green;
                        targetPos = hit.point;
                        targetAcquired = true;
                        return;
                    }
                    else
                    {
                        laser.startColor = laser.endColor = Color.red;
                        return;
                    }
                   
                }
                else
                {
                    laser.SetPosition(i + 1, origin + offset);
                    origin += offset;
                }
            }

            laser.startColor = laser.endColor = Color.red;
        }

        private void TryTeleport()
        {
            targetAcquired = false;
            ResetLaser();
            Debug.Log(targetPos + " " +  head.transform.position );
            Vector3 offset = new Vector3(targetPos.x - head.transform.position.x, (targetPos.y - head.position.y) + 2, targetPos.z - head.transform.position.z);
            head.position += offset;
            
        }

        private void ResetLaser()
        {
            for (int i = 0; i < laser.positionCount; i++)
            {
                laser.SetPosition(i, Vector3.zero);
            }
        }
    
}


