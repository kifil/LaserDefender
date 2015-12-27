using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;

	void Start(){
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	//basically event listeners for the OnTrigger and OnCollision events
	void OnTriggerEnter2D(Collider2D collider){
		print ("trigger!");
		levelManager.LoadLevel("Lose");
	}
	
	void OnCollisionEnter2D(Collision2D collision ){
		print("collision");
	}
}
