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

    Vector3 Force;
    Vector3 Torque;
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
        print(_thrust);
        Force = new Vector3(0, _lift, _thrust);
        Torque = new Vector3(0, _roll, 0);
;        Speed = _body.velocity.magnitude;
        //transform.Translate(_roll * Time.deltaTime, _lift * Time.deltaTime, _thrust * Time.deltaTime);
        _body.AddRelativeForce(Force);
        _body.AddRelativeTorque(Torque);
    }

}
