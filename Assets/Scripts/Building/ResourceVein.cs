using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceVein : MonoBehaviour
{
    public Resource resource;
    public float time;
    public BuildManager manager;
    private bool whileCaptured = false;
    private bool collect = false;

    private void Update()
    {
        if (!whileCaptured)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.tag == "Resource")
                    {
                        collect = true;
                        whileCaptured = true;
                    }
                }


            }
        }
        if (whileCaptured && collect)
        {
            Debug.Log("dfsdf");
            StartCoroutine(OverTime(resource.number, resource.amountPerAction));
        }
    }

    IEnumerator OverTime(int type, int amount)
    {
        collect = false;
        yield return new WaitForSeconds(time);
        if (type == 0)
        {
            manager.vinculum += amount;
        }
        if (type == 1)
        {
            manager.opalium += amount;
        }
        collect = true;
    }
}
