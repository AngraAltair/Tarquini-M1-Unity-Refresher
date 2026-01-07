using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spawnBall : MonoBehaviour
{
    public GameObject projectileSource;
    public Vector3 initialVelocity = new Vector3(0, 0, 10f);

    private GameObject prototype;

    void Awake()
    {
        if (projectileSource != null)
        {
            prototype = projectileSource;
        }
        else
        {
            try
            {
                prototype = GameObject.FindWithTag("projectile");
            }
            catch (UnityException)
            {
                prototype = null;
            }

            if (prototype == null)
            {
                prototype = GameObject.Find("Projectile");
            }

            if (prototype == null)
            {
                Debug.LogWarning("spawnBall: No projectile found. Assign a prefab to 'projectileSource' or create a GameObject named 'Projectile' or tag one with 'projectile'.");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (prototype == null)
        {
            Debug.LogWarning("spawnBall: Cannot spawn - prototype is null.");
            return;
        }

        GameObject go = Instantiate(prototype, transform.position, prototype.transform.rotation);
        go.transform.SetParent(null);

        try
        {
            if (string.IsNullOrEmpty(go.tag) || go.tag == "Untagged")
            {
                go.tag = "projectile";
            }
        }
        catch { }

        var sp = go.GetComponent<SimpleProjectile>();
        if (sp != null)
        {
            sp.SetInitialVelocity(transform.TransformDirection(initialVelocity));
            return;
        }

        var rb = go.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.TransformDirection(initialVelocity);
        }
    }
}
