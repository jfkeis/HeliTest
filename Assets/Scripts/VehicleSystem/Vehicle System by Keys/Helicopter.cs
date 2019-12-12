using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{ 
    [Header("Components")]
    public Rigidbody _mainRotor;
    public Rigidbody _body;
    public Transform _tailRotor;

    private LinearForceApplicator _lift;
    private RotationalForceApplicator _mainRotorTorque;
    private RotationalForceApplicator _bodyTorque;
    private RotationalForceApplicator _bodyCounterTorque;
    private RotationalForceApplicator _pitchTorque;
    private RotationalForceApplicator _rollTorque;
    private HelicopterControls _controls;
    private Rotator _tailRotorRotator;

    // Start is called before the first frame update
    void Start()
    {
        _lift = new LinearForceApplicator(_body, 1000062, Vector3.up);
        _mainRotorTorque = new RotationalForceApplicator(_mainRotor, 3, 240, Vector3.down);
        _bodyTorque = new RotationalForceApplicator(_body, 25000, 120, Vector3.down);
        _bodyCounterTorque = new RotationalForceApplicator(_body, 50000, 120, Vector3.up);
        _pitchTorque = new RotationalForceApplicator(_body, 20000, 100, Vector3.right);
        _rollTorque = new RotationalForceApplicator(_body, 15000, 100, Vector3.back);
        _controls = new HelicopterControls();
        _tailRotorRotator = new Rotator(_tailRotor, Vector3.right, 500);

        // account for wobbling
        //_body.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _controls.HandleInput();
    }

    void FixedUpdate()
    {
        // Throttle
        _mainRotorTorque.ApplyForcePercentage(_controls.Throttle);
        _bodyTorque.ApplyForcePercentage(_mainRotorTorque.PercentMaxRPM);

        // Yaw 
        _tailRotorRotator.Rotate(_mainRotorTorque.PercentMaxRPM);
        _bodyCounterTorque.ApplyForcePercentage(_mainRotorTorque.PercentMaxRPM * _controls.Yaw);

        // Collective
        _lift.ApplyForcePercentage(_mainRotorTorque.PercentMaxRPM * _controls.Collective);

        // Cyclic
        _pitchTorque.ApplyForcePercentage(_mainRotorTorque.PercentMaxRPM * _controls.Pitch);
        _rollTorque.ApplyForcePercentage(_mainRotorTorque.PercentMaxRPM * _controls.Roll);
    }
}
