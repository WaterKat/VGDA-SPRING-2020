using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_TrackBoost : MonoBehaviour
{
    public UI_UpdateAlphaMask alphaMask;
    public WaterKat.Player_N.Jetpack jetpack;

    private void Update()
    {
        alphaMask.deltaDesiredMask = jetpack.fuel / jetpack.maxFuel;
    }
}
