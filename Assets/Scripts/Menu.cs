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
    public Dropdown graphicsQualityDropdown;
    public Dropdown screenMethodDropdown;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;


    public RawImage rawImageFade;
    byte alpha;

    private void Start()
    { //Reset narrador
        PlayerPrefs.SetInt("NarrativeScreenOrder", 0);
        PlayerPrefs.SetInt("FIGHT_ORDER", 0);
        PlayerPrefs.SetInt("COINS", 0);
        PlayerPrefs.SetInt("ORBS", 0);
        PlayerPrefs.SetInt("FIGHT_ORDER", 0);
        PlayerPrefs.SetInt("Rage", 0);
        PlayerPrefs.SetInt("Terror", 0);
        PlayerPrefs.SetInt("Grief", 0);
        PlayerPrefs.SetInt("Courage", 0);
        PlayerPrefs.SetInt("Focus", 0);
        PlayerPrefs.SetInt("Will", 0);
        PlayerPrefs.SetInt("Grace", 0);

        //Setting player stats to 5 when the game starts
        PlayerPrefs.SetInt("Vitality", 5);
        PlayerPrefs.SetInt("Strenght", 5);
        PlayerPrefs.SetInt("Endurance", 5);
        PlayerPrefs.SetInt("Power", 5);
        PlayerPrefs.SetInt("Vigor", 5);

        //Load settings Sound
        masterVolumeSlider.value = PlayerPrefs.GetInt("masterVolumeSlider");
        fxVolumeSlider.value = PlayerPrefs.GetInt("fxVolumeSlider");
        musicVolumeSlider.value = PlayerPrefs.GetInt("musicVolumeSlider");

        mixer.SetFloat("masterVolume", masterVolumeSlider.value);
        mixer.SetFloat("fxVolume", fxVolumeSlider.value);
        mixer.SetFloat("musicVolume", musicVolumeSlider.value);

        //Load settings Video
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(resolutionOption);
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex");

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityIndex"));
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("qualityIndex");

        switch (PlayerPrefs.GetInt("screenIndex"))
        {
            case 0:
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreen = false;
                break;
            default:
                break;
        }
        screenMethodDropdown.value = PlayerPrefs.GetInt("screenIndex");


        audioSource = this.GetComponents<AudioSource>();
        Time.timeScale = 0.5f;

        alpha = 0;
        rawImageFade.color = new Color32(0, 0, 0, alpha);

        //StartCoroutine(FadeIn());
    }

    public void NewGame()
    {
        StartCoroutine(FadeIn());
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

    IEnumerator FadeIn()
    {
        rawImageFade.enabled = true;
        for (int i = 0; i < 255 / 2; i++)
        {
            alpha -= 2;
            rawImageFade.color = new Color32(0, 0, 0, alpha);

            yield return 0;
        }
        rawImageFade.enabled = false;
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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }

    public void SetScreenMode(int screenIndex)
    {
        switch (screenIndex)
        {
            case 0:
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreen = false;
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt("screenIndex", screenIndex);
    }

    public void SetScreenResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, true);
        switch (PlayerPrefs.GetInt("screenIndex"))
        {
            case 0:
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreen = false;
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }
}
