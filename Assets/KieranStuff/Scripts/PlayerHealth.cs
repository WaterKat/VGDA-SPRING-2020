using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerHealthText playerHealthText;
    public int maxHealth = 10;
    [SerializeField]
    private int _curHealth = 10;
    
    public int curHealth
    {
        get
        {
            return _curHealth;
        }
        set 
        {
            _curHealth = Mathf.Max(0, value);
            playerHealthText.UpdateHealthUI();
        }
    }

    public float getPercentHealth()
    {
        return curHealth / (float)maxHealth;
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
}
