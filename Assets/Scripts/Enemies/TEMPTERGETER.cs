using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TEMPTERGETER : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        this.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
    }

    void Update()
    {
        
    }
}
