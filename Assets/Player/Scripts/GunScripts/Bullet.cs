using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Enemy_N;

namespace WaterKat.Player_N
{
    public class Bullet : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public float DamageDealt = 5;

        private void Start()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            Destroy(gameObject, .33f);
        }
        private void OnCollisionEnter(Collision collision)
        {
            Enemy enemyscript = collision.collider.transform.GetComponent<Enemy>();
            if (enemyscript != null)
            {
                enemyscript.TakeDamage(DamageDealt);
            }
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
