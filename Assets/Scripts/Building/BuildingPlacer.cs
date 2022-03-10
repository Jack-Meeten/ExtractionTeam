using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private Grid grid;
    public GameObject tower;
    public BuildManager manager;

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

                if (Physics.Raycast(ray, out hitInfo) && Input.GetMouseButtonDown(0))
                {
                    if (hitInfo.transform.tag == "Placeable") PlaceCubeNear(hitInfo.point);
                }
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetPointOnGrid(clickPoint);

        drawPos = finalPosition;

        //makes box and checks whats in it
        Collider[] hitColliders = Physics.OverlapBox(finalPosition, transform.localScale / 2, Quaternion.identity);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
        }
        foreach (var item in hitColliders)
        {
            //if not a tower spawn
            if (item.tag != "Tower" || item.tag != "Resource")
            {
                Instantiate(tower, finalPosition, transform.rotation);
                manager.opalium -= tower.GetComponent<TowerStats>().opalium;
                manager.vinculum -= tower.GetComponent<TowerStats>().vinculum;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(drawPos, transform.localScale);
    }
}
