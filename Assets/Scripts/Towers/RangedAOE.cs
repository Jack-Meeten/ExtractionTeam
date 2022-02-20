using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAOE : MonoBehaviour
{
    public float damage;
    public float range;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = range;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().health -= damage;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
