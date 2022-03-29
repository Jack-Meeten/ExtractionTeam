using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMenu : MonoBehaviour
{
    public GameObject selectedTower;
    public BuildManager bM;
    public GameObject empty;
    private void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo) && Input.GetMouseButtonDown(0))
        {
            if (hitInfo.transform.tag == "Tower")
            {
                if (hitInfo.transform.GetComponent<TowerClass>() || hitInfo.transform.GetComponent<MortarClass>())
                {
                    selectedTower = hitInfo.transform.gameObject;
                    empty.SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            empty.SetActive(false);
        }
    }
    public void Sell()
    {
        bM.opalium += selectedTower.GetComponent<TowerStats>().opalium / 2;
        bM.vinculum += selectedTower.GetComponent<TowerStats>().vinculum / 2;
        Destroy(selectedTower);
        empty.SetActive(false);
    }
}
