using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float padding = 1.0f;
	public GameObject playerLaserPrefab;
	public GameObject playerMindControlPrefab;
	public float projectileSpeed;
	public float projectileRate;
	public float maxHealth;
	public AudioClip fireSound;
	public bool isInvulnerable = false;
	public Slider shieldSlider;
	public float shieldRegenerationRate;
	public float shieldRegenerationAmount;
	
	private float health;
	private float xMin;
	private float xMax;
	// Use this for initialization
	void Start () {
		health = maxHealth;
		xMin = Helpers.GetLeftScreenBound(transform.position.z) + padding;
		xMax = Helpers.GetRightScreenBound(transform.position.z) - padding;
		SliderBar shieldSliderBar = shieldSlider.GetComponent<SliderBar>();
		shieldSliderBar.SetMaxValue(health);
		InvokeRepeating("RegenerateShields",0.0001f,shieldRegenerationRate);
	} 
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		HandleFiring();
		
	}
	
	void RegenerateShields(){
		UpdateHealth(shieldRegenerationAmount);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		//if an object has a projectile script attached to it
		if(projectile){
			ProjectileHit(projectile);
		}
	}
	
	void ProjectileHit(Projectile projectile){
		UpdateHealth(-projectile.GetDamage());
		projectile.Hit();
		if(health <=0 && !isInvulnerable){
			Die ();
		}
	}
	
	void UpdateHealth(float healthChange){
		health += healthChange;
		if(health > maxHealth){
			health = maxHealth;
		}
		shieldSlider.value = health;
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
	
	void FireMindControl(){
	//TODO: update sound
		AudioSource.PlayClipAtPoint(fireSound,this.transform.position,0.1f);
		//		Vector3 projectileLocation = transform.position + new Vector3(0,transform.localScale.y / 2);
		GameObject mindShot = GameObject.Instantiate(playerMindControlPrefab,transform.position,Quaternion.identity) as GameObject;
		mindShot.rigidbody2D.velocity = Vector2.up * projectileSpeed;
	}
	
	void HandleFiring(){
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("FireProjectile",0.00001f, projectileRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("FireProjectile");
		}
		if(Input.GetKeyDown(KeyCode.C)){
			FireMindControl();
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
	
	public Transform NextFreeFriendlyPosition(){
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
}
