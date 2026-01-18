using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float mass = 1f;
    public bool includeGravity = true;
    public List<Vector3> forceVectorList = new List<Vector3>();
    private Vector3 netForce = Vector3.zero;

    public void AddForce(Vector3 f)
    {
        forceVectorList.Add(f);
    }

    public void SetInitialVelocity(Vector3 worldVelocity)
    {
        velocity = worldVelocity;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        netForce = Vector3.zero;
        for (int i = 0; i < forceVectorList.Count; i++)
        {
            netForce += forceVectorList[i];
        }

        if (includeGravity)
            netForce += Physics.gravity * mass;

        if (netForce.sqrMagnitude > 1e-8f)
        {
            Debug.LogError($"SimpleProjectile: net force detected = {netForce} (will still integrate v = u + a*t)", this);
        }

        Vector3 a = netForce / Mathf.Max(1e-6f, mass);
        velocity += a * dt;
        transform.position += velocity * dt;
        forceVectorList.Clear();
    }
}
