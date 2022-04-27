using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceVein : MonoBehaviour
{
    public Resource resource;
    public float time;
    public BuildManager manager;
    private bool collect = true;

    private void Start()
    {
        manager = FindObjectOfType<BuildManager>();
    }
    private void Update()
    {
        if (collect)
        {
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
