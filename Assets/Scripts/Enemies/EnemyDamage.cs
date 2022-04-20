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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == HealthTarget)
        {
            HealthTarget.GetComponent<ShuttleScript>().CurrentHealth -= EnemyDMG;
            GetComponent<EnemyStats>().health = 0;
        }
    }
}

