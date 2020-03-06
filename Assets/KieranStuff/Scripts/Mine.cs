using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    [SerializeField]
    private float explosionForce = 10f;

    [SerializeField]
    private int mineDamage = 2;

    [SerializeField]
    private float mineRadius = 8f;

    [SerializeField]
    private float explosionDelay = 1f;

    public Rigidbody playerRb;
    public PlayerHealth playerHealth;

    [SerializeField]
    private Material triggeredMat;

    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerHealth = playerRb.GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(ExplodeTimer());
        }
    }

    IEnumerator ExplodeTimer()
    {
        this.GetComponent<MeshRenderer>().material = triggeredMat;
        yield return new WaitForSeconds(explosionDelay);

        // Explode effects / particles
        Explode();
    }

    private void Explode()
    {
        float distanceFromPlayer = Vector3.Distance(this.transform.position, playerHealth.transform.position);

        if(distanceFromPlayer < mineRadius)
        {
            playerHealth.TakeDamage(mineDamage);
            playerRb.AddForce((playerRb.transform.position - this.transform.position) * explosionForce, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
