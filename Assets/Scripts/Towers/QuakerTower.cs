using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakerTower : DebuffTower
{
    List<GameObject> inRange = new List<GameObject>();

    bool allowFire = true;

    [SerializeField] float rateOfFire;

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
            enemy.GetComponent<SplineFinding>().speedModifier = 0;
        }

        yield return new WaitForSeconds(rateOfFire);

        foreach (GameObject enemy in inRange)
        {
            enemy.GetComponent<SplineFinding>().speedModifier = 0.1f;
        }
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
