using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour {

	public GameObject[] enemiesToSpawn;
	void Start() {
		InvokeRepeating("SpawnNow", 5, 4);
	}

	Vector3 getRandomPos() {
		float _x = Random.Range(-40, 40);
		float _y = 0.5f;
		float _z = Random.Range(-45, 45);
		Vector3 newPos = new Vector3(-_x, _y, _z);
		return newPos;
	}
	void SpawnNow() {
		Instantiate(enemiesToSpawn[Random.Range(0, 2)], getRandomPos(), Quaternion.identity);
	}
	void Update() {

	}
}