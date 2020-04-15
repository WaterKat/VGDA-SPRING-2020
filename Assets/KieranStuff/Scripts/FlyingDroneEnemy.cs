using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlyingDroneEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform playerTrans;

    private Vector3 originPos;
    private Vector3 randWanderPos;
    private float wanderSpeed = 5f;
    private float wanderRadius = 10f;
    private bool aiWalking = true;

    bool playerAggro = false;
    float aggroDistance = 50f;

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

        Debug.Log("ORIGIN: " + originPos);
        Debug.Log("RANDOM: " + randWanderPos);
    }

    private void FixedUpdate()
    {
        if(playerAggro == false)
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
        Debug.Log("MISSILE SHOT");
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
