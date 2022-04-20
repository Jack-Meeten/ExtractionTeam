using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float baseHealth = 10;
    public float health = 10;


    void Update()
    {
        if (health <= 0)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(transform.position.x, -20, transform.position.z);

            gameObject.SetActive(false);
        } 
    }
}
