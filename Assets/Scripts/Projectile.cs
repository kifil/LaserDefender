using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage;
	
//	public bool isEnemy;
	
	public float GetDamage(){
		return damage;
	}
	
	public void Hit(){
		Destroy(gameObject);
	}
	
}
