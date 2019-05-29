using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieNarrator : MonoBehaviour {

    [TextArea()]
    public string[] dieNarrative;

    public Text narrative;
    byte alpha;
    int sentenceCounter;
    bool sentenceFinished;
    bool jumpScene;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        alpha = 0;
        narrative.color = new Color32(255, 255, 255, alpha);
        sentenceFinished = true;
        jumpScene = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(sentenceFinished && sentenceCounter < 4)
        {
            StartCoroutine(ShowText(dieNarrative[sentenceCounter], 4));
            sentenceCounter++;
        }
        else if(sentenceFinished && sentenceCounter == 4)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
	}

    IEnumerator ShowText(string text, int seconds)
    {
        sentenceFinished = false;
        narrative.text = text;
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        for(int i = 0; i < 255 / 2; i++)
        {
            alpha += 2;
            narrative.color = new Color32(255, 255, 255, alpha);

            yield return 0;
        }
    }

    IEnumerator FadeOut()
    {
        for(int i = 0; i < 255 / 2; i++)
        {
            alpha -= 2;
            narrative.color = new Color32(255, 255, 255, alpha);

            yield return 0;
        }
        sentenceFinished = true;
    }
}
