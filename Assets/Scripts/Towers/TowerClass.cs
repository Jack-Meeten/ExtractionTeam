using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(TowerStats))]
[RequireComponent(typeof(AudioSource))]

public class TowerClass : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] float Damage;
    public float rateOfFire;
    [SerializeField] float range;
    [SerializeField] ParticleSystem[] MuzzleFlash;
    [SerializeField] AudioClip shootSound;

    [SerializeField] List<Transform> enemies = new List<Transform>();
    public Transform target;//[HideInInspector]

    [SerializeField] GameObject baseMountPoint;
    [SerializeField] GameObject barrelMountPoint;
    [SerializeField] float turnSpeed = 90f;

    [SerializeField] bool isAnimated;
    [SerializeField] Animator _animation;

    bool close, far, weak, strong;
    bool allowFire = true;

    private void Awake()
    {
        CloseFire();
    }

    void FixedUpdate()
    {
        FindAllEnemies();
        if (target != null)
        {
            if ((transform.position - target.transform.position).magnitude < range && target.GetComponent<EnemyStats>().health > 0)
            {
                //points turret at target
                BaseRotate();
                BarrelRotate();
            }
            IsAimed();
        }
        if (isAnimated && !allowFire)
        {
            _animation.SetBool("Shoot", true);
        }
        if (isAnimated && allowFire)
        {
            _animation.SetBool("Shoot", false);
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
                if ((transform.position - potentialTarget.position).magnitude < range && dSqrToTarget < closeestDistanceSqr)
                {
                    closeestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            if (far)
            {
                if ((transform.position - potentialTarget.position).magnitude < range && dSqrToTarget >= furthestDistanceSqr)
                {
                    furthestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            if (weak)
            {
                if ((transform.position - potentialTarget.position).magnitude < range && health < lowestHealth)
                {
                    lowestHealth = health;
                    bestTarget = potentialTarget;
                }
            }

            if (strong)
            {
                if ((transform.position - potentialTarget.position).magnitude < range && health > highestHealth)
                {
                    highestHealth = health;
                    bestTarget = potentialTarget;
                }
            }
            target = bestTarget;
        }

    }

    public void BaseRotate()
    {
        var los = target.position - baseMountPoint.transform.position;//gets position difference
        los.y = 0;
        var rotation = Quaternion.LookRotation(los);//get look rotation
        baseMountPoint.transform.rotation = Quaternion.Slerp(baseMountPoint.transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
    public void BarrelRotate()
    {
        var los = target.position - barrelMountPoint.transform.position;//gets position difference
        los.x = 0;
        var rotation = Quaternion.LookRotation(los);//get look rotation
        barrelMountPoint.transform.rotation = Quaternion.Slerp(barrelMountPoint.transform.rotation, rotation, Time.deltaTime * turnSpeed);


        var los2 = target.position - barrelMountPoint.transform.position;//gets position difference
        los.z = 0;
        var rotation2 = Quaternion.LookRotation(los2);//get look rotation
        barrelMountPoint.transform.rotation = Quaternion.Slerp(barrelMountPoint.transform.rotation, rotation2, Time.deltaTime * turnSpeed);
    }

    public void IsAimed()
    {
        //raycast to target
        var ray = new Ray(barrelMountPoint.transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.DrawRay(transform.position, transform.position - target.transform.position);
            //checks if can fire, in range and target active
            if (allowFire && (target.gameObject.activeInHierarchy && target.GetComponent<EnemyStats>().health > 0))
            {
                Debug.DrawLine(barrelMountPoint.transform.position, target.position, Color.red);
                StartCoroutine(Shoot());
            }
        }
    }
    public IEnumerator Shoot()
    {
        allowFire = false;
        target.GetComponent<EnemyStats>().health -= Damage;
        foreach (ParticleSystem flash in MuzzleFlash)
        {
            flash.Play();
        }
        GetComponent<AudioSource>().PlayOneShot(shootSound);
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
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
