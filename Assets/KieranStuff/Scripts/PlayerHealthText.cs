﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthText : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        healthText.text = playerHealth.curHealth + " / " + playerHealth.maxHealth;
    }

    public void UpdateHealthUI()
    {
        healthText.text = playerHealth.curHealth + " / " + playerHealth.maxHealth;
    }
}
