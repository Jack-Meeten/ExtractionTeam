using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlownessTower : DebuffTower
{
    public float slowModifier;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<SplineFinding>().speedModifier = slowModifier;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<SplineFinding>().speedModifier = 0.2f;
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }
}
