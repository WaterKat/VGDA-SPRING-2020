using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Audio;

namespace WaterKat.Enemy_N
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        float health = 100;

        [Header("OPTIONAL")]
        [SerializeField]
        private GameObject deathEffects;
        [SerializeField]
        private float effectDuration = 3f;

        [SerializeField]
        private string takeDamageSound = "EnemyHit";
        [SerializeField]
        private string deathSound = "EnemyHit";

        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = Mathf.Max(value, 0);
                if (health == 0)
                {
                    Death();
                }
            }
        }

        public void TakeDamage(float damage)
        {
            Health += -damage;
            AudioManager.PlaySound(takeDamageSound);

        }

        void Death()
        {
            if(deathEffects != null)
            {
                Destroy(Instantiate(deathEffects, transform.position, transform.rotation),effectDuration);
            }
            AudioManager.PlaySound(deathSound);
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
