using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MinigameProfileUI : MonoBehaviour {
    public TMP_Text placeText;
    public TMP_Text scoreText;
    public BikeMovement bikeMovement;
    public GameObject fire;

    private string[] placements;
    private int[] points;

    private void Awake() {
        placements = new[] { "", "st", "nd", "rd", "th" };
        points = new[] { 0, 200, 140, 60, 25 };
    }

    private void Update() { 
        int place = bikeMovement.GetCurrentPosition(); 
        placeText.text = place.ToString() + placements[place];

        if (bikeMovement.speed >= 20) {
            fire.SetActive(true);
            StartCoroutine(HideFire());
        }

        if (bikeMovement.gameLogic.raceEnded) {
            StartCoroutine(GivePoints());
        }
    }

    IEnumerator HideFire() {
        yield return new WaitForSeconds(2f);
        fire.SetActive(false);
    }

    IEnumerator GivePoints() {
        yield return new WaitForSeconds(4f);
        scoreText.text = "Points: " + points[bikeMovement.GetCurrentPosition()];
    }
    
}
