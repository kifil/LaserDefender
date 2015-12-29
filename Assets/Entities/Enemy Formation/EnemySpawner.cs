using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed;
	public float padding;

	float xMin;
	float xMax;
	bool moveRight = true;
	
	// Use this for initialization
	void Start () {
		foreach(Transform child in this.transform){
			//spawn a new enemy at a position within the formation
			GameObject enemy = GameObject.Instantiate(enemyPrefab,child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	
		xMin = Helpers.GetLeftScreenBound(transform.position.z) + padding;
		xMax = Helpers.GetRightScreenBound(transform.position.z) - padding;
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position,new Vector3(width,height));
	}
	
	// Update is called once per frame
	void Update () {
		if(moveRight){
			this.transform.position += new Vector3(speed * Time.deltaTime,0f, 0f);
		}
		else{
			this.transform.position += new Vector3(-speed * Time.deltaTime,0f, 0f);
		}
		
		float rightEdgOfFormation = transform.position.x + (0.5f * width);
		float leftEdgOfFormation = transform.position.x - (0.5f * width);
		
		//flip directions at the borders
		if(rightEdgOfFormation >= xMax){
			moveRight = false;
		}
		if(leftEdgOfFormation <= xMin){
			moveRight = true;
		}
		
//		float clampedX = Mathf.Clamp(transform.position.x,xMin,xMax);
//		transform.position = new Vector3(clampedX,transform.position.y);
	
	}
}
