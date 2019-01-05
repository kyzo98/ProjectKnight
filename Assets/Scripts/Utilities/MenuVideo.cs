using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuVideo : MonoBehaviour
{
    public RawImage rawImage;
    public RawImage rawImageFade;
    public VideoPlayer videoPlayer;
    byte alpha;

    void Start()
    {
        alpha = 255;
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        StartCoroutine(FadeIn());
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
    IEnumerator FadeIn()
    {
        for (int i = 0; i < 255/2; i++)
        {
            alpha -= 2;
            rawImageFade.color = new Color32(0, 0, 0, alpha);

            yield return 0;
        }
        rawImageFade.enabled = false;
    }
}
