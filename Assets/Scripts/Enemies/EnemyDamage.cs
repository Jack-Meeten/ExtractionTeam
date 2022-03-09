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
        if (collision.gameObject == HealthTarget)
        {
            HealthTarget.GetComponent<ShuttleScript>().CurrentHealth -= EnemyDMG;
            gameObject.SetActive(false);
        }
    }
}

