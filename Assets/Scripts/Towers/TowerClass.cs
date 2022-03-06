using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerClass : MonoBehaviour
{
    public float Damage;
    public float rateOfFire;
    public float range;
    public ParticleSystem MuzzleFlash;

    [SerializeField] List<Transform> enemies = new List<Transform>();
    public Transform target;

    public GameObject baseMountPoint;
    public GameObject barrelMountPoint;
    public float turnSpeed = 90f;

    public bool allowFire = true;

    public bool isAnimated;
    public Animator _animation;

    public Vector3 offset;

    protected virtual void Update()
    {
        FindAllEnemies();
        if (target != null)
        {
            if ((transform.position - target.transform.position).magnitude < range)
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
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 dirToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = dirToTarget.sqrMagnitude;
            if (dSqrToTarget < closeestDistanceSqr)
            {
                closeestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
            target = bestTarget;
        }

    }

    public void BaseRotate()
    {
        var los = target.position - baseMountPoint.transform.position;//gets position difference
        los.y = 0;
        var rotation = Quaternion.LookRotation(los + offset);//get look rotation
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
            if (allowFire && (transform.position - target.transform.position).magnitude < range && target.gameObject.activeInHierarchy)
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
        MuzzleFlash.Play();
        yield return new WaitForSeconds(Time.deltaTime / rateOfFire);
        allowFire = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
