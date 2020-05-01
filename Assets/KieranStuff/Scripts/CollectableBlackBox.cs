﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlackBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<BlackBoxCounter>().blackBoxCount++;
            WaterKat.Audio.AudioManager.PlaySound("BlackBox");
            Destroy(this.gameObject);
        }
    }
}
