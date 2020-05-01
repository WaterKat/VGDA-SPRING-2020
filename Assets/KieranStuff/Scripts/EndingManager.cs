using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public void GoToEndScene()
    {

        Time.timeScale = 1;
        // SceneManager.LoadScene("Credits");
        FadeInOutScript.StartFade("EndScene");
    }

}
