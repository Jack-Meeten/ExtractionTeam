using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : TowerClass
{
    public Collider aOE;
    private List<GameObject> _enemy = new List<GameObject>();
    private bool canTick = true;
    public float quickTickDamage;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (canTick)
        {
            StartCoroutine(tickDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemy.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        _enemy.Remove(other.gameObject);
    }

    IEnumerator tickDamage()
    {
        canTick = false;
        foreach (GameObject obj in _enemy)
        {
            obj.GetComponent<EnemyStats>().health -= quickTickDamage;
            if (obj.GetComponent<EnemyStats>().health <= 0)
            {
                _enemy.Remove(obj);
            }
        }

        yield return new WaitForSeconds(Time.deltaTime / rateOfFire);
        canTick = true;
    }
}
