using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float EnemyDMG;
    [SerializeField] GameObject HealthTarget;

    private void Start()
    {
        HealthTarget = GameObject.FindWithTag("Shuttle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthTarget.GetComponent<ShuttleScript>().CurrentHealth -= EnemyDMG;
        Debug.Log("Dealt " + EnemyDMG + " damage!");
        Destroy(gameObject);
    }
}

