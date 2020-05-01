using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WaterKat.Player_N;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
    [Header("These fields are found on start")]
    public PlayerHealth playerHealth;
    public Jetpack jetpack;
    public Running running;
    public CameraController camController;

    [Header("Input these fields")]
    public GameObject pausePanel;
    public GameObject settingsPanel;
    private bool menuOpen = false;
    private bool settingsMenuOpen = false;

    public AudioMixer audioMixer;
    public Toggle cheatModeToggler;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        jetpack = player.GetComponent<Jetpack>();
        running = player.GetComponent<Running>();
        camController = player.GetComponent<CameraController>();

        volumeSlider.value = 0.88f;
        sensitivitySlider.value = 0.5f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen)
        {
            Debug.Log("MENU OPEN");
            menuOpen = true;
            Debug.Log("BOOL CHANGED");
            pausePanel.SetActive(true);
            Debug.Log("PANEL ACTIVATED");
            Time.timeScale = 0;
            Debug.Log("TIME FROZE");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen && !settingsMenuOpen)
        {
            Debug.Log("PANEL DEACTIVATED");
            Time.timeScale = 1;
            menuOpen = false;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen && settingsMenuOpen)
        {
            GoBackFromSettings();
        }
    }

    public void OpenSettings()
    {
        settingsMenuOpen = true;
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBackFromSettings()
    {
        settingsMenuOpen = false;
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void TurnCheatModeOn()
    {
        playerHealth.maxHealth = 10000;
        playerHealth.curHealth = 10000;

        jetpack.fuelCost = 0;
        jetpack.jetpackMaxVelocity *= 3;


        running.runningMaxVelocity = 150f;
        running.runningAcceleration = running.runningMaxVelocity * 2;

        running.enabled = false;
        running.enabled = true;

        cheatModeToggler.interactable = false;
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-50f, 10f, volumeSlider.value));
    }

    public void SetSensitivity()
    {
        camController.sensitivityModifier = Mathf.Lerp(0f, 2f, sensitivitySlider.value);
    }
}