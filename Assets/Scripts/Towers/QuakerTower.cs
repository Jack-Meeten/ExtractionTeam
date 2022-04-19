using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakerTower : DebuffTower
{
    List<GameObject> inRange = new List<GameObject>();

    bool allowFire = true;

    [SerializeField] float rateOfFire;
    [SerializeField] float damagePerTick;

    private void FixedUpdate()
    {
        if (allowFire)
        {
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        allowFire = false;
        foreach (GameObject enemy in inRange)
        {
            enemy.GetComponent<EnemyStats>().health -= damagePerTick;
        }
        yield return new WaitForSeconds(Time.deltaTime / rateOfFire);
        allowFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            inRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inRange.Remove(other.gameObject);
    }
}
