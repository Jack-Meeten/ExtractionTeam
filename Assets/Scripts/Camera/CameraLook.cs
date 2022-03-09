using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //Mouse sensitivity input.
    [SerializeField] private float sensX = 250f;
    [SerializeField] private float sensY = 250f;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    //Mouse sensitivity floats.
    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    //Up and Down, side to side rotation.
    float xRotation;
    float yRotation;

    private void Awake()
    {
        //Locks the cursor on screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraManage();
    }

    void CameraManage()
    {
        //Gets the input from the keyboard.
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //Calculates the rotation of the camera from up and down movement and side to side movement.
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //Clamps the rotation to a specific angle so that the player doesnt clip the camera.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Sets the camera transform to the local rotation, then transforms the rotation of the camera to the X and Y values.
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}