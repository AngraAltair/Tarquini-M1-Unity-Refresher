using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class projectileVelocity : MonoBehaviour
{
    public Vector3 initialVelocity = new Vector3(0, 50f, 10f);
    public Vector3 extraAcceleration = Vector3.zero;
    public bool includeGravity = true;
    public bool useRigidbody = true;
    public float mass = 1f;

    private Vector3 accumulatedForces = Vector3.zero;
    private Rigidbody rb;
    private Vector3 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocity = initialVelocity;

        if (rb != null)
        {
            mass = rb.mass;
            if (useRigidbody)
            {
                rb.velocity = velocity;
            }
        }
    }

    public void SetInitialVelocity(Vector3 worldVelocity)
    {
        initialVelocity = worldVelocity;
        velocity = worldVelocity;
        if (rb != null && useRigidbody)
        {
            rb.velocity = velocity;
        }
    }

    public void AddForce(Vector3 force)
    {
        accumulatedForces += force;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        Vector3 accFromForces = accumulatedForces / Mathf.Max(0.00001f, mass);
        Vector3 gravity = includeGravity ? Physics.gravity : Vector3.zero;
        Vector3 totalAcceleration = extraAcceleration + accFromForces + gravity;
        velocity += totalAcceleration * dt;

        if (rb != null && useRigidbody)
        {
            rb.velocity = velocity;
        }
        else
        {
            transform.position += velocity * dt;
        }

        accumulatedForces = Vector3.zero;
    }

    public bool NetForceIsZero(float tolerance = 1e-6f)
    {
        return accumulatedForces.sqrMagnitude <= tolerance * tolerance && extraAcceleration.sqrMagnitude <= tolerance * tolerance && (!includeGravity || Physics.gravity.sqrMagnitude <= tolerance * tolerance);
    }

    void OnCollisionEnter(Collision collision)
    {
    }
}

