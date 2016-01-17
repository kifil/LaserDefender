using UnityEngine;
using System.Collections;

public class MindControlBomb : MonoBehaviour {

	public void Hit(){
		Destroy(gameObject);
	}
}
