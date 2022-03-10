using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public int opalium;
    public int vinculum;
    public BuildingPlacer placer;

    /*[SerializeField] bool cannonTurret;
    [SerializeField] bool pDTurret;
    [SerializeField] bool debuffTurret;
    [SerializeField] bool mortarTurret;
    [SerializeField] bool gatlingTurret;
    [SerializeField] bool slownessTurret;
    [SerializeField] bool poisonTurret;
    [SerializeField] bool laserPDTurret;
    [SerializeField] bool damageDebuffTurret;
    [SerializeField] bool artilleryTurret;
    [SerializeField] bool railgunTurret;
    [SerializeField] bool stunTurret;
    [SerializeField] bool missileTurret;*/

    public void click(GameObject turret)
    {
        placer.beginPlace = true;
        placer.tower = turret;
        if (placer.beginPlace) placer.beginPlace = false;
        else placer.beginPlace = true;
    }

}
