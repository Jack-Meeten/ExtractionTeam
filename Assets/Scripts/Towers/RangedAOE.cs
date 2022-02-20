using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAOE : MonoBehaviour
{
    public float damage;
    public float range;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = range;
    }
}
