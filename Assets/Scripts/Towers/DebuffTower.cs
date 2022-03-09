using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : MonoBehaviour
{
    public SphereCollider _collider;
    public float radius;
    void Start()
    {
        _collider.radius = radius;
    }
}
