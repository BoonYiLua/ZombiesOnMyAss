using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour {
    public GameObject[] enemiesToSpawn;
    public Transform[] spawnPoints;

    void Start() {
        InvokeRepeating("SpawnNow", 3, 2);
    }

    Transform getRandomSpawnPoint() {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }

    void SpawnNow() {
        Transform spawnPoint = getRandomSpawnPoint();
        GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPoint.position, Quaternion.identity);
        // Disable any scripts or components that control movement or behavior on the enemy
    }

    void Update() {

    }
}
