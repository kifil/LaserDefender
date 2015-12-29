using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health;
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSecond;
	
	float timeElapsedInSecondsSinceLastShot = 0;
	float randomShotProbability = 0;

	void OnTriggerEnter2D(Collider2D collider){
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		
		//if an object has a projectile script attached to it
		if(projectile){
			health -= projectile.GetDamage();
			projectile.Hit();
			if(health <=0 ){
				Destroy(gameObject);
			}
			
		}
	
	
	}
	
	void FireProjectile(){
		Vector3 startPosition = this.transform.position + new Vector3(0,-1);
		GameObject laserShot = GameObject.Instantiate(projectile,startPosition,Quaternion.identity) as GameObject;
		laserShot.rigidbody2D.velocity = Vector3.down * projectileSpeed;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsedInSecondsSinceLastShot += Time.deltaTime;
		if(randomShotProbability == 0){
			randomShotProbability = Random.value;
		}
		//TODO: this probability doesnt seem right to me
		float currentProbability = timeElapsedInSecondsSinceLastShot * shotsPerSecond;
		if(randomShotProbability < currentProbability){
			FireProjectile();
			timeElapsedInSecondsSinceLastShot = 0;
			randomShotProbability = 0;
		}
		
		
	
	}
}
