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
    public float maxAngle = 80;
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

        //print(_thrust);
        //print(xAngle);
        //print(yAngle);
        //print(zAngle);

        Speed = _body.velocity;
        AngularSpeed = _body.angularVelocity;

        Force = new Vector3(0, _lift, _thrust);
        Torque = new Vector3(0, _roll, 0);
        _body.AddRelativeForce(Force);
        _body.AddRelativeTorque(Torque);

        // add lift force to combat gravity when flying when 1m or greater above the ground or base it off of altitue
        // mass * gravity
        //if (_body.Position.y > 1)
        //{
            _body.AddRelativeForce(0, 200 * 9.79f, 0);
        //}

        // script to set pitch and roll angles based on forward speed and angular speed instead of correction using quat

        // nose up and down correction  
        if (xAngle != 0)
        {
            if (xAngle <= 180 && xAngle > 0.5)
            {
                LevelerX = new Vector3(-1 * (xAngle/180) * correctionForce, 0, 0);
                _body.AddRelativeTorque(LevelerX);
            }
            else if (xAngle > 180 && xAngle < 359.5)
            {
                LevelerX = new Vector3((xAngle/180) * correctionForce, 0, 0);
                _body.AddRelativeTorque(LevelerX);
            }
        }

        // roll correciton
        if (zAngle != 0)
        {
            if (zAngle <= 180 && zAngle > 0.5)
            {
                LevelerZ = new Vector3(0, 0, -1 * (zAngle/90) * correctionForce);
                _body.AddRelativeTorque(LevelerZ);
            }
            else if (zAngle > 180 && zAngle < 359.5)
            {
                LevelerZ = new Vector3(0, 0, (zAngle/90) * correctionForce);
                _body.AddRelativeTorque(LevelerZ);
            }
        }

        // apply force to rotors 

        //transform.Translate(_roll * Time.deltaTime, _lift * Time.deltaTime, _thrust * Time.deltaTime);

    }

}
