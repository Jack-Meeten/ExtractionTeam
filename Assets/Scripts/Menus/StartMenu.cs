using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    OptionsMenu ops;
    void Start()
    {
        ops = FindObjectOfType<OptionsMenu>();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1);
        ops.ShowCursor();
        yield return new WaitForSecondsRealtime(33);
        gameObject.SetActive(false);
    }

    public void toggleMouse()
    {
        if (Cursor.visible) Cursor.visible = false;

        else Cursor.visible = true;
    }
}
