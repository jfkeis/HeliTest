using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeliController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody _body;
    public float ForwardForce = 10000f;
    public float TurnForce = 1000f;
    public float LiftForce = 10000f;
    public float MaxSpeed = 1000f;
    public float Speed;
    public float AngularSpeed;
    public float RotForce = 100f;
    public float xAngle;
    public float yAngle;
    public float zAngle;
    public float maxAngle = 80;
    public float correctionForce = 1000f;

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
        print(xAngle);
        //print(yAngle);
        //print(zAngle);

        Speed = _body.velocity.magnitude;

        Force = new Vector3(0, _lift, _thrust);
        Torque = new Vector3(0, _roll, 0);
        _body.AddRelativeForce(Force);
        _body.AddRelativeTorque(Torque);

        if (xAngle != 0)
        {
            if (xAngle <= 180)
            {
                LevelerX = new Vector3(-1 * correctionForce, 0, 0);
                _body.AddRelativeTorque(LevelerX);
            }
            else if (xAngle > 180)
            {
                LevelerX = new Vector3(correctionForce, 0, 0);
                _body.AddRelativeTorque(LevelerX);
            }
        }

        // now do for z


        //if (xAngle > maxAngle && xAngle < (360 - maxAngle))  // check if helicopter tilted forwards or backwards
        //{
        //    // if forwards
        //    if (xAngle <= 180)
        //    {
        //        // push nose upward
        //    }
        //    // if backwards
        //    else if (xAngle > 180)
        //    {
        //        // push nose downward
        //    }
        //    else
        //    {
        //        // do nothing
        //    }
        //    //transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
        //}


        //transform.Translate(_roll * Time.deltaTime, _lift * Time.deltaTime, _thrust * Time.deltaTime);

    }

}
