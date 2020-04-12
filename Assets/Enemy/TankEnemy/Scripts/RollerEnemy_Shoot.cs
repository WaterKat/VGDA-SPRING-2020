using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeKeeping;

public class RollerEnemy_Shoot : MonoBehaviour
{
    public Transform gunTransform;
    public float forwardOffset = 5;

    public GameObject bullet;
    public float bulletSpeed = 200;

    public float timeBetweenShots = 1;
    public float initialOffset = 0;

    private Transform currentPlayer;

    private Ticker shotTicker = new Ticker();

    void Start()
    {
        shotTicker.MaxTick = timeBetweenShots;
        currentPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (shotTicker.TryTick())
        {
            GameObject newBullet = Instantiate(bullet);
            bullet.GetComponent<Rigidbody>().velocity = gunTransform.forward * bulletSpeed;
            bullet.transform.position = gunTransform.position + gunTransform.forward * forwardOffset;
            bullet.SetActive(true);
        }
    }
}
