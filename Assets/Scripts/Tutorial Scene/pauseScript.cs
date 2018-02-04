using UnityEngine;
using System.Collections;

public class pauseScript : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public AudioSource audioSource;

    public FadeScene fade;
    float loadLevelTimer = 2;

    bool isPaused;
	// Use this for initialization
	void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Start Button")) {
            if (isPaused) {
                canvasGroup.alpha = 0;
                audioSource.pitch = 1;
                Time.timeScale = 1f;
            } else {
                canvasGroup.alpha = 1;
                audioSource.pitch = 0;
                Time.timeScale = 0;
            }
            isPaused = !isPaused;
        }

        if (Application.loadedLevel == 1) {
            if (Input.GetAxis("Left Trigger") == 1 && Input.GetAxis("Right Trigger") == 1) {
                canvasGroup.alpha = 1;
                audioSource.pitch = 0;
                fade.FadeOutToLevel(2);
            }
        }
    }
}
