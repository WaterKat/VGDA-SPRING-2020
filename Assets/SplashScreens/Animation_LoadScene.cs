using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_LoadScene : MonoBehaviour
{
    public string desiredScene = "SplashScreens";
    public void LoadDesiredScene()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(desiredScene);
        FadeInOutScript.StartFade(desiredScene);
    }
}
