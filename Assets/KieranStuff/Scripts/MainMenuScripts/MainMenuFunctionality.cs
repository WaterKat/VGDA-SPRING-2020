﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctionality : MonoBehaviour
{
    public GameObject homePanel;

    public string FirstScene = "FirstVideoScene";

    public void StartGame()
    {
        FadeInOutScript.StartFade(FirstScene);
       // SceneManager.LoadScene(FirstScene);
    }

    public void ExitGame()
    {
        Debug.Log("QUITTING GAME");
        Application.Quit();
    }
 
}
