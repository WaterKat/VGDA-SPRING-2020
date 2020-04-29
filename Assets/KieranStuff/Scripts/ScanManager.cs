using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    [Header("Flying Drone")]
    public string flyingDroneLore = "This is a drone made to protect the planet.";
    public Image flyingDroneImage = null;
    public float flyingDroneScanTime = 2f;
    [Header("Stationary Enemy")]
    public string stationaryEnemyLore = "This is a staionary robot made to defend certain areas of the world.";
    public Image stationaryEnemyImage = null;
    public float stationaryEnemyScanTime = 1f;
    [Header("Wheel Enemy")]
    public string wheelEnemyLore = "This rolling enemy tracks down enemy foes and tramples them to the ground.";
    public Image wheelEnemyImage = null;
    public float wheelEnemyScanTime = 4f;
}
