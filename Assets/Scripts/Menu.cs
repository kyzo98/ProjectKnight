using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour {
    public string SceneToLoad = "Lobby";


    AudioSource[] audioSource;
    public AudioMixer mixer;

    public Slider masterVolumeSlider;
    public Slider fxVolumeSlider;
    public Slider musicVolumeSlider;

    public RawImage rawImageFade;
    byte alpha;

    private void Start()
    { //Reset narrador
        PlayerPrefs.SetInt("NarrativeScreenOrder", 0);

        //Load settings
        masterVolumeSlider.value = PlayerPrefs.GetInt("masterVolumeSlider");
        fxVolumeSlider.value = PlayerPrefs.GetInt("fxVolumeSlider");
        musicVolumeSlider.value = PlayerPrefs.GetInt("musicVolumeSlider");

        mixer.SetFloat("masterVolume", masterVolumeSlider.value);
        mixer.SetFloat("fxVolume", fxVolumeSlider.value);
        mixer.SetFloat("musicVolume", musicVolumeSlider.value);

        audioSource = this.GetComponents<AudioSource>();
        Time.timeScale = 0.5f;

        alpha = 0;
        rawImageFade.color = new Color32(0, 0, 0, alpha);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickButton()
    {
        audioSource[0].Play();
    }

    public void HoverButton()
    {
        audioSource[1].Play();
    }

    public void SetMasterVolume()
    {
        PlayerPrefs.SetInt("masterVolumeSlider", (int)masterVolumeSlider.value);
        mixer.SetFloat("masterVolume", masterVolumeSlider.value);
    }

    public void SetFxVolume()
    {
        PlayerPrefs.SetInt("fxVolumeSlider", (int)fxVolumeSlider.value);
        mixer.SetFloat("fxVolume", fxVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetInt("musicVolumeSlider", (int)musicVolumeSlider.value);
        mixer.SetFloat("musicVolume", musicVolumeSlider.value);
    }

    public void FadeOutAndLoad()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        rawImageFade.enabled = true;
        for (int i = 0; i < 255 / 2; i++)
        {
            alpha += 2;
            rawImageFade.color = new Color32(0, 0, 0, alpha);

            yield return 0;
        }
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }
}
