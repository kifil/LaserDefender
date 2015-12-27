using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding = 1.0f;
	
	float xMin;
	float xMax;
	// Use this for initialization
	void Start () {
		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,zDistance));
		Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1,0,zDistance));
		xMin = leftBound.x + padding;
		xMax = rightBound.x - padding;
	} 
	
	// Update is called once per frame
	void Update () {
	
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
