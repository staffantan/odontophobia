using UnityEngine;
using System.Collections;

public class FreezeCloth : MonoBehaviour {
	float timer;

	Cloth cloth;

	// Use this for initialization
	void Start () {
		cloth = GetComponent<Cloth>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > 2){
			ClothSkinningCoefficient[] constraints =  cloth.coefficients;

			for(int i = 0; i < constraints.Length; i++){
				constraints[i].maxDistance = 0;
			}

			cloth.coefficients = constraints;
		}
	}
}
