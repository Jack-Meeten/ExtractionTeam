using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Basic values for the player speed.
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float movementMultiplier = 10f;


    // Key mapping.
    [Header("Keybindings")]
    [SerializeField] KeyCode UpKey = KeyCode.Space;
    [SerializeField] KeyCode DownKey = KeyCode.LeftControl;


    // Movement input floats.
    float horizontalMovement;
    float verticalMovement;
    float UpDownMovement;


    // Air drag.
    [Header("Drag")]
    [SerializeField] float airDrag = 3f;


    // Camera dependencies.
    [SerializeField] Transform orientation;


    // Direction of the player.
    Vector3 moveDirection;
    Vector3 UpDownDirection;


    // Rigidbody for player physics.
    Rigidbody rb;


    private void Start()
    {
        // Gets the players RB and freezes its rotation.
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {       
        // Calling Player Input and Drag Control every frame.
        PlayerInput();     
    }

    void PlayerInput()
    {
        // Gets the horizontal and the vertical input from the keyboard and sets the horizontal and vertical floats to the same value.
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        UpDownMovement = Input.GetAxisRaw("UpDown");

        // Sets the moving direction to the multiplication of the vertical and horizontal movement.
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement + orientation.up * UpDownMovement;

        // Adds the forces to the camera so that it can move.
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        // Sets airdrag for the camera to slow down.
        rb.drag = airDrag;
    }
}
