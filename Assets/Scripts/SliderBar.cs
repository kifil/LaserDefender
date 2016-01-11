using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderBar : MonoBehaviour {

	float maxValue;
	float minValue;
	Slider thisSlider;
	// Use this for initialization
	void Start () {
		
	}
	
	public void SetMaxValue(float maxValue){
		thisSlider = GetComponent<Slider>() as Slider;
		thisSlider.maxValue = maxValue;
		thisSlider.value = maxValue;
	}
	
}
