using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBotMissile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private Transform playerTrans = null;
    [SerializeField]
    private int missileDamage = 3;
    [SerializeField]
    private float missileSpeed = 10f;
    [SerializeField]
    private float nonTrackingRatio = 0.6f;

    private float nonTrackingDistance;

    private bool tracking = true;

    float distanceFromPlayerOnSpawn;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerTrans = playerHealth.GetComponent<Transform>();

        distanceFromPlayerOnSpawn = Vector3.Distance(this.transform.position, playerTrans.position);
        nonTrackingDistance = distanceFromPlayerOnSpawn * (nonTrackingRatio);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DealDamage();
        }
        Destroy(this.gameObject);
    }

    private void DealDamage()
    {
        playerHealth.curHealth -= missileDamage;
    }

    private void Update()
    {
        if(nonTrackingDistance > Vector3.Distance(this.transform.position, playerTrans.position) && tracking)
        {
            tracking = false;
        }

        if (tracking)
        {
            this.transform.LookAt(playerTrans);
            transform.position = Vector3.MoveTowards(this.transform.position, playerTrans.position, missileSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(this.transform.position, transform.position + transform.forward, missileSpeed * Time.deltaTime);
        }
    }
}
