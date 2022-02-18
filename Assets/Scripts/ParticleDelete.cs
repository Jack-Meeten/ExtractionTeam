using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelete : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke("Delete", time);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
