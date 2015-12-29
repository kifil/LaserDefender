using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed;
	public float padding;
	public float spawnDelay;

	float xMin;
	float xMax;
	bool moveRight = true;
	
	// Use this for initialization
	void Start () {
		SpawnEnemiesUntilFormationFull();
		
		xMin = Helpers.GetLeftScreenBound(transform.position.z) + padding;
		xMax = Helpers.GetRightScreenBound(transform.position.z) - padding;
	}
	
//	void SpawnEnemiesInFormation(){
//		//spawn a new enemy at a position within the formation
//		foreach(Transform childPosition in this.transform){
//			GameObject enemy = GameObject.Instantiate(enemyPrefab,childPosition.transform.position,Quaternion.identity) as GameObject;
//			//set the enemy's parent to be a position in the formation
//			enemy.transform.parent = childPosition;
//		}
//	}
	
	void SpawnEnemiesUntilFormationFull(){
		Transform nextPosition = NextFreePosition();
		if(nextPosition){
			GameObject enemy = GameObject.Instantiate(enemyPrefab,nextPosition.transform.position,Quaternion.identity) as GameObject;
			//set the enemy's parent to be a position in the formation
			enemy.transform.parent = nextPosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnEnemiesUntilFormationFull", spawnDelay);
		}

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
		
		if(FormationEmpty()){
			SpawnEnemiesUntilFormationFull();
		}

	
	}
	
	Transform NextFreePosition(){
		//loop through all the positions in the formation
		foreach(Transform childPosition in transform){
			//see if the position has an enemy on it
			if(childPosition.childCount == 0){
				return childPosition;
			}
		}
		//No free positions found
		return null;
		
	}
	
	bool FormationEmpty(){
		//loop through all the positions in the formation
		foreach(Transform childPosition in transform){
			//see if the position has an enemy on it
			if(childPosition.childCount > 0){
				return false;
			}
		}
		return true;
	
	}
}
