using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TowerStats))]
[RequireComponent(typeof(AudioSource))]

public class MortarClass : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] float innerRange;
    [SerializeField] float outerRange;
    [SerializeField] float AOERange;
    [SerializeField] float Damage;
    [SerializeField] float rateOfFire;
    [SerializeField] float timeToImpact;
    [SerializeField] GameObject projectile;

    [Header("FX")]
    [SerializeField] ParticleSystem[] MuzzleFlash;
    public Transform target; //[HideInInspector] 

    bool allowFire = true;
    bool close, far, weak, strong;
    List<Transform> enemies = new List<Transform>();

    private void Awake()
    {
        CloseFire();
    }

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
        //clears current enemies
        enemies.Clear();
        //loops over all enemies and adds them to list, to be replaced later on
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = enemy.gameObject.transform;

            if (enemies.Contains(enemyTransform) == false)
            {
                enemies.Add(enemyTransform);
            }
        }
        //calculates nearest enemy and sets to target
        Transform bestTarget = null;
        float closeestDistanceSqr = Mathf.Infinity;
        float furthestDistanceSqr = 0;
        Vector3 currentPosition = transform.position;

        float lowestHealth = Mathf.Infinity;
        float highestHealth = 0;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 dirToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = dirToTarget.sqrMagnitude;
            float health = potentialTarget.GetComponent<EnemyStats>().health;

            if (close)
            {
                if (dSqrToTarget < closeestDistanceSqr)
                {
                    closeestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            if (far)
            {
                if (dSqrToTarget >= furthestDistanceSqr)
                {
                    furthestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            if (weak)
            {
                if (health < lowestHealth)
                {
                    lowestHealth = health;
                    bestTarget = potentialTarget;
                }
            }

            if (strong)
            {
                if (health >= highestHealth)
                {
                    highestHealth = health;
                    bestTarget = potentialTarget;
                }
            }
            target = bestTarget;
        }

    }

    private void IsAimed()
    {
        if ((transform.position - target.position).magnitude > innerRange && (transform.position - target.position).magnitude < outerRange && allowFire && target.gameObject.activeInHierarchy && target.GetComponent<EnemyStats>().health > 0)
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
        yield return new WaitForSeconds(timeToImpact);
        GameObject AOE = Instantiate(projectile, TargetPos, transform.rotation);
        AOE.GetComponent<RangedAOE>().damage = Damage;
        AOE.GetComponent<RangedAOE>().range = AOERange;
        yield return new WaitForSeconds(rateOfFire);
        allowFire = true;
    }

    public void CloseFire()
    {
        close = true;
        far = false;
        weak = false;
        strong = false;
    }
    public void FarFire()
    {
        close = false;
        far = true;
        weak = false;
        strong = false;
    }
    public void WeakFire()
    {
        close = false;
        far = false;
        weak = true;
        strong = false;
    }
    public void StrongFire()
    {
        close = false;
        far = false;
        weak = false;
        strong = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerRange);
        Gizmos.DrawWireSphere(transform.position, outerRange);
    }
}
