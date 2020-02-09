using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.Enemy_N
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        int health = 100;
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
            Debug.Log("Enemy:" + transform.name + " took " + damage + " damage!");

        }

        void Death()
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
