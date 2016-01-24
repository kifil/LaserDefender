using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource myAudioSource;
	
	void Awake (){
		if(instance != null){
			Destroy(gameObject);
		}
		//else this is the first music player
		else{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			myAudioSource = GetComponent<AudioSource>();
			//default clip
			myAudioSource.clip = startClip;
			myAudioSource.loop = true;
			myAudioSource.Play();
		}
	}
	
	//build in function that is called when a level is changed
	void OnLevelWasLoaded(int level){
		myAudioSource.Stop();
		
		if(level == 0){
			myAudioSource.clip = startClip;
		}
		else if(level == 1){
			myAudioSource.clip = gameClip;
		}
		else if(level == 2){
			myAudioSource.clip = endClip;
		}
	
		myAudioSource.Play();
	
	}


}
