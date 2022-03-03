using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarClass : MonoBehaviour
{
    public float innerRange;
    public float outerRange;
    public float AOERange;
    public float Damage;
    public float rateOfFire;
    public float timeToImpact;
    public GameObject projectile;

    [Header("FX")]
    public ParticleSystem[] MuzzleFlash;

    private List<Transform> enemies = new List<Transform>();
    public Transform target;

    public bool allowFire = true;

    protected virtual void FixedUpdate()
    {
        FindAllEnemies();
        if (target != null)
        {
            IsAimed();
        }
    }

    public void FindAllEnemies()
    {
        enemies.Clear();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = enemy.gameObject.transform;

            if (enemies.Contains(enemyTransform) == false)
            {
                enemies.Add(enemyTransform);
            }
        }

        Transform bestTarget = null;
        float closeestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 dirToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = dirToTarget.sqrMagnitude;
            if ((transform.position - potentialTarget.position).magnitude > innerRange && (transform.position - potentialTarget.position).magnitude < outerRange)
            {
                if (dSqrToTarget < closeestDistanceSqr )
                {
                    closeestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            target = bestTarget;
        }

    }

    private void IsAimed()
    {
        if ((transform.position - target.position).magnitude > innerRange && (transform.position - target.position).magnitude < outerRange && allowFire && target.gameObject.activeInHierarchy)
        {
            Debug.DrawLine(transform.position, target.position, Color.red);
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        allowFire = false;
        Vector3 TargetPos = target.position;
        foreach (ParticleSystem flash in MuzzleFlash)
        {
            flash.Play();
        }
        yield return new WaitForSeconds(Time.deltaTime / timeToImpact);
        GameObject AOE = Instantiate(projectile, TargetPos, transform.rotation);
        AOE.GetComponent<RangedAOE>().damage = Damage;
        AOE.GetComponent<RangedAOE>().range = AOERange;
        yield return new WaitForSeconds(Time.deltaTime / rateOfFire);
        allowFire = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerRange);
        Gizmos.DrawWireSphere(transform.position, outerRange);
    }
}
