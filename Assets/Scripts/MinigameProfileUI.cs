using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MinigameProfileUI : MonoBehaviour {
    public TMP_Text placeText;
    public BikeMovement bikeMovement;
    public GameObject fire;

    private string[] placements;

    private void Awake() {
        placements = new[] { "", "st", "nd", "rd", "th" };
    }

    private void Update() { 
        int place = bikeMovement.GetCurrentPosition(); 
        placeText.text = place.ToString() + placements[place];

        if (bikeMovement.speed >= 20) {
            fire.SetActive(true);
            StartCoroutine(HideFire());
        }
    }

    IEnumerator HideFire() {
        yield return new WaitForSeconds(2f);
        fire.SetActive(false);
    }
}
