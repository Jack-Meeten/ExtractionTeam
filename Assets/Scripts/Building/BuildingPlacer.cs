using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    private Grid grid;
    [HideInInspector] public GameObject tower;
    public GameObject towerBlueprint;
    [SerializeField] BuildManager manager;
    [SerializeField] BuildMenu bM;

    private Vector3 drawPos;

    public bool beginPlace = false;

    void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }


    void Update()
    {
        if (beginPlace)
        {
            //add prefab to mouse position
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitInfo) && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (hitInfo.transform.tag == "Placeable") PlaceCubeNear(hitInfo.point);
                }

                towerBlueprint.transform.position = new Vector3(grid.GetPointOnGrid(hitInfo.point).x, grid.GetPointOnGrid(hitInfo.point).y + 0.5f, grid.GetPointOnGrid(hitInfo.point).z);
            }
        }

        CheckBuildMode();
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetPointOnGrid(clickPoint);
        drawPos = finalPosition;

        //makes box and checks whats in it
        Collider[] hitColliders = Physics.OverlapBox(new Vector3(finalPosition.x, finalPosition.y - 5, finalPosition.z), transform.localScale / 2, Quaternion.identity);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Increase the number of Colliders in the array
            i++;
            Debug.Log(hitColliders.Length);
        }
        if (hitColliders.Length == 0)
        {
            if (manager.opalium >= tower.GetComponent<TowerStats>().opalium && manager.vinculum >= tower.GetComponent<TowerStats>().vinculum)
            {
                Instantiate(tower, new Vector3(finalPosition.x, finalPosition.y + 0.5f, finalPosition.z), transform.rotation);//+ 0.225f
                manager.opalium -= tower.GetComponent<TowerStats>().opalium;
                manager.vinculum -= tower.GetComponent<TowerStats>().vinculum;
            }
        }
        else
        {
            foreach (var item in hitColliders)
            {
                //if not a tower spawn
                if (item.tag != "Tower" || item.tag != "Resource")
                {
                    if (manager.opalium >= tower.GetComponent<TowerStats>().opalium && manager.vinculum >= tower.GetComponent<TowerStats>().vinculum)
                    {
                        Instantiate(tower, new Vector3(finalPosition.x, finalPosition.y + 0.5f, finalPosition.z), transform.rotation);//+ 0.225f
                        manager.opalium -= tower.GetComponent<TowerStats>().opalium;
                        manager.vinculum -= tower.GetComponent<TowerStats>().vinculum;
                        PlaceSwitch();
                    }
                }
            }
        }
    }

    void CheckBuildMode()
    {
        if (!bM.BuildingMenu)
        {
            Destroy(towerBlueprint);
            beginPlace = false;
        }
        if (!beginPlace)
        {
            Destroy(towerBlueprint);
        }
    }
    void PlaceSwitch()
    {
        if (beginPlace) beginPlace = false;
        else beginPlace = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(drawPos, transform.localScale);
    }
}