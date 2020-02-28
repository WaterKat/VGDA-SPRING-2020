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
    private Transform _player;
    //[SerializeField]
    private Transform player
    {
        get
        {
            if (_player == null)
            {
                _player = FindPlayer();
            }
            return _player;
        }
    }
    [SerializeField]
    private GameObject bullet;

    bool gunReady = true;
    float distanceFromPlayer;
    float attackRange = 60f;

    float attackDelay = 2f;
    float gunForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
    }

    Transform FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform;
        }
        else 
        { 
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if(distanceFromPlayer < attackRange)
        {
            cannon.transform.LookAt(player);
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
        tempBulletRb.AddForce(tempBullet.transform.forward * gunForce, ForceMode.Impulse);
        StartCoroutine(ReloadCannon());
    }

    IEnumerator ReloadCannon()
    {
        gunReady = false;
        yield return new WaitForSeconds(attackDelay);
        gunReady = true;
    }
}
