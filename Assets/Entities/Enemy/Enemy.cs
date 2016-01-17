using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health;
	public GameObject projectile;
	public GameObject mindControlledProjectile;
	public float projectileSpeed;
	public float shotsPerSecond;
	public int pointValue = 150;
	public AudioClip fireSound;
	public AudioClip explosionSound;
	Enemy enemyPrefab;
	public GameObject positionPrefab;
	
	float timeElapsedInSecondsSinceLastShot = 0;
	float randomShotProbability = 0;
	ScoreKeeper scoreKeeper;
	WaveKeeper waveKeeper;
//	bool isFriendly = false;
	Animator myAnimtor;
	bool isMindControlled = false;
	bool isInFlyState = false;
	PlayerController playerController;

	// Use this for initialization
	void Start () {
		myAnimtor =  GetComponent<Animator>();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		waveKeeper = GameObject.Find("Wave").GetComponent<WaveKeeper>();
		playerController = GameObject.Find("PlayerShip").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckMindControlState();
		
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
	
	void CheckMindControlState(){
		if(!isInFlyState && myAnimtor.GetCurrentAnimatorStateInfo(0).IsName("Fly")){
			//enemy just enetred the flyAnimation
			isInFlyState = true;
			FlyToPlayerFormation();
		}
		
		if(!isMindControlled && myAnimtor.GetCurrentAnimatorStateInfo(0).IsName("MindControlIdle")){
			//enemy just enetred the mindContol Idle animation, they are not mind controlled
			if(playerController.NextFreeFriendlyPosition()){
				//TODO: figure out how to destroy old parent
				//set to friendlies layer
				this.gameObject.layer = 8;
				this.transform.parent = playerController.NextFreeFriendlyPosition();
			}
			else{
				//TODO make this nicer, position was filled before it arrived
				this.Die();
			}
			isInFlyState = false;
			isMindControlled = true;
		}
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		MindControlBomb mindBomb = collider.gameObject.GetComponent<MindControlBomb>();
		
		//if an object has a projectile script attached to it
		if(projectile){
			health -= projectile.GetDamage();
			projectile.Hit();
			if(health <=0 ){
				Die();
			}
			
		}
		if(mindBomb){
			MindControlHit();
			mindBomb.Hit();
		}
	}
	
	void FlyToPlayerFormation(){
	
		if(playerController.NextFreeFriendlyPosition()){
			Transform destination = playerController.NextFreeFriendlyPosition();
			
			//instantiate temp moving position
			GameObject movePosition = GameObject.Instantiate(positionPrefab,transform.position,Quaternion.identity) as GameObject;
			this.transform.parent = movePosition.transform;
			movePosition.GetComponent<Position>().SetDestination(destination.position);
			
			//move toward player position
			Vector3 differenceInVectors = destination.position - this.transform.position;
			movePosition.rigidbody2D.velocity = differenceInVectors * 1.3f; //its about 1 unit per second. so 2 secs of animation
		}
		else{
		//no more room, SELF DESTRUCT!
			this.Die();
		}

	}
	
	void MindControlHit(){

//		Animator thisAnimator = GetComponent<Animator>();
//		AnimationInfo[] thing = myAnimtor.GetNextAnimationClipState(0);
//		GetComponent<Animator>().Play("Spin");
		myAnimtor.Play("Spin");

		//find a way to animate moving from start to end position
		//either that or create a vector between start point and detination and just give it that vector until it arrives
		

		//TODO: when we arrive, set parent transform to destination
			//give this object a destination transform
			//on update when this.transform.position == destination.transform.position
				//this.transform.parent = destination.transform
			
		
		//do animation
			//spin
			//fly over to next free spot
		//reset health
		//change ship and projectile layers
		
	
	}
	
	void Die(){
		scoreKeeper.Score(pointValue);
		AudioSource.PlayClipAtPoint(explosionSound, this.transform.position, 0.1f);
		Destroy(gameObject);
	}
	
	void FireProjectile(){
		AudioSource.PlayClipAtPoint(fireSound, this.transform.position, 0.1f);
		if(isMindControlled){
			GameObject laserShot = GameObject.Instantiate(mindControlledProjectile,transform.position,Quaternion.identity) as GameObject;
			laserShot.rigidbody2D.velocity = Vector3.up * projectileSpeed;
		}
		else{
			GameObject laserShot = GameObject.Instantiate(projectile,transform.position,Quaternion.identity) as GameObject;
			laserShot.rigidbody2D.velocity = Vector3.down * projectileSpeed;
		}

	}

	public void SetDifficulty(float difficultyModifier){
		this.projectileSpeed *= difficultyModifier;
		this.health *= difficultyModifier;
	}

	

	
}
