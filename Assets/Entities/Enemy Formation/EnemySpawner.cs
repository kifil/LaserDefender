using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		foreach(Transform child in this.transform){
			//spawn a new enemy at a position within the formation
			GameObject enemy = GameObject.Instantiate(enemyPrefab,child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
