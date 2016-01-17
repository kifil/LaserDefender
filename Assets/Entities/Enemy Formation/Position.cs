using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	Vector3 destinationPosition;

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(this.transform.position,1);
	}
	
	public void SetDestination(Vector3 des){
		destinationPosition = des;
	}
	
//	void Update(){
//		if(this.transform.position == destinationPosition){
//			this.rigidbody2D.velocity = new Vector2(0,0);
//		}
//	}
}
