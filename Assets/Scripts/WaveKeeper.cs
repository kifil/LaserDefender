using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveKeeper : MonoBehaviour {
	public static int currentWave = 0;
	public GameObject[] enemyPrefabs;
	public float roundDifficultyModifier;
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
	
	int NumberOfRoundsCompleted(){
		int roundsCompleted =  currentWave / enemyPrefabs.Length;
		return roundsCompleted;
	}
	
	public GameObject EnemyForCurrentWave(){
		int enemySpawnWave = currentWave % enemyPrefabs.Length;
		return enemyPrefabs[enemySpawnWave];
	}
	
	public float CurrentDifficulty(){
		return (NumberOfRoundsCompleted() * roundDifficultyModifier) + 1;
	}
	
}
