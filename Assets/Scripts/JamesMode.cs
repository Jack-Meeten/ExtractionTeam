using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JamesMode : MonoBehaviour
{
    public Material james;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
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

        Image[] image = FindObjectsOfType(typeof(Image)) as Image[];

        for (int i = 0; i < image.Length; i++)
        {
            image[i].GetComponent<Image>().material = james;
        }
    }
}
