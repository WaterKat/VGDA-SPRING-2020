using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthText : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;
    public Image filledHealthBarImage;

    private void Start()
    {
        healthText.text = playerHealth.curHealth + " / " + playerHealth.maxHealth;
    }

    public void UpdateHealthUI()
    {
        Debug.Log("UPDATING UI");
        healthText.text = playerHealth.curHealth + " / " + playerHealth.maxHealth;
        filledHealthBarImage.fillAmount = playerHealth.getPercentHealth();
    }
}
