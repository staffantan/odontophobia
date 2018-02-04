using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialSomething : MonoBehaviour {
    Animator anim;
    public Text infoText;
    public TutorialSUD inputSUD;
    public FadeScene fade;

	[TextArea]
    public string startText = "Håll nere båda triggers för att starta genomgången. Detta kan du " +
        "även göra när som helst för att avbryta simulationen";
	[TextArea]
	public string pauseText = "Du kan när som helst i spelet pausa simulationen, samt starta " +
        "den igen genom att klicka på startknappen på handkontrollen";
	[TextArea]
	public string anxietyText = "Använd vänstra styrspaken för att ändra på mätaren för att visa " + "hur du mår just nu, använd sedan A-knappen för att slutföra ditt val";
    //string anxietyText = "Använd vänstra styrspaken för att styra kameran." + " Tryck på A-knappen för att fortsätta.";
	[TextArea]
	public string finishText = "När du är redo att starta experimentet kan du trycka på A-knappen";
	[TextArea]
	public string loadingText = "Laddar simulationen, var god vänta";

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        infoText.text = startText;
    }

    // Update is called once per frame
    void Update() {
		switch(anim.GetInteger("tutorialState")){
		case 0:
			if (Input.GetAxis("Left Trigger") == 1 && Input.GetAxis("Right Trigger") == 1) {
				anim.SetInteger("tutorialState", anim.GetInteger("tutorialState") + 1);
				infoText.text = pauseText;
			}
			break;

		case 1:
			if (Input.GetButtonDown("Start Button")) {
				anim.SetInteger("tutorialState", anim.GetInteger("tutorialState") + 1);
			}
			break;

		case 2:
			if (Input.GetButtonDown("Start Button")) {
				anim.SetInteger("tutorialState", anim.GetInteger("tutorialState") + 1);
				infoText.text = anxietyText;
				inputSUD.Show();
			}
			break;

		case 3: 
			if (Input.GetButtonDown("Submit")) {
				anim.SetInteger("tutorialState", anim.GetInteger("tutorialState") + 1);
				infoText.text = finishText;
			}
			break;
		case 4:
			if (Input.GetButtonDown("Submit")) {
				infoText.text = loadingText;
                fade.FadeOutToLevel(1);
				//Application.LoadLevel(1);
			}
			break;

		}
    }
}
