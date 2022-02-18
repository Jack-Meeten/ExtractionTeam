using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClass : MonoBehaviour
{
    public GameObject target;
    public int Damage;
    private List<Transform> enemies = new List<Transform>();
    void Start()
    {
        InvokeRepeating("FindAllEnemies", 0, 2);
    }

    void Update()
    {
        
    }

    Transform FindAllEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = enemy.gameObject.transform;
            if (enemies.Contains(enemyTransform))
            {
                break;
            }
            enemies.Add(enemyTransform);
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
            Transform close = bestTarget;
            return bestTarget;
        }

    }
}
