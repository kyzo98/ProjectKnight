using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpScene : MonoBehaviour {
    public string SceneToLoad = "Lobby";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }
}
