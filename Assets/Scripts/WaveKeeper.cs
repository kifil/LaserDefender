using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveKeeper : MonoBehaviour {
	public static int currentWave = 0;
	Text waveText;
	
	// Use this for initialization
	void Start () {
		waveText = GetComponent<Text>();
		Reset();
	}
	
	public void NextWave(){
		currentWave++;
		waveText.text = currentWave.ToString();
	}
	
	public static void Reset(){
		currentWave = 0;
	}
	
	public int GetCurrentWave(){
		return currentWave;
	}
}
