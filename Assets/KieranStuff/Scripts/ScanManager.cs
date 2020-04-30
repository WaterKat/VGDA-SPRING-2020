using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    [Header("Flying Drone")]
    public string flyingDroneLore = "This is a drone made to protect the planet.";
    public Sprite flyingDroneImage = null;
    public float flyingDroneScanTime = 2f;
    [Header("Stationary Enemy")]
    public string stationaryEnemyLore = "This is a staionary robot made to defend certain areas of the world.";
    public Sprite stationaryEnemyImage = null;
    public float stationaryEnemyScanTime = 1f;
    [Header("Wheel Enemy")]
    public string wheelEnemyLore = "This rolling enemy tracks down enemy foes and tramples them to the ground.";
    public Sprite wheelEnemyImage = null;
    public float wheelEnemyScanTime = 4f;
    [Header("Health Canister")]
    public string healthPickupLore = "This essence of pure life restores health back to the player.";
    public Sprite healthPickupImage = null;
    public float healthPickupScanTime = 1f;
    [Header("Black Box")]
    public string blackBoxLore = "When you look closely, you realize that the black box is actually black.";
    public Sprite blackBoxImage = null;
    public float blackBoxScanTime = 5f;
}
