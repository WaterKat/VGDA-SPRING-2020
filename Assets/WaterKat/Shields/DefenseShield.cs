using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseShield : MonoBehaviour
{
    public List<GameObject> enemyList;

    private void Update()
    {
        if (enemyList.Count == 0)
        {
            this.gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                return;
            }
        }
    }
}
