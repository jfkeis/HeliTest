using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator
{
    private Transform _transform;
    private Vector3 _rotationAxis;
    private float _maxDegreesPerSecond;

    public Rotator(Transform _newTransform, Vector3 _newRotationAxis, float _maxRPM)
    {
        _transform = _newTransform;
        _rotationAxis = _newRotationAxis;
        _maxDegreesPerSecond = (_maxRPM / 60F) * 360F;
    }

    public void Rotate(float _percentMaxSpeed)
    {
        _transform.Rotate(_rotationAxis * (Time.deltaTime * (_maxDegreesPerSecond * _percentMaxSpeed)));
    }
}
