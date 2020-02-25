using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeliController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public Transform _bodyTrans;
    public Transform _mainRotor;
    public Transform _tailRotor;
    public float ForwardForce = 10000f;
    public float TurnForce = 5000f;
    public float LiftForce = 10000f;
    public Vector3 Speed;
    public Vector3 AngularSpeed;
    private float RotForce;
    public float xAngle;
    public float yAngle;
    public float zAngle;
    public float maxAngle = 30;
    public float correctionForce = 10000f;
    private float maxRotForce = 25f;

    Vector3 Force;
    Vector3 Torque;
    //Vector3 LevelerX;
    //Vector3 LevelerY;
    //Vector3 LevelerZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float _thrust = Input.GetAxis("Vertical") * ForwardForce;
        float _roll = Input.GetAxis("Horizontal") * TurnForce;
        float _lift = Input.GetAxis("Fire3") * LiftForce;
        float xAngle = _bodyTrans.eulerAngles.x;
        float yAngle = _bodyTrans.eulerAngles.y;
        float zAngle = _bodyTrans.eulerAngles.z;

        // break down thrust into a y and z component
        float zThrust = Mathf.Cos(xAngle * Mathf.Deg2Rad) * _thrust;
        float yThrust = Mathf.Sin(xAngle * Mathf.Deg2Rad) * _thrust;

        Speed = rb.velocity;
        AngularSpeed = rb.angularVelocity;

        // add lift force to combat gravity when flying when 2m or greater above the ground or base it off of realtive altitude
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_bodyTrans.position, _bodyTrans.TransformDirection(Vector3.down), out RaycastHit hit, 1.6f))
        {   
            // if hit use gravity
            rb.useGravity = true;
            Debug.DrawRay(_bodyTrans.position, _bodyTrans.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            // apply force to rotors 
            _mainRotor.Rotate(0f, Mathf.Clamp(RotForce + Input.GetAxis("Fire3") * maxRotForce, maxRotForce / 2, maxRotForce), 0f);
            _tailRotor.Rotate(Mathf.Clamp(RotForce + Input.GetAxis("Fire3") * maxRotForce, maxRotForce / 2, maxRotForce), 0f, 0f);

            // disable additional forces when on the ground
            _roll = 0;
            yThrust = 0;
            zThrust = 0;
        }
        //else if (Physics.Raycast(_bodyTrans.position, _bodyTrans.TransformDirection(Vector3.down), out RaycastHit hit2, 2.6f))
        //{
        //    rb.useGravity = false;
        //    Debug.DrawRay(_bodyTrans.position, _bodyTrans.TransformDirection(Vector3.down) * hit2.distance, Color.cyan);
        //    // apply force to rotors 
        //    _mainRotor.Rotate(0f, maxRotForce, 0f);
        //    _tailRotor.Rotate(maxRotForce, 0f, 0f);

        //    // reduce additional forces when close to the ground
        //    _roll = _roll/2;
        //    yThrust = yThrust/2;
        //    zThrust = zThrust/2;
        //}
        else
        {
            // no hit so add opposite gravity
            //_body.AddRelativeForce(0, mass * 9.81f, 0);
            rb.useGravity = false;
            Debug.DrawRay(_bodyTrans.position, _bodyTrans.TransformDirection(Vector3.down) * 1000, Color.white);
            // apply force to rotors 
            _mainRotor.Rotate(0f, maxRotForce, 0f);
            _tailRotor.Rotate(maxRotForce, 0f, 0f);
        }

        Force = new Vector3(0, _lift + yThrust, zThrust);
        Torque = new Vector3(0, _roll, 0);
        rb.AddRelativeForce(Force);
        rb.AddRelativeTorque(Torque);

        // script to set pitch and roll angles based on forward speed and angular speed instead of correction using quat
        // set to ingore speeds less than .1
        var localVelocity = _bodyTrans.InverseTransformDirection(rb.velocity);
        float forwardSpeed = localVelocity.z;

        Vector3 localAngularVelocity = _bodyTrans.InverseTransformDirection(rb.angularVelocity);
        float turnVelocity = localAngularVelocity.y;

        _bodyTrans.eulerAngles = new Vector3(forwardSpeed, _bodyTrans.eulerAngles.y, -turnVelocity * maxRotForce);

        //// nose up and down correction  
        //if (xAngle != 0)
        //{
        //    if (xAngle <= 180 && xAngle > 0.5)
        //    {
        //        LevelerX = new Vector3(-1 * (xAngle/180) * correctionForce, 0, 0);
        //        _body.AddRelativeTorque(LevelerX);
        //    }
        //    else if (xAngle > 180 && xAngle < 359.5)
        //    {
        //        LevelerX = new Vector3((xAngle/180) * correctionForce, 0, 0);
        //        _body.AddRelativeTorque(LevelerX);
        //    }
        //}

        //// roll correction
        //if (zAngle != 0)
        //{
        //    if (zAngle <= 180 && zAngle > 0.5)
        //    {
        //        LevelerZ = new Vector3(0, 0, -1 * (zAngle/90) * correctionForce);
        //        _body.AddRelativeTorque(LevelerZ);
        //    }
        //    else if (zAngle > 180 && zAngle < 359.5)
        //    {
        //        LevelerZ = new Vector3(0, 0, (zAngle/90) * correctionForce);
        //        _body.AddRelativeTorque(LevelerZ);
        //    }
        //}

    }

}
