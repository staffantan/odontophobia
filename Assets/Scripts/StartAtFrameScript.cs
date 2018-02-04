using UnityEngine;
using System.Collections;

public class StartAtFrameScript : MonoBehaviour {
    Animator[] anims;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        anims = GameObject.FindObjectsOfType<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F)) {
            /*
            foreach (Animator a in anims) {
                a.speed = 10;
            }
             * */
            Time.timeScale = 10;
            audioSource.pitch = 10;
        } else {
            /*
            foreach (Animator a in anims) {
                a.speed = 1;
            }
             */
            Time.timeScale = 1;
            audioSource.pitch = 1;
        }
	}
}
