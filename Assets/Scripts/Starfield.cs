using UnityEngine;
using System.Collections;

public class Starfield : MonoBehaviour {

	ParticleSystem starfield;
	public float minStarSpeed;
	public float maxStarSpeed;
	// Use this for initialization
	void Start () {
		starfield = this.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
//		starfield.startSpeed = Random.Range(minStarSpeed,maxStarSpeed);
	
	}
}
