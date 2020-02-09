using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeKeeping;

public class BasicSpawner : MonoBehaviour
{
    public GameObject desiredObject;
    public Ticker Timer = new Ticker() { MaxTick = 5 };

    public Vector3 Offset = new Vector3(0, 5, 0);
    private void Update()
    {
        if (Timer.TryTick())
        {
            GameObject childObject = Instantiate(desiredObject);
            childObject.transform.position = transform.position + Offset;
            childObject.SetActive(true);
        }
    }
}
