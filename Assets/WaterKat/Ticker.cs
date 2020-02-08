using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.TimeKeeping
{
    [System.Serializable]
    public class Ticker
    {
        float currentTick = 0;
        public float MaxTick = 1;

        public void ResetTick()
        {
            currentTick = 0;
        }

        public bool TryTick()
        {
            if (currentTick > MaxTick) { currentTick = 0; }
            currentTick += Time.deltaTime;
            return currentTick >= MaxTick;
        }

        public bool TryTick(float CustomTick)
        {
            CustomTick = Mathf.Abs(CustomTick);
            if (currentTick > MaxTick) { currentTick = 0; }
            currentTick += CustomTick;
            return currentTick >= MaxTick;
        }
    }
}
