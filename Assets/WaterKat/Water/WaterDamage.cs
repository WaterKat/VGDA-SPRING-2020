using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    public float DamagePerSecond = 10;

    private float savedDamage = 0;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            savedDamage += DamagePerSecond * Time.deltaTime;
            bool damageSucceeded = other.transform.GetComponent<PlayerHealth>().TakeDamage(savedDamage);
            if (damageSucceeded)
            {
                savedDamage = 0;
            }
        }
    }
}
