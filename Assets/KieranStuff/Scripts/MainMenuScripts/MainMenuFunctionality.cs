using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctionality : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject homePanel;

    public void StartGame()
    {
        SceneManager.LoadScene("3rdPresentation");
    }

    public void ExitGame()
    {
        Debug.Log("QUITTING GAME");
        Application.Quit();
    }

    public void OpenSettings()
    {
        homePanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    
    public void CloseSettings()
    {
        SettingsPanel.SetActive(false);
        homePanel.SetActive(true);
    }
}
