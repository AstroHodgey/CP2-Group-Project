using System;
using System.Collections;
using UnityEngine;

public class CameraScrolling : MonoBehaviour {
    [SerializeField] private Vector3 offset;
    [SerializeField] private float dampening;

    public GameLogic gameLogic;
    public Transform target;

    private Vector3 vel = Vector3.zero;
    private Vector3 targetPosition;

    void Update() {

        if (gameLogic.raceEnded == true) {
            StartCoroutine(NewPosition());
        }
        
        if (gameLogic.raceEnded == false) {
            targetPosition = target.position + offset;
        }
        
        if (target.position.x < 150) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, dampening);
        }
    }

    IEnumerator NewPosition() {
        yield return new WaitForSeconds(2f);
        targetPosition.x = 160;
        targetPosition.z = -30;
    }
    
    
}
