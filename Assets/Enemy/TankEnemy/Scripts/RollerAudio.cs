using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Enemy_N;
public class RollerAudio : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    Enemy enemy;
    [SerializeField]
    Rigidbody rigidbody;

    [SerializeField]
    float _volume = 1;

    float volume
    {
        get
        {
            return _volume;
        }
        set
        {
            _volume = Mathf.Clamp(0, 1, _volume);
        }
    }
    [SerializeField]
    float volumeChange = 1;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemy = GetComponent<Enemy>();
        rigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //volume += Time.deltaTime * volumeChange*-1f;
        audioSource.volume = volume * Mathf.Min(rigidbody.velocity.magnitude/10f,1);
        volume = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Terrain")
        {
            //volume += Time.deltaTime * volumeChange*2f;
            volume = 1;
        }
    }
}
