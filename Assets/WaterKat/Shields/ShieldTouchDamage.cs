using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTouchDamage : MonoBehaviour
{
    public float DamagePerSecond = 20;

    private float savedDamage = 0;


    private void OnCollisionStay(Collision other)
    {

        if (other.collider.gameObject.tag == "Player")
        {
            savedDamage += DamagePerSecond * Time.deltaTime;
            bool damageSucceeded = other.collider.transform.GetComponent<PlayerHealth>().TakeDamage(savedDamage);
            if (damageSucceeded)
            {
                savedDamage = 0;
            }
        }
    }
}
