using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI counter;

    float pollingtime = 1f;
    float time;
    int frameCount;

    private void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingtime)
        {
            int framerate = Mathf.RoundToInt(frameCount / time);
            counter.text = framerate.ToString() + "FPS";

            time -= pollingtime;
            frameCount = 0;
        }
    }
}
