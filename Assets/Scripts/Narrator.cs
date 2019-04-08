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
    [TextArea(3, 10)]
    public string[] IntroBoss2;
    [TextArea(3, 10)]
    public string[] OutroBoss2;
    [TextArea(3, 10)]
    public string[] IntroBoss3;
    [TextArea(3, 10)]
    public string[] OutroBoss3;
    [TextArea(3, 10)]
    public string[] IntroBoss4;
    [TextArea(3, 10)]
    public string[] OutroBoss4;




    public Text Narrative;
    int NarrativeScreenOrder; 
    byte alpha;
    bool SentenceFinished;
    int SentenceCounter;
    bool jumpScene;

	void Start () {
        NarrativeScreenOrder = PlayerPrefs.GetInt("NarrativeScreenOrder");
        Time.timeScale = 1;
        alpha = 0;
        Narrative.color = new Color32(255, 255, 255, alpha);
        SentenceFinished = true;
        SentenceCounter = 0;
        jumpScene = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(SentenceCounter);
        jumpScene = Input.GetKeyDown(KeyCode.Space);
        switch (NarrativeScreenOrder)
        {
            case 0:                                                                            //Intro to game narrative, 4 texts
                if (SentenceFinished && SentenceCounter < 5)
                {
                    StartCoroutine(ShowSentence(IntroGame[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if((SentenceFinished && SentenceCounter == 5) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Lobby", LoadSceneMode.Single);  
                }
                break;
            case 1:
                if (SentenceFinished && SentenceCounter < 5)                                       //Intro to Fight 1, 4 texts
                {
                    StartCoroutine(ShowSentence(IntroBoss1[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 5) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Fight", LoadSceneMode.Single);
                }
                break;
            case 2:                                                                               //Outro to Fight 1, 4 texts
                if (SentenceFinished && SentenceCounter < 4)
                {
                    StartCoroutine(ShowSentence(OutroBoss1[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 4) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
                }

                break;
            case 3:                                                                              //Intro to Fight 2, 4 texts
                if (SentenceFinished && SentenceCounter < 4)
                {
                    StartCoroutine(ShowSentence(IntroBoss2[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 4) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Fight4", LoadSceneMode.Single);
                }
                break;
            case 4:                                                                            //Outro  Fight 2, 4 texts
                if (SentenceFinished && SentenceCounter < 4)
                {
                    StartCoroutine(ShowSentence(OutroBoss2[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 4) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", 0);  // Resetj
                    SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
                }
                break;
            case 5:                                                                       //Intro to Fight 3, 4 texts
                if (SentenceFinished && SentenceCounter < 4)
                {
                    StartCoroutine(ShowSentence(IntroBoss3[SentenceCounter], 4));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 4) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Fight3", LoadSceneMode.Single);
                }
                break;
            case 6:                                                                        //Outro Fight 3, 3 texts
                if (SentenceFinished && SentenceCounter < 4)
                {
                    StartCoroutine(ShowSentence(OutroBoss3[SentenceCounter], 3));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 4) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", 0);  // Resetj
                    SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
                }
                break;
            case 7:                                                                        //Intro to Fight 4, 3 texts
                if (SentenceFinished && SentenceCounter < 3)
                {
                    StartCoroutine(ShowSentence(IntroBoss4[SentenceCounter], 3));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 3) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", NarrativeScreenOrder);
                    SceneManager.LoadScene("Fight2", LoadSceneMode.Single);
                }
                break;
            case 8:                                                                    //Outro Fight 4, 3 texts
                if (SentenceFinished && SentenceCounter < 3)
                {
                    StartCoroutine(ShowSentence(OutroBoss4[SentenceCounter], 3));
                    SentenceCounter++;
                }
                else if ((SentenceFinished && SentenceCounter == 3) || jumpScene)
                {
                    NarrativeScreenOrder++;
                    PlayerPrefs.SetInt("NarrativeScreenOrder", 0);  // Resetj
                    SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
                }
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
