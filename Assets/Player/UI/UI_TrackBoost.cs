using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TrackBoost : MonoBehaviour
{
   // public UI_UpdateAlphaMask alphaMask;
    public Image image;

    public WaterKat.Player_N.Jetpack jetpack;

    private void Update()
    {
        image.fillAmount = jetpack.fuel / jetpack.maxFuel / 2;
        //alphaMask.deltaDesiredMask = jetpack.fuel / jetpack.maxFuel;
    }
}
