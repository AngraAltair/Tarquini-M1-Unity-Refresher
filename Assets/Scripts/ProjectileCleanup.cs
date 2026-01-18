using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCleanup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // if (gameObject.GetComponentsInChildren<Renderer>().Length > 5)
        // {
        //     // foreach(Transform child in transform)
        //     // {
        //     //     if (child.gameObject.tag == "Projectile")
        //     //     {
        //     //         Destroy(child.gameObject);
        //     //     }
        //     // }
        //     if (gameObject.GetComp)
        // }

        if (transform.GetComponentsInChildren<Renderer>().Length > 5)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
