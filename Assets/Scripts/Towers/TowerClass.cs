using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerClass : MonoBehaviour
{
    public int Damage;
    public int rateOfFire;

    private List<Transform> enemies = new List<Transform>();
    [SerializeField] Transform target;

    public GameObject baseMountPoint;
    public GameObject barrelMountPoint;
    public float turnSpeed = 90f;

    private bool allowFire;
    void Update()
    {
        FindAllEnemies();

        if (target != null)
        {
            BaseRotate();
            BarrelRotate();
            IsAimed();
        }
    }

    public void FindAllEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = enemy.gameObject.transform;

            if (enemies.Contains(enemyTransform) == false)
            {
                enemies.Add(enemyTransform);
                Debug.Log("Added");
            }
        }

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

    private void BaseRotate()
    {
        var los = target.position - baseMountPoint.transform.position;//gets position difference
        los.y = 0;
        var rotation = Quaternion.LookRotation(los);//get look rotation
        baseMountPoint.transform.rotation = Quaternion.Slerp(baseMountPoint.transform.rotation, rotation, Time.deltaTime * turnSpeed);
    }
    private void BarrelRotate()
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

    private void IsAimed()
    {
        var ray = new Ray(barrelMountPoint.transform.position, -transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, 3))
        {
            Debug.DrawLine(barrelMountPoint.transform.position, target.position, Color.red);
            Shoot();
        }
    }
    private void Shoot()
    {
        allowFire = false;
        Debug.Log("fire");
    }
}
