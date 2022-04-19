using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : DebuffTower
{
    [SerializeField] float tickDamage;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().health -= tickDamage;
        }
    }
}
