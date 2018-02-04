using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {
	public Animator animator;
	public AudioSource audiosource;
	public CanvasGroup group;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
            animator.gameObject.SetActive(true);
			animator.SetTrigger("Start");
			audiosource.Play();
			group.alpha = 0;
		}
	}
}
