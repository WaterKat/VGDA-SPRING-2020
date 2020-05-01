using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public void GoToEndScene()
    {
        SceneManager.LoadScene("Ending");
    }

}
