using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeInOutScript : MonoBehaviour
{
    private static FadeInOutScript main;

    public float fadeTime = 0;
    public float fadeMaxTime = 1;
    public Image fadeImage;

    public bool sceneLoaded = false;
    public bool animationRunning = false;

    private string nextSceneToLoad = "";

    private void Awake()
    {
        main = this;
        SceneManager.sceneLoaded += newSceneLoaded;
        DontDestroyOnLoad(main.gameObject);
    }
    public void newSceneLoaded(Scene scene, LoadSceneMode loadscenemode)
    {
        sceneLoaded = true;
    }

    public static void StartFade(string desiredScene)
    {
        if (main.animationRunning)
        {
            return;
        }
        main.nextSceneToLoad = desiredScene;
        main.animationRunning = true;
        main.sceneLoaded = false;
        main.fadeImage.color = new Color(0, 0, 0, 0);
        main.fadeImage.enabled = true;
        main.fadeTime = 0;
        main.StartCoroutine("FadeCoroutine");
    }

    IEnumerator FadeCoroutine()
    {
        while (fadeTime < fadeMaxTime)
        {
            fadeTime += Time.deltaTime;
            main.fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, fadeTime / fadeMaxTime));
            yield return null;
        }
        SceneManager.LoadScene(nextSceneToLoad);
        while (!sceneLoaded)
        {
            yield return null;
        }
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            main.fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, fadeTime / fadeMaxTime));
            yield return null;
        }
        main.sceneLoaded = false;
        main.fadeImage.color = new Color(0, 0, 0, 0);
        main.fadeImage.enabled = false;
        main.fadeTime = 0;
        main.animationRunning = false;

    }

}
