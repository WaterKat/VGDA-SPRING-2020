using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TrackHealth : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public UI_UpdateAlphaMask updateMask;

    private void Update()
    {
        updateMask.deltaDesiredMask = (float)playerHealth.curHealth / playerHealth.maxHealth;
    }
}
