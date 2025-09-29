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

    private bool allowedToMove = false;
    private bool moving = false;
    private bool stunned = false;

    public string controlScheme;
    public PlayerInput playerInput;
    public GameLogic gameLogic;

    void Start() {
        _controller = GetComponent<CharacterController>();
        if (controlScheme == "Gamepad") {
            playerInput.SwitchCurrentControlScheme(controlScheme, Gamepad.current);
        }
        else {
            playerInput.SwitchCurrentControlScheme(controlScheme, Keyboard.current);
        }

        StartCoroutine(startRace());
    }

    IEnumerator startRace() {
        yield return new WaitForSeconds(4);
        allowedToMove = true;
    }
    
    public void OnPedal(InputAction.CallbackContext context) {
        if (!allowedToMove) return;
        
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
        }
        
        if (speed <= 0) {
            moving = false;
        }
        
        frontWheel.transform.Rotate(0, speed, 0);
        backWheel.transform.Rotate(0, speed, 0);
    }

    public void Stunned() {
        stunned = true;
        StartCoroutine(notStunned());
    }
    
    IEnumerator notStunned() {
        yield return new WaitForSeconds(2);
        stunned = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "FinishLine") {
            speed = 4;
            moving = false;
            allowedToMove = false;
            StartCoroutine(Stopped());
        }
        
    }

    IEnumerator Stopped() {
        yield return new WaitForSeconds(GetPositionInRaceReversed() + 1);
        speed = 0;
    }


    private int GetPositionInRaceReversed() {
        int positionInRaceReversed = 0;

        switch (GetPositionInRace()) {
            case 1: positionInRaceReversed = 4;
                break;
            case 2: positionInRaceReversed = 3;
                break;
            case 3: positionInRaceReversed = 2;
                break;
            case 4: positionInRaceReversed = 1;
                break;
        }
        return positionInRaceReversed;
    }

    private int GetPositionInRace() {
        int positionInRace = 0;
        positionInRace = gameLogic.bikesFinished + 1;
        gameLogic.bikesFinished++;

        return positionInRace;
    }
    
}
