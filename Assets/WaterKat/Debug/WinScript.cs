using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("="))
        {
            BlackBoxCounter counter = GameObject.FindObjectOfType<BlackBoxCounter>();

            counter.blackBoxCount += 100;

        }
    }
}
