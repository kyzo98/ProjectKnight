using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public string SceneToLoad = "Lobby";

    private void Start()
    { //Reset narrador
        PlayerPrefs.SetInt("NarrativeScreenOrder", 0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
