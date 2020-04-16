using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerEnemy_Aim : MonoBehaviour
{
    public Transform gunTransform;
    public Vector3 dotComparison = Vector3.right;
    public float xClamp_0 = 0;
    public float xClamp_1 = 1;

    public float rotationSpeed = 45;

    private Transform targetPlayer;
    private Rigidbody targetPlayerRigidbody;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        targetPlayerRigidbody = targetPlayer.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Quaternion oldRotation = gunTransform.rotation;

        Quaternion newRotation = Quaternion.identity;

        float timeTillImpact = Vector3.Distance(targetPlayer.position, transform.position) / 200f;
        newRotation = Quaternion.LookRotation(targetPlayer.position+(targetPlayerRigidbody.velocity*timeTillImpact) - transform.position,transform.rotation*dotComparison);

        Vector3 newRotationForwards = newRotation * Vector3.forward;

        //Debug.Log(gunTransform.rotation.eulerAngles);

        if (Vector3.Dot(newRotationForwards, transform.rotation * dotComparison) < 0)
        {
            Vector3 forwardGun = Quaternion.Inverse(transform.rotation) * newRotationForwards;
            forwardGun.x = Mathf.Clamp(forwardGun.x, xClamp_0,xClamp_1);
            newRotation = Quaternion.LookRotation(transform.rotation * forwardGun, dotComparison);
//            gunTransform.rotation = oldRotation;
        }

        gunTransform.rotation = Quaternion.RotateTowards(oldRotation, newRotation, rotationSpeed);
    }
}
