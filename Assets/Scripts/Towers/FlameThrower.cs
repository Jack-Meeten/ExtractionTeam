using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : TowerClass
{
    public Collider aOE;
    public List<GameObject> _enemy = new List<GameObject>();

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemy.Add(other.gameObject);
        Debug.Log("added");
    }
    private void OnTriggerExit(Collider other)
    {
        _enemy.Remove(other.gameObject);
        Debug.Log("removed");
    }
}
