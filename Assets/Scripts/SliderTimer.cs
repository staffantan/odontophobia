using UnityEngine;
using System.Collections;

public class SliderTimer : MonoBehaviour {
	public InputSUD inputSUD;
	public float SUDTime = 5 * 60;
	float timer;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > SUDTime) {
			timer = 0;
			inputSUD.Show ();
		}
	}
}
