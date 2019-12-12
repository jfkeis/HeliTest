using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplicator
{
    protected Rigidbody _rigidbody;
    protected Vector3 _forceAxis;

    public float MaxForce
    {
        get;
        protected set;
    }

    public ForceApplicator(Rigidbody _newRigidbody, float _newMaxforce, Vector3 _newForceAxis)
    {
        _rigidbody = _newRigidbody;
        _forceAxis = _newForceAxis;
        MaxForce = _newMaxforce;
    }

    public virtual void ApplyForcePercentage(float _percent)
    {
        // Apply forces from inherited classes
    }

    protected Vector3 CalculateForce(float _percentMaxForce)
    {
        return _forceAxis * (MaxForce * _percentMaxForce);
    }

}

