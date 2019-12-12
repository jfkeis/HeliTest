using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalForceApplicator : ForceApplicator
{
    public RotationalForceApplicator(Rigidbody _rigidbody, float _maxForce, float _maxRPM, Vector3 _forceAxis) : base(_rigidbody, _maxForce, _forceAxis)
    {
        // converts _RPM to degrees/sec and then to rads/sec
        _rigidbody.maxAngularVelocity = ((_maxRPM / 60F) * 360F) / 57.29577951308F;
    }

    public float PercentMaxRPM
    {
        get
        {
            return _rigidbody.angularVelocity.magnitude / _rigidbody.maxAngularVelocity;
        }
    }

    public override void ApplyForcePercentage(float _percent)
    {
        _rigidbody.AddRelativeTorque(CalculateForce(_percent));
    }
}
