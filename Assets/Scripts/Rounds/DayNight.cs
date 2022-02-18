using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    // Setting game's TickRate.
    [SerializeField] float CurrentTick;
    [SerializeField] float CurrentAngle;
    [SerializeField] float RotationSpeed;
    [SerializeField] float SmoothAngle;

    [SerializeField] GameObject TickRate;

    // Get Component for Sun rotation.
    [SerializeField] Transform CenterRotation;

    private void Start()
    {
        
    }

    void Update()
    {

    }

    void AddAngle()
    {
        // Get the component to rotate.
        CurrentAngle = CurrentTick / 60;
        CenterRotation.transform.rotation = Quaternion.Euler(CurrentAngle, 0, 0);
    }

    void LightAngle()
    {
    }
}
