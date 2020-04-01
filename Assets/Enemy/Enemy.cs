using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.Enemy_N
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        int health = 100;

        [Header("OPTIONAL")]
        [SerializeField]
        private GameObject deathEffects;
        public int Health
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

        public void TakeDamage(int damage)
        {
            Health += -damage;

        }

        void Death()
        {
            if(deathEffects != null)
            {
                Instantiate(deathEffects, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
