using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameLogic : MonoBehaviour
{
    public GameObject[] playerObjects;
    private Vector3 lastPlaceVector3;
    public bool raceStarted = false;
    public bool raceEnded = false;
    public int bikesFinished = 0;

    [SerializeField] private TMP_Text text;
    private string[] stringArray = new string[]{"Ready?", "3", "2", "1", "GO!", ""};
    
    public float firstPlaceDistance = 0;
    public float secondPlaceDistance;
    public float thirdPlaceDistance;
    public float lastPlaceDistance;
    
    void Update() {
        
        float playerX = playerObjects[0].transform.position.x;
        
        firstPlaceDistance = playerX;
        secondPlaceDistance = playerX;
        thirdPlaceDistance = playerX;
        lastPlaceDistance = playerX;
        float zoomOutZ = 0;
        float zoomAheadX = 0;
        
        for (int i = 0; i < playerObjects.Length; i++) {
            playerX = playerObjects[i].transform.position.x;
            if (playerX > firstPlaceDistance) {
                firstPlaceDistance = playerX;
            }

            if (playerX < lastPlaceDistance) {
                lastPlaceDistance = playerX;
            }
        }
        
        for (int i = 0; i < playerObjects.Length; i++) {
            playerX = playerObjects[i].transform.position.x;
            if (playerX < firstPlaceDistance && playerX > lastPlaceDistance) {
                if (playerX < secondPlaceDistance) {
                    secondPlaceDistance = thirdPlaceDistance;
                    thirdPlaceDistance = playerX;
                }
                else {
                    secondPlaceDistance = playerX;
                }
            }
        }
        
        float distanceBetweenFirstAndLast = firstPlaceDistance - lastPlaceDistance;
        if (distanceBetweenFirstAndLast > 30) {
            zoomOutZ = (0.5f*distanceBetweenFirstAndLast) - 15;
            zoomAheadX = (0.5f * distanceBetweenFirstAndLast)-15;
        }

        lastPlaceVector3 = new Vector3(lastPlaceDistance + zoomAheadX, 0 , 0 - zoomOutZ);

        transform.position = lastPlaceVector3; //stay at the last place players x for the camera to target

        if (bikesFinished == 3) {
            raceEnded = true;
            text.text = "FINISHED!";
        }
    }

    private void Start() {
        ShowStartText(0);
    }

    private void ShowStartText(int i) {
        i++;
        StartCoroutine(ChangeText(i));
    }
    IEnumerator ChangeText(int i) {
        yield return new WaitForSeconds(1);
        text.text = stringArray[i];
        if (i < 5) {
            ShowStartText(i);
        }
    }
}
