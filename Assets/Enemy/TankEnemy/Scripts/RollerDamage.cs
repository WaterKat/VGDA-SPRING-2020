using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Audio;
public class RollerDamage : MonoBehaviour
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
                if (!AudioManager.SoundPlaying("RollerImpact"))
                {
                    AudioManager.PlaySound("RollerImpact");
                }
                savedDamage = 0;
            }
        }
    }
}


