using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScene : MonoBehaviour {
    float fade;
    Image black;
    public Color transparent, full;
    public enum Fade {
        In,
        Out
    }
    public Fade fadeMode;
    int levelToLoad;
    bool loadLevel;
    float timer;

	// Use this for initialization
	void Start () {
        black = GetComponent<Image>();

        switch (fadeMode) {
            case Fade.In:
            black.color = full;
            break;
            case Fade.Out:
            black.color = transparent;
            break;
        }
	}

    public void FadeOutToLevel(int level) {
        levelToLoad = level;
        loadLevel = true;
        fadeMode = Fade.Out;
    }

	// Update is called once per frame
	void Update () {
        if (loadLevel) {
            timer += Time.deltaTime;
            if (timer > 2) {
                Application.LoadLevel(levelToLoad);
            }
        }
        switch (fadeMode) {
            case Fade.In:
                black.color = Color.Lerp(black.color, transparent, Time.deltaTime * 0.5f);
            break;
            case Fade.Out:
                black.color = Color.Lerp(black.color, full, Time.deltaTime * 2);
            break;
        }
	}


}
