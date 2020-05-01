using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;
public class ActivatedShields : MonoBehaviour
{
    public GameObject activateWhat;
    private void OnTriggerEnter(Collider other)
    {
        Player possiblePlayer = other.gameObject.GetComponent<Player>();
        if (possiblePlayer != null)
        {
            activateWhat.SetActive(true);
            this.enabled = false;
        }
    }
}
