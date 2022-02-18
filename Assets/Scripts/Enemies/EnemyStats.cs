using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health = 10;


    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        } 
    }
}
