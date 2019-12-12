using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControls
{
    public float Pitch
    {
        get;
        protected set;
    }

    public float Roll
    {
        get;
        protected set;
    }

    public float Yaw
    {
        get;
        protected set;
    }

    public float Collective
    {
        get;
        protected set;
    }

    public float Throttle
    {
        get;
        protected set;
    }

    public void HandleInput()
    {
        Pitch = Input.GetAxis("Vertical");  // "Vertical"
        Roll = Input.GetAxis("Horizontal");  // "Horizontal"
        Yaw = Mathf.InverseLerp(-1F, 1F, Input.GetAxis("Fire3"));
        Collective = Mathf.InverseLerp(-1F, 1F, Input.GetAxis("Fire2"));
        Throttle = 1.0F;
    }
}
