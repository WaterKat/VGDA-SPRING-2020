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
    private GameObject bullet;

    bool gunReady = true;
    float distanceFromPlayer;
    float attackRange = 60f;

    float attackDelay = 2f;
    float gunForce = 150f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if(distanceFromPlayer < attackRange)
        {
            //cannon.transform.LookAt(player);
            cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, Quaternion.LookRotation(player.transform.position - cannon.transform.position), 5);
            if(gunReady)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject tempBullet = Instantiate(bullet, bulletSpawnLoc.transform.position, cannon.transform.rotation) as GameObject;
        Rigidbody tempBulletRb = tempBullet.GetComponent<Rigidbody>();
        // tempBulletRb.AddForce(tempBullet.transform.forward * gunForce, ForceMode.Impulse);
        tempBullet.transform.forward = (player.transform.position - cannon.transform.position).normalized;
        tempBulletRb.AddForce((player.transform.position-cannon.transform.position).normalized * gunForce, ForceMode.Impulse);
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
