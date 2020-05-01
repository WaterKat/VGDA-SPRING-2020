using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using WaterKat.Player_N;

public class BlackBoxCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI blackBoxText;
    [SerializeField]
    private GameObject winGamePanel;
    private CameraController camController;

    private int _blackBoxCount = 0;
    private int blackBoxGoal = 4;
    public int blackBoxCount
    {
        get
        {
            return _blackBoxCount;
        }
        set
        {
            _blackBoxCount = value;
            UpdateBlackBoxUI();
            if(_blackBoxCount >= blackBoxGoal)
            {
                WinGame();
            }
        }
    }

    private void Start()
    {
        camController = GetComponent<CameraController>();
        UpdateBlackBoxUI();
    }

    private void UpdateBlackBoxUI()
    {
        blackBoxText.text = blackBoxCount + " Boxes Collected";
    }

    private void WinGame()
    {
        camController.enabled = false;
        Time.timeScale = 0;
        winGamePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayAgain()
    {
        Debug.Log("Played again!");
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

}
