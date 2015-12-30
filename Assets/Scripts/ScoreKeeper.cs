using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	public static int currentScore = 0;
	Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		Reset();
	}
	
	public void Score(int points){
		currentScore += points;
		scoreText.text = currentScore.ToString();
	}
	
	public static void Reset(){
		currentScore = 0;
	}
}
