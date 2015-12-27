using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Application.LoadLevel(name);
	}
	
	public void QuitRequest(){
		Debug.Log("quit requested");
		Application.Quit();
	}
	
	//Loads next level by index
	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}

}
