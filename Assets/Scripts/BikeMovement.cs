using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BikeMovement : MonoBehaviour {
    public float speed;

    private CharacterController _controller;
    private Vector3 moveInput;
    private Vector3 jumpInput;

    public GameObject frontWheel;
    public GameObject backWheel;
    public GameObject cyclist;

    public int playerNumber;
    private float slowDownSpeed = 0.01f;

    private bool allowedToMove = false;
    private bool moving = false;
    private bool stunned = false;
    private bool dazed = false;

    public string controlScheme;
    public PlayerInput playerInput;
    public GameLogic gameLogic;
    public BoxCollider boxCollider;
    public GameObject dizzyBirds;

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

        if (!stunned && !dazed) {
            moveInput = Vector3.right;
            speed += 1f;
        }

        if (speed >= 20 && !stunned) {
            Stunned();
        }
    }

    public void OnDuck(InputAction.CallbackContext context) {
        if (allowedToMove && !stunned) {
            cyclist.transform.position = new Vector3(cyclist.transform.position.x, 2.2f, cyclist.transform.position.z);
            dazed = true;
            StartCoroutine(unduck());
            slowDownSpeed = 0.02f;
            boxCollider.enabled = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumpInput = Vector3.up;
    }

    // Update is called once per frame
    void Update() {
        Vector3 move = new Vector3(moveInput.x, jumpInput.y, 0);
        _controller.Move(move * speed * Time.deltaTime);

        if (moving) {
            speed -= slowDownSpeed;
            if (stunned && speed >= 1) {
                speed -= 0.06f;
            }
        }

        if (speed <= 0) {
            moving = false;
        }

        frontWheel.transform.Rotate(0, speed, 0);
        backWheel.transform.Rotate(0, speed, 0);
        
        if (gameLogic.raceEnded == true) {
            allowedToMove = false;
        }
    }

    public void Stunned() {
        stunned = true;
        StartCoroutine(notStunned());
    }
    public void BirdStunned() {
        stunned = true;
        StartCoroutine(notStunned());
        dizzyBirds.SetActive(true);
    }

    IEnumerator notStunned() {
        yield return new WaitForSeconds(2);
        stunned = false;
        dizzyBirds.SetActive(false);
    }

    IEnumerator unduck() {
        yield return new WaitForSeconds(1f);
        cyclist.transform.position = new Vector3(cyclist.transform.position.x, 2.7f, cyclist.transform.position.z);
        dazed = false;
        slowDownSpeed = 0.01f;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (allowedToMove == false) {
            return;
        }
        if (other.CompareTag("Magpie" ) && dazed == false && stunned == false) {
            BirdStunned();
        }
        
        if (other.name == "FinishLine") {
            speed = 4;
            moving = false;
            allowedToMove = false;
            StartCoroutine(Stopped());
        }
    }

    IEnumerator Stopped() {
        yield return new WaitForSeconds(GetPositionInRaceReversed()*2);
        speed = 0;
    }

    public int GetPositionInRaceReversed() {
        int positionInRaceReversed = 0;

        switch (GetPositionInRace()) {
            case 1:
                positionInRaceReversed = 4;
                break;
            case 2:
                positionInRaceReversed = 3;
                break;
            case 3:
                positionInRaceReversed = 2;
                break;
            case 4:
                positionInRaceReversed = 1;
                break;
        }

        return positionInRaceReversed;
    }

    private int GetPositionInRace() {
        int positionInRace = gameLogic.bikesFinished + 1;
        gameLogic.bikesFinished++;
        return positionInRace;
    }

    public int GetCurrentPosition() {
        float playerX = transform.position.x;
        if (playerX >= gameLogic.firstPlaceDistance) {
            return 1;
        }
        if (playerX >= gameLogic.secondPlaceDistance) {
            return 2;
        }
        if (playerX >= gameLogic.thirdPlaceDistance) {
            return 3;
        }
        if (playerX >= gameLogic.lastPlaceDistance) {
            return 4;
        }
        return 0;
    }
}