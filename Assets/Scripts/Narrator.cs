using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class Narrator : MonoBehaviour {

    [TextArea(3,10)]
    public string[] IntroGame;
    [TextArea(3, 10)]
    public string[] IntroBoss1;
    [TextArea(3, 10)]
    public string[] OutroBoss1;
    
    public Text Narrative;
    int NarrativeScreenOrder; 
    byte alpha;
    bool SentenceFinished;
    int SentenceCounter; 


	void Start () {
        NarrativeScreenOrder = PlayerPrefs.GetInt("NarrativeScreenOrder");
        
        alpha = 0; 
        Narrative.color = new Color32(255, 255, 255, alpha);
        SentenceFinished = true;    
        SentenceCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
        switch (NarrativeScreenOrder)
        {
            case 0:
                if (SentenceFinished && SentenceCounter < 5)
                {
                    StartCoroutine(ShowSentence(IntroGame[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if(SentenceFinished && SentenceCounter == 5)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Lobby", LoadSceneMode.Single);  
                }
                break;
            case 1:
                if (SentenceFinished && SentenceCounter < 5)
                {
                    StartCoroutine(ShowSentence(IntroBoss1[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if (SentenceFinished && SentenceCounter == 5)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Fight", LoadSceneMode.Single);
                }
                break;
            case 2:
                if (SentenceFinished && SentenceCounter < 5)
                {
                    StartCoroutine(ShowSentence(OutroBoss1[SentenceCounter], 5));
                    SentenceCounter++;
                }
                else if (SentenceFinished && SentenceCounter == 5)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", 0);  // Resetj
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                }

                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
        }
         
	}

    IEnumerator ShowSentence(string s, int seconds)
    {
        SentenceFinished = false;
        Narrative.text = s;
        StartCoroutine(FadeIn()); 
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeOut());


    }

    IEnumerator FadeOut()
    {
        for (int i = 0; i < 255 / 2; i++)
        {
            alpha -= 2;
            Narrative.color = new Color32(255, 255, 255, alpha);

            yield return 0;
        }
        SentenceFinished = true; 
    }
    IEnumerator FadeIn()
    {
        for (int i = 0; i < 255 / 2; i++)
        {
            alpha += 2;
            Narrative.color = new Color32(255, 255, 255, alpha);

            yield return 0;
        }
        
    }
}
