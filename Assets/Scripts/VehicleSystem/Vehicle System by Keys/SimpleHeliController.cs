using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeliController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody _body;
    public float ForwardForce = 10000f;
    public float TurnForce = 5000f;
    public float LiftForce = 10000f;
    public float MaxSpeed = 1000f;
    public Vector3 Speed;
    public Vector3 AngularSpeed;
    public float RotForce; 
    public float xAngle;
    public float yAngle;
    public float zAngle;
    public float maxAngle = 30;
    public float correctionForce = 10000f;

    Vector3 Force;
    Vector3 Torque;
    Vector3 LevelerX;
    Vector3 LevelerY;
    Vector3 LevelerZ;
    public Vector3 CenterOfMass = new Vector3(0, -1, 0);


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.centerOfMass = CenterOfMass;
    }

    void FixedUpdate()
    {
        float _thrust = Input.GetAxis("Vertical") * ForwardForce;
        float _roll = Input.GetAxis("Horizontal") * TurnForce;
        float _lift = Input.GetAxis("Fire3") * LiftForce;
        float xAngle = transform.eulerAngles.x;
        float yAngle = transform.eulerAngles.y;
        float zAngle = transform.eulerAngles.z;

        // break down thrust into a y and z component
        float zThrust = Mathf.Cos(xAngle * Mathf.Deg2Rad) * _thrust;
        float yThrust = Mathf.Sin(xAngle * Mathf.Deg2Rad) * _thrust;

        Speed = _body.velocity;
        AngularSpeed = _body.angularVelocity;

        Force = new Vector3(0, _lift + yThrust, zThrust);
        Torque = new Vector3(0, _roll, 0);
        _body.AddRelativeForce(Force);
        _body.AddRelativeTorque(Torque);

        // add lift force to combat gravity when flying when 2m or greater above the ground or base it off of realtive altitue
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2))
        {   
            // if hit use gravity
            _body.useGravity = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
        else
        {
            // no hit so add opposite gravity
            //_body.AddRelativeForce(0, mass * 9.81f, 0);
            _body.useGravity = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        }

        // script to set pitch and roll angles based on forward speed and angular speed instead of correction using quat
        // set to ingore speeds less than .1
        var localVelocity = transform.InverseTransformDirection(_body.velocity);
        float forwardSpeed = localVelocity.z;

        Vector3 localAngularVelocity = transform.InverseTransformDirection(_body.angularVelocity);
        float turnVelocity = localAngularVelocity.y;

        transform.eulerAngles = new Vector3(forwardSpeed, transform.eulerAngles.y, -turnVelocity * 20);

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

        //// roll correciton
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

        // apply force to rotors 

        //transform.Translate(_roll * Time.deltaTime, _lift * Time.deltaTime, _thrust * Time.deltaTime);

    }

}
