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

    private Vector3 moveDir;
    private bool changingMoveDir = false;
    private float switchMoveDirDelay = 2f;

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

            Vector3 playerTransXYZ = new Vector3(playerTrans.position.x, playerTrans.position.y, playerTrans.position.z);
            Vector3 thisTransXYZ = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

            // Maintain looking at player when in range
            this.transform.LookAt(playerTrans);

            FollowPlayer(playerTransXYZ, thisTransXYZ);
        }
    }

    private void FollowPlayer(Vector3 playerXYZ, Vector3 thisXYZ)
    {
        Vector3 aerialOffset = new Vector3(0f, 5f, 0f);

        if((thisXYZ - playerXYZ).magnitude > reChaseDistance && !moving)
        {
            moving = true;
            curSpeed = baseSpeed;
        }

        if ((thisXYZ - playerXYZ).magnitude > chasingMinDistance)
        {
            if(moving)
            {
                rb.MovePosition(this.transform.position + (aerialOffset + playerTrans.position - this.transform.position).normalized * Time.fixedDeltaTime * curSpeed);

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
            if(changingMoveDir == false)
            {
                StartCoroutine(SwitchMoveDirection());
            }

            rb.MovePosition(this.transform.position + moveDir.normalized * curSpeed * Time.fixedDeltaTime);

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

    IEnumerator SwitchMoveDirection()
    {
        changingMoveDir = true;
        moveDir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0f);
        yield return new WaitForSeconds(switchMoveDirDelay);
        changingMoveDir = false;
    }
}
