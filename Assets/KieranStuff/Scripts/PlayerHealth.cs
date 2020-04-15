using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;

public class PlayerHealth : MonoBehaviour
{
    public PlayerHealthText playerHealthText;
    public int maxHealth = 10;
    [SerializeField]
    private int _curHealth = 10;
    public CameraShake cameraShake;


    private CameraController camController;
    [SerializeField]
    private GameObject loseGamePanel;
    
    public int curHealth
    {
        get
        {
            return _curHealth;
        }
        set 
        {
            // Shake screen if lose health
            if(curHealth > value)
            {
                StartCoroutine(cameraShake.SingleShake(0.05f, 0.02f));
            }

            _curHealth = Mathf.Clamp(value, 0, maxHealth);
            playerHealthText.UpdateHealthUI();
            if (_curHealth <= 0)
            {
                LoseGame();
            }
        }
    }

    public float getPercentHealth()
    {
        return curHealth / (float)maxHealth;
    }


    public float damageCooldown = 0.5f;
    private float lastTookDamage = 0;
    public void TakeDamage(int damage)
    {
        if (Time.time - lastTookDamage < damageCooldown) { return; }
        lastTookDamage = Time.time;
        curHealth -= damage;
        gameObject.SendMessage("StartTakeDamageUI");
        gameObject.BroadcastMessage("CalledCameraShake", new float[] { 0.1f, damage/7f });
        WaterKat.Audio.AudioManager.PlaySound("PlayerTakeDamage");
    }
    private void LoseGame()
    {
        camController.enabled = false;
        Time.timeScale = 0;
        loseGamePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        camController = GetComponent<CameraController>();
    }
}
