﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scannable : MonoBehaviour
{
    public ScanManager scanManager;

    [Header("INPUT THIS FIELD")]
    public ScannableIDS scannableID;

    [HideInInspector]
    public string scanName = "";
    [HideInInspector]
    public string scanLore = "";
    [HideInInspector]
    public float scanTimeLength = 3f;
    [HideInInspector]
    public Sprite scannedItemImage;

    public enum ScannableIDS
    {
        drone = 0,
        stationaryEnemy = 1,
        wheelEnemy = 2,
        healthPickup = 3,
        blackBox = 4,
    }

    private void Start()
    {
        scanManager = GameObject.Find("ScanManager").GetComponent<ScanManager>();

        switch (scannableID)
        {
            case ScannableIDS.drone:
                scanName = "Flying Drone";
                scanLore = scanManager.flyingDroneLore;
                scannedItemImage = scanManager.flyingDroneImage;
                scanTimeLength = scanManager.flyingDroneScanTime;
                break;
            case ScannableIDS.stationaryEnemy:
                scanName = "Stationary Enemy";
                scanLore = scanManager.stationaryEnemyLore;
                scannedItemImage = scanManager.stationaryEnemyImage;
                scanTimeLength = scanManager.stationaryEnemyScanTime;
                break;
            case ScannableIDS.wheelEnemy:
                scanName = "Roller Enemy";
                scanLore = scanManager.wheelEnemyLore;
                scannedItemImage = scanManager.wheelEnemyImage;
                scanTimeLength = scanManager.wheelEnemyScanTime;
                break;
            case ScannableIDS.healthPickup:
                scanName = "Health Canister";
                scanLore = scanManager.healthPickupLore;
                scannedItemImage = scanManager.healthPickupImage;
                scanTimeLength = scanManager.healthPickupScanTime;
                break;
            case ScannableIDS.blackBox:
                scanName = "Black Box";
                scanLore = scanManager.blackBoxLore;
                scannedItemImage = scanManager.blackBoxImage;
                scanTimeLength = scanManager.blackBoxScanTime;
                break;
            default:
                break;
        }
    }
}
