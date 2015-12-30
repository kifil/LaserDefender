using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding = 1.0f;
	public GameObject playerLaserPrefab;
	public float projectileSpeed;
	public float projectileRate;
	public float health;
	public AudioClip fireSound;
	
	float xMin;
	float xMax;
	// Use this for initialization
	void Start () {
		xMin = Helpers.GetLeftScreenBound(transform.position.z) + padding;
		xMax = Helpers.GetRightScreenBound(transform.position.z) - padding;
	} 
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		HandleFiring();
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		
		//if an object has a projectile script attached to it
		if(projectile){
			Debug.Log("player hit!");
			health -= projectile.GetDamage();
			projectile.Hit();
			if(health <=0){
				Die ();
			}
		}
		
		
	}
	
	void Die(){
		//TODO explosion sounds and graphic
		LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		levelManager.LoadLevel("Win");
		Destroy(gameObject);
	}
	
	void FireProjectile(){
		AudioSource.PlayClipAtPoint(fireSound,this.transform.position,0.1f);
//		Vector3 projectileLocation = transform.position + new Vector3(0,transform.localScale.y / 2);
		GameObject laserShot = GameObject.Instantiate(playerLaserPrefab,transform.position,Quaternion.identity) as GameObject;
		laserShot.rigidbody2D.velocity = Vector2.up * projectileSpeed;
	}
	
	void HandleFiring(){
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("FireProjectile",0.00001f, projectileRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("FireProjectile");
		}
	}
	
	void HandleMovement(){
		if(Input.GetKey(KeyCode.RightArrow)){
			this.transform.position += new Vector3(speed * Time.deltaTime,0f, 0f);
			
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			this.transform.position += new Vector3(-speed * Time.deltaTime,0f, 0f);
		}
		
		float clampedX = Mathf.Clamp(transform.position.x,xMin,xMax);
		transform.position = new Vector3(clampedX,transform.position.y, 0f);
	}
}
