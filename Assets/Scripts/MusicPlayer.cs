using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	static MusicPlayer instance = null;
	
//	public MusicPlayer(){
//		musicPlayersCreated++;
//		print ("musics: " + musicPlayersCreated);
//	}
// music player awake and start is only called when there has been a muic plaeyer placed in the scene
	
	void Awake (){
		if(instance != null){
			Destroy(gameObject);
		}
		//else this is the first music player
		else{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
