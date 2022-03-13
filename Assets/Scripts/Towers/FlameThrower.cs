using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : TowerClass
{
    public Collider aOE;
    public List<GameObject> enemies;
    private void Start()
    {
        enemies = new List<GameObject>();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        enemies.Add(other.gameObject);
        Debug.Log("added");
    }
    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.gameObject);
        Debug.Log("removed");
    }
}
