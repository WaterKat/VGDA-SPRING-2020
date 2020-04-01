using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBotEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform playerTrans;

    [SerializeField]
    private float followDistance = 30f;
    [SerializeField]
    private float reChaseDistance = 30f;
    [SerializeField]
    private float reChaseRatio = 0.5f;
    private float chasingMinDistance;
    [SerializeField]
    private float baseSpeed = 15f;
    [SerializeField]
    private float rotationAroundPlayerSpeed = 12f;
    private float curSpeed;
    bool moving = false;

    [Header("Missile Stuff")]
    public GameObject missile;
    public Transform missileSpawnLoc;
    public float missileForce = 100f;
    public float attackDelay = 3f;
    private bool missileReady = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        curSpeed = baseSpeed;
        chasingMinDistance = reChaseDistance * reChaseRatio;
    }

    private void FixedUpdate()
    {
        if ((this.transform.position - playerTrans.position).magnitude < followDistance)
        {
            // Shoot at player
            if (missileReady)
            {
                ShootMissile();
            }

            Vector3 playerTransXZ = new Vector3(playerTrans.position.x, 0f, playerTrans.position.z);
            Vector3 thisTransXZ = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

            // Maintain looking at player when in range
            this.transform.LookAt(playerTrans);

            FollowPlayer(playerTransXZ, thisTransXZ);
        }
    }

    private void FollowPlayer(Vector3 playerXZ, Vector3 thisXZ)
    {
        Vector3 aerialOffset = new Vector3(0f, 8f, 0f);

        if((thisXZ - playerXZ).magnitude > reChaseDistance && !moving)
        {
            moving = true;
            curSpeed = baseSpeed;
        }

        if ((thisXZ - playerXZ).magnitude > chasingMinDistance)
        {
            if(moving)
            {
                rb.MovePosition(this.transform.position + (playerTrans.position + aerialOffset - this.transform.position).normalized * curSpeed * Time.fixedDeltaTime);

                // OUTDATED MOVEMENT
                /*transform.position = Vector3.MoveTowards(transform.position, playerTrans.position + aerialOffset, curSpeed * Time.deltaTime);*/
            }   
        }
        else
        {
            if(moving)
            {
                curSpeed = baseSpeed * 0.3f;
                moving = false;
            }
        }

        if(!moving)
        {
            // RANDOM DIRECTION ATTEMPT
            // Vector3 direction = Random.insideUnitCircle.normalized;

            rb.MovePosition(new Vector3(this.transform.position.x, playerTrans.position.y, this.transform.position.z) + aerialOffset + this.transform.right * curSpeed * Time.fixedDeltaTime);

            // OUTDATED MOVEMENT
            /*transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x, playerTrans.position.y, this.transform.position.z) + aerialOffset + this.transform.right, curSpeed * Time.deltaTime);*/
            /*this.transform.RotateAround(playerTrans.position, Vector3.up, rotationAroundPlayerSpeed * Time.deltaTime);*/
        }
    }

    void ShootMissile()
    {
        Debug.Log("MISSILE SHOT");
        GameObject tempMissile = Instantiate(missile, missileSpawnLoc.position, this.transform.rotation) as GameObject;
        Rigidbody tempMissileRb = tempMissile.GetComponent<Rigidbody>();
        /*tempMissileRb.AddForce(tempMissile.transform.forward * missileForce, ForceMode.Impulse);*/
        StartCoroutine(ReloadMissile());
    }

    IEnumerator ReloadMissile()
    {
        missileReady = false;
        yield return new WaitForSeconds(attackDelay + Random.Range(-0.5f, 0.5f));
        missileReady = true;
    }
}
