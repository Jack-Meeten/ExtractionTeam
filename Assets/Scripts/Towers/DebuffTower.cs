using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerStats))]
[RequireComponent(typeof(AudioSource))]

public class DebuffTower : MonoBehaviour
{
    public SphereCollider _collider;
    public float radius;
    public AudioClip shootSound;

    void Start()
    {
        _collider.radius = radius;
    }
}
