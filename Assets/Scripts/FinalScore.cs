using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text scoreText = GetComponent<Text>();
		scoreText.text = ScoreKeeper.currentScore.ToString();
		ScoreKeeper.Reset();
	}
	
}
