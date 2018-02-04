using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	float x, y, z;
	public float speed = 1;
	// Use this for initialization
	void Start () {
		x = transform.rotation.eulerAngles.x;
		y = transform.rotation.eulerAngles.y;
		z = transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		y += Time.deltaTime * speed;
		transform.rotation = Quaternion.Euler(x, y, z);
	}
}
