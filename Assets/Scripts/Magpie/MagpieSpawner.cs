using System;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Random = UnityEngine.Random;

public class MagpieSpawner : MonoBehaviour {

    public GameObject magpie;
    public GameLogic gameLogic;

    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float spawnInterval;

    private float timer;

    public Vector3 spawnPoint;


    private void Awake() {
        spawnPoint = new Vector3(180, 15, 0);
    }

    private void Start() {
        for (int i = 0; i < 12; i++) {
            spawnPoint.x -= 10;
            Instantiate(magpie, spawnPoint, Quaternion.identity);
        }
        spawnPoint = new Vector3(180, 15, 0);
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Instantiate(magpie, spawnPoint, Quaternion.identity);
            timer = 0f;
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        if (gameLogic.raceEnded == true) {
            Destroy(gameObject);
        }
    }
}
