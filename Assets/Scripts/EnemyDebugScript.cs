using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebugScript : MonoBehaviour
{
    public Transform enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            int enemyCount = enemyParent.childCount;
            for (int i = 0; i < enemyCount; i++)
            {
                Transform enemy = enemyParent.GetChild(i);
                Destroy(enemy.gameObject);
            }
        }
    }
}
