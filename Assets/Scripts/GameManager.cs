using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool active = true;

    public List<GameObject> UI = new List<GameObject>();
    public void DisableUI()
    {
        foreach (GameObject item in UI)
        {
            if (active)
            {
                item.SetActive(false);
            }
            if(!active)
            {
                item.SetActive(true);
            }
        }
    }
}
