using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;

namespace WaterKat.Pickups
{
    public class HealthPickup : MonoBehaviour
    {
        public int HealAmount = 1;
        private void OnTriggerEnter(Collider collider)
        {
            PlayerHealth touchingPlayerHealth = collider.GetComponent<PlayerHealth>();

            if (touchingPlayerHealth == null) { return; }

            touchingPlayerHealth.curHealth += HealAmount;

            Debug.Log("Healing: " + HealAmount);
            WaterKat.Audio.AudioManager.PlaySound("ItemGet");


            Destroy(this.gameObject);
        }
    }
}