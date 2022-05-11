using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    OptionsMenu ops;
    void Start()
    {
        ops = FindObjectOfType<OptionsMenu>();
        StartCoroutine(wait());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1);
        ops.ShowCursor();
        yield return new WaitForSecondsRealtime(33);
        gameObject.SetActive(false);
    }
}
