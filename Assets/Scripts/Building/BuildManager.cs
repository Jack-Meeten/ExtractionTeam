using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public int opalium;
    public int vinculum;
    public BuildingPlacer placer;
    public BuildManager manager;
    public GameObject grid;

    [SerializeField] TextMeshProUGUI OpaliumText;
    [SerializeField] TextMeshProUGUI VinculumText;

    void Update()
    {
        // Update the resources number on the UI
        OpaliumText.text = opalium.ToString();
        VinculumText.text = vinculum.ToString();
        if (!placer.beginPlace)
        {
            grid.SetActive(false);
        }
    }

    public void selectTurret(GameObject turret)
    {
        if (turret.GetComponent<TowerStats>().opalium <= manager.opalium && turret.GetComponent<TowerStats>().vinculum <= manager.vinculum)
        {
            placer.tower = turret;

            if (placer.beginPlace)
            {
                placer.beginPlace = false;
                grid.SetActive(false);
            }
            else
            {
                placer.beginPlace = true;
                grid.SetActive(true);
            }
        }
    }
    public void bpTurret(GameObject blueprintTurret)
    {
        GameObject bP = Instantiate(blueprintTurret, transform.position, transform.rotation);
        placer.towerBlueprint = bP;
    }
}
