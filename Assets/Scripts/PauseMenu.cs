using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {
    public string SceneToLoad = "Menu";


    AudioSource[] audioSource;
    public AudioMixer mixer;

    public Slider masterVolumeSlider;
    public Slider fxVolumeSlider;
    public Slider musicVolumeSlider;
    public Dropdown graphicsQualityDropdown;
    public Dropdown screenMethodDropdown;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
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

        for (int i = 0; i < resolutions.Length; i++)
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
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
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
