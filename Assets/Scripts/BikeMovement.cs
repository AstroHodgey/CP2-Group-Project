using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BikeMovement : MonoBehaviour {
    public float speed;

    private CharacterController _controller;
    private Vector3 moveInput;
    private Vector3 jumpInput;

    public GameObject frontWheel;
    public GameObject backWheel;

    public int playerNumber;
    
    private bool moving = false;
    private bool stunned = false;

    public string controlScheme;
    public PlayerInput playerInput;

    void Start() {
        _controller = GetComponent<CharacterController>();
        if (controlScheme == "Gamepad") {
            playerInput.SwitchCurrentControlScheme(controlScheme, Gamepad.current);
        }
        else {
            playerInput.SwitchCurrentControlScheme(controlScheme, Keyboard.current);
        }
    }

    public void OnPedal(InputAction.CallbackContext context) {

        if (!moving) {
            moving = true;
        }

        if (!stunned) {
            moveInput = Vector3.right;
            speed += 1f;
        }

        if (speed >= 20 && !stunned) {
            Stunned();
        }
    }

    public void OnDuck(InputAction.CallbackContext context) {
        Stunned();
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumpInput = Vector3.up;
    }

    // Update is called once per frame
    void Update() {
        Vector3 move = new Vector3(moveInput.x, jumpInput.y, 0);
        _controller.Move(move * speed * Time.deltaTime);

        if (moving) {
            speed -= 0.01f;
            if (stunned && speed >= 1 ) {
                speed -= 0.05f;
            }
            frontWheel.transform.Rotate(0, speed, 0);
            backWheel.transform.Rotate(0, speed, 0);
        }

        if (speed <= 0) {
            moving = false;
        }
    }

    public void Stunned() {
        stunned = true;
        StartCoroutine(notStunned());
    }
    
    IEnumerator notStunned() {
        yield return new WaitForSeconds(2);
        stunned = false;
    }

}
