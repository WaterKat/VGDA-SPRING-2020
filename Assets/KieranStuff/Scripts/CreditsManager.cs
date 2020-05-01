using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public float backToMenuTime = 27f;
    private float currentTime = 0f;

    public void BackToMenu()
    {
        SceneManager.LoadScene("Splashes");
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= backToMenuTime)
        {
            BackToMenu();
        }
    }
}
