using UnityEngine;
using System.Collections;

public static class Helpers {

	/// <summary>
	/// Gets the left screen bound.
	/// </summary>
	/// <returns>The left screen bound.</returns>
	/// <param name="zPosition">Z position of current object.</param>
	public static float GetLeftScreenBound(float zPosition){
		float zDistance = zPosition - Camera.main.transform.position.z;
		Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,zDistance));
		return leftBound.x;
	}
	
	/// <summary>
	/// Gets the right screen bound.
	/// </summary>
	/// <returns>The right screen bound.</returns>
	/// <param name="zPosition">Z position of current object.</param>
	public static float GetRightScreenBound(float zPosition){
		float zDistance = zPosition - Camera.main.transform.position.z;
		Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1,0,zDistance));
		return rightBound.x;
	}

}
