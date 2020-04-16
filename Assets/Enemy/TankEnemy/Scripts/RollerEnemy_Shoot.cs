using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeKeeping;

public class RollerEnemy_Shoot : MonoBehaviour
{
    public Transform gunTransform;
    public float forwardOffset = 5;

    public GameObject bullet;
    [SerializeField]
    public float bulletSpeed = 200;

    public float timeBetweenShots = 1;
    public float initialOffset = 0;

    private Transform currentPlayer;

    private Ticker shotTicker = new Ticker();

    [SerializeField]
    private float maxTargetDistance = 100;
    void Start()
    {
        shotTicker.MaxTick = timeBetweenShots;
        currentPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentPlayer.position) > maxTargetDistance)
        {
            return;
        }
        if (shotTicker.TryTick())
        {
            GameObject newBullet = Instantiate(bullet);
            
            newBullet.transform.position = gunTransform.position + (gunTransform.forward * forwardOffset);
            newBullet.transform.forward = gunTransform.forward;

            newBullet.GetComponent<Rigidbody>().velocity = gunTransform.forward * bulletSpeed;
            
            newBullet.SetActive(true);
        }
    }
}
