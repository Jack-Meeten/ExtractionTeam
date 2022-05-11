using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPDelete : MonoBehaviour
{
    void Start()
    {
        BuildingPlacer _bp = FindObjectOfType<BuildingPlacer>();
        StartCoroutine(OnDelete(_bp));
    }
    IEnumerator OnDelete(BuildingPlacer bP)
    {
        yield return new WaitForSeconds(.1f);
        if (!bP.beginPlace ^ gameObject != bP.towerBlueprint)
        {
            Destroy(gameObject);
        }
        StartCoroutine(OnDelete(bP));
    }
}
