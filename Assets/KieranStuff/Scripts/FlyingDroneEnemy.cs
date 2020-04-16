using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlyingDroneEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform playerTrans;

    [Header("Un-Aggroed Mode")]
    private Vector3 originPos;
    private Vector3 randWanderPos;
    private float wanderSpeed = 5f;
    private float wanderRadius = 10f;
    private bool aiWalking = true;
    private bool playerAggro = false;
    public float detectRadius = 100f;

    [Header("Aggroed Mode")]
    public float chaseSpeed = 10f;
    public float circlingSpeed = 3f;
    public float stopRadius = 20f;
    private Vector2 randomVector2;
    private Vector3 randomRotationalVector;
    private bool gettingRotationalVector = false;

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

        originPos = this.transform.position;
        randWanderPos = new Vector3(originPos.x + Random.Range(-wanderRadius, wanderRadius), originPos.y, originPos.z + Random.Range(-wanderRadius, wanderRadius));
        this.transform.LookAt(randWanderPos);

        randomVector2 = Random.insideUnitCircle;
        randomRotationalVector = new Vector3(randomVector2.x, randomVector2.y, 0f);
    }

    private void FixedUpdate()
    {
        if((rb.position - playerTrans.position).magnitude > detectRadius)
        {
            BasicAI();
        }
        else
        {
            FollowPlayer();
        }
    }

    void ShootMissile()
    {
        GameObject tempMissile = Instantiate(missile, missileSpawnLoc.position, this.transform.rotation) as GameObject;
        Rigidbody tempMissileRb = tempMissile.GetComponent<Rigidbody>();
        StartCoroutine(ReloadMissile());
    }

    IEnumerator ReloadMissile()
    {
        missileReady = false;
        yield return new WaitForSeconds(attackDelay + Random.Range(-0.5f, 0.5f));
        missileReady = true;
    }

    private void BasicAI()
    {
        if ((rb.position - randWanderPos).magnitude < 1f && aiWalking) {
            StartCoroutine(ChooseNewPosAndWait());
        }
        else
        {
            if(aiWalking)
            {
                rb.MovePosition(this.transform.position + ((randWanderPos - this.transform.position).normalized * wanderSpeed * Time.fixedDeltaTime));
            }
        }
    }

    private void FollowPlayer()
    {
        if(missileReady)
        {
            ShootMissile();
        }
        this.transform.LookAt(playerTrans.position);

        if ((rb.position - playerTrans.position).magnitude > stopRadius)
        {
            MoveTowardPlayer();
        }
        else
        {
            if(!gettingRotationalVector)
            {
                StartCoroutine(GetNewRotationalVector());
            }

            rb.MovePosition(rb.position + randomRotationalVector * circlingSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator GetNewRotationalVector()
    {
        gettingRotationalVector = true;
        yield return new WaitForSeconds(2f);
        randomVector2 = Random.insideUnitCircle;
        randomRotationalVector = new Vector3(randomVector2.x, randomVector2.y, 0f);
        gettingRotationalVector = false;
    }
    private void MoveTowardPlayer()
    {
        rb.MovePosition(this.transform.position + (playerTrans.position - rb.position).normalized * chaseSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator ChooseNewPosAndWait()
    {
        aiWalking = false;
        yield return new WaitForSeconds(2f);
        randWanderPos = new Vector3(originPos.x + Random.Range(-wanderRadius, wanderRadius), originPos.y, originPos.z + Random.Range(-wanderRadius, wanderRadius));
        this.transform.LookAt(randWanderPos);
        aiWalking = true;
    }
}
