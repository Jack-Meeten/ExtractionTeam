using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SplineMover : MonoBehaviour
{
    public Transform follower;

    void Update()
    {
        follower.position = transform.position; 
    }
}
