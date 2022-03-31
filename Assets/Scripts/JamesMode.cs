using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JamesMode : MonoBehaviour
{
    public Material james;
    bool yames = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            yames = true;
        }
        if (yames)
        {
            James();
        }
    }

    void James()
    {
        Renderer[] rend = FindObjectsOfType(typeof(Renderer)) as Renderer[];

        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].GetComponent<Renderer>().material = james;
        }
    }
}
