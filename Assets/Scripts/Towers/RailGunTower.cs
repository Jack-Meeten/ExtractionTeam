using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunTower : TowerClass
{

    void Update()//protected override 
    {
        //base.Update();
        if (base.target != null)
        {
            Debug.DrawLine(transform.position, base.target.position, Color.cyan);
        }
    }
}
