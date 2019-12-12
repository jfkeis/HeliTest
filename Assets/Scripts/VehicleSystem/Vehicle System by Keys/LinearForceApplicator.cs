using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearForceApplicator : ForceApplicator
{
    public LinearForceApplicator(Rigidbody _rigidbody, float _maxForce, Vector3 _forceAxis) : base(_rigidbody, _maxForce, _forceAxis)
    {
        //
    }

    public override void ApplyForcePercentage(float _percent)
    {
        _rigidbody.AddRelativeForce(CalculateForce(_percent));
    }
}
