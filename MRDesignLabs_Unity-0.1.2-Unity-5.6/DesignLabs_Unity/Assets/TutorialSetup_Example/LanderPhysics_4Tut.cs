//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HUX.Utility;
using UnityEngine;

namespace MRDL
{
    public class LanderPhysics_4Tut : Singleton<LanderPhysics_4Tut>
    {
        //public float Gravity = -0.1f;

        ////public float SafeCeilingCollisionDot = 0.5f;
        //public bool ApplyPhysics = true;
        //public bool UseGravity
        //{
        //    set
        //    {
        //        if (value)
        //        {
        //            gravityTarget = Vector3.up * Gravity;
        //        }
        //        else
        //        {
        //            Physics.gravity = Vector3.zero;
        //            gravityTarget = Vector3.zero;
        //        }
        //    }
        //}
        //public float ForwardDampening = 0.5f;
        //public float ForceMultiplier;

        //[SerializeField]
        //private Rigidbody mainRigidbody;
        //private Vector3 gravityTarget;
        //private float axisLeftRight;
        //private float axisFrontBack;
        //private float axisUpDown;

        //private void Start()
        //{
        //    OnGameplayStarted();
        //}

        //public Vector3 LanderPosition
        //{
        //    get
        //    {
        //        return transform.position;
        //    }
        //    set
        //    {
        //        transform.position = value;
        //    }
        //}

        //public Quaternion LanderRotation
        //{
        //    get
        //    {
        //        return transform.rotation;
        //    }
        //}

        //public float AxisLeftRight
        //{
        //    get
        //    {
        //        return Mathf.Clamp(axisLeftRight, -1f, 1f);
        //    }
        //}

        //public float AxisFrontBack
        //{
        //    get
        //    {
        //        return Mathf.Clamp(axisFrontBack, -1f, 1f);
        //    }
        //}

        //public float AxisUpDown
        //{
        //    get
        //    {
        //        return Mathf.Clamp(axisUpDown, -1f, 1f);
        //    }
        //}

        //private void OnGameplayStarted()
        //{
        //    Physics.gravity = Vector3.zero;
        //}

        //private void FixedUpdate()
        //{

        //    Physics.gravity = Vector3.Lerp(Physics.gravity, gravityTarget, Time.deltaTime);

        //    // If we're not applying physics make the rigidbody kinematic so we don't fly about
        //    mainRigidbody.isKinematic = !ApplyPhysics;
        //    // Reset angular velocity to zero every frame
        //    mainRigidbody.angularVelocity = Vector3.Lerp(mainRigidbody.angularVelocity, Vector3.zero, Time.deltaTime * ForwardDampening);

        //    // Make sure the rigidbody never falls asleep
        //    if (mainRigidbody.IsSleeping())
        //        mainRigidbody.WakeUp();

        //    // Apply up/down force
        //    if (LanderInput.Instance.TargetThrust != 0)
        //    {
        //        Vector3 upForce = Vector3.up * LanderInput.Instance.TargetThrust * ForceMultiplier;
        //        mainRigidbody.AddRelativeForce(upForce, ForceMode.VelocityChange);
        //    }
        //}
    }
}
