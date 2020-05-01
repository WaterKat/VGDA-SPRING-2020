using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnLoc;
    [SerializeField]
    private GameObject cannon;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private GameObject bullet;

    bool gunReady = true;
    float distanceFromPlayer;
    [SerializeField]
    float attackRange = 60f;

    float attackDelay = 2f;
    [SerializeField]
    float gunForce = 200f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerRigidbody = player.GetComponent<Rigidbody>();

        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if(distanceFromPlayer < attackRange)
        {
            //cannon.transform.LookAt(player);
            cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, Quaternion.LookRotation(player.transform.position - cannon.transform.position), 2.5f);
            if(gunReady)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        float timeTillHit = Vector3.Distance(player.position+ playerRigidbody.velocity, cannon.transform.position) / gunForce;
        Vector3 target = player.transform.position + (playerRigidbody.velocity * timeTillHit);
        //target += Random.insideUnitSphere * Mathf.Clamp(Vector3.Distance(player.position + playerRigidbody.velocity, cannon.transform.position), 0, 5);
        Vector3 shotDirection = (target-cannon.transform.position).normalized;

        GameObject tempBullet = Instantiate(bullet, bulletSpawnLoc.transform.position, Quaternion.LookRotation(shotDirection)) as GameObject;
        Rigidbody tempBulletRb = tempBullet.GetComponent<Rigidbody>();
        // tempBulletRb.AddForce(tempBullet.transform.forward * gunForce, ForceMode.Impulse);
        tempBullet.transform.forward = shotDirection;
        tempBulletRb.AddForce(shotDirection * gunForce, ForceMode.Impulse);
        WaterKat.Audio.AudioManager.PlaySound("TurretShoot");
        StartCoroutine(ReloadCannon());
    }

    IEnumerator ReloadCannon()
    {
        gunReady = false;
        yield return new WaitForSeconds(attackDelay + Random.Range(-0.5f,0.5f));
        gunReady = true;
    }
}
