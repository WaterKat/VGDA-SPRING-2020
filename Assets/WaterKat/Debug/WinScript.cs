using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public BlackBoxCounter counter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("="))
        {
            //BlackBoxCounter counter = GameObject.FindObjectOfType<BlackBoxCounter>();
            if (counter != null)
            {
                counter.blackBoxCount += 100;
                this.enabled = false;
            }
            else
            {
                Debug.LogWarning("Fuck");
            }

        }
    }
}
