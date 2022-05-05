using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlownessTower : DebuffTower
{
    public float slowModifier;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        other.GetComponent<SplineFinding>().speedModifier = other.GetComponent<SplineFinding>().speedModifier * slowModifier;
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<SplineFinding>().speedModifier = 0.1f;
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }
}
