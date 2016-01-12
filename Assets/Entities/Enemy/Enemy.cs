using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health;
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSecond;
	public int pointValue = 150;
	public AudioClip fireSound;
	public AudioClip explosionSound;
	Enemy enemyPrefab;
	
	float timeElapsedInSecondsSinceLastShot = 0;
	float randomShotProbability = 0;
	ScoreKeeper scoreKeeper;
	WaveKeeper waveKeeper;

	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		waveKeeper = GameObject.Find("Wave").GetComponent<WaveKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsedInSecondsSinceLastShot += Time.deltaTime;
		if(randomShotProbability == 0){
			randomShotProbability = Random.value;
		}
		
		float currentProbability = timeElapsedInSecondsSinceLastShot * shotsPerSecond;
		if(randomShotProbability < currentProbability){
			FireProjectile();
			timeElapsedInSecondsSinceLastShot = 0;
			randomShotProbability = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		
		//if an object has a projectile script attached to it
		if(projectile){
			health -= projectile.GetDamage();
			projectile.Hit();
			if(health <=0 ){
				Die();
			}
			
		}
	}
	
	void Die(){
		scoreKeeper.Score(pointValue);
		AudioSource.PlayClipAtPoint(explosionSound, this.transform.position, 0.1f);
		Destroy(gameObject);
	}
	
	void FireProjectile(){
		AudioSource.PlayClipAtPoint(fireSound, this.transform.position, 0.1f);
		GameObject laserShot = GameObject.Instantiate(projectile,transform.position,Quaternion.identity) as GameObject;
		laserShot.rigidbody2D.velocity = Vector3.down * projectileSpeed;
	}

	public void SetDifficulty(float difficultyModifier){
		this.projectileSpeed *= difficultyModifier;
		Debug.Log("my speed" + projectileSpeed);
	}

	

	
}
