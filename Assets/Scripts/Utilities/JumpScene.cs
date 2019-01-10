﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpScene : MonoBehaviour {
    public string SceneToLoad = "Menu";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }
}
