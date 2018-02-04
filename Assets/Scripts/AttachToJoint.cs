using UnityEngine;
using System.Collections;

public class AttachToJoint : MonoBehaviour {
    public AttachObject[] objects;
	public GameObject[] objects2;

	public GameObject realNapkin;

	public GameObject dentalEngine;

    public FadeScene fade;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < objects.Length; i++ ) {
			if (objects[i].attached) {
				objects[i].obj.transform.position = objects[i].jnt.transform.position + objects[i].offset;
                if (objects[i].followRotation) {
                    objects[i].obj.transform.rotation = objects[i].jnt.transform.rotation;
                }
            }
        }
        
	}

    public void Attach(int i) {
		objects[i].attached = true;
        objects[i].startPos = objects[i].obj.transform.position;
        objects[i].startRot = objects[i].obj.transform.rotation.eulerAngles;
		if(objects[i].calculateOffset)
			objects[i].offset = objects[i].obj.transform.position - objects[i].jnt.transform.position;
    }

    public void Detach(int i) {
		objects[i].attached = false;
        if (objects[i].resetOnDetach)
            ResetTransform(i);
    }

    public void ResetTransform(int i) {
        objects[i].obj.transform.position = objects[i].startPos;
        objects[i].obj.transform.rotation = Quaternion.Euler(objects[i].startRot);
		
    }

	public void ActiveObject(int i){
		objects2[i].SetActive(true);
	}
	
	public void DeactiveObject(int i){
		objects2[i].SetActive(false);
	}

	public void HideNapkin(int i){
		objects[i].obj.SetActive(false);
		realNapkin.GetComponent<SkinnedMeshRenderer>().enabled = true;
	}

	public void LightLamp(){
		dentalEngine.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
	}

    public void SimulationComplete() {
        fade.FadeOutToLevel(2);
    }
}

[System.Serializable]
public class AttachObject{
	public GameObject obj, jnt;
	public bool attached, calculateOffset, followRotation, resetOnDetach = true;
	
	public Vector3 offset, startPos, startRot;
}