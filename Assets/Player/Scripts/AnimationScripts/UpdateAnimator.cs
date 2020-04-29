using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;
public class UpdateAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private Rigidbody currentRigidbody;
    private WaterKat.Player_N.Player currentPlayer;
    private WaterKat.Player_N.CameraController currentCameraController;
    void Start()
    {
        //animator = GetComponent<Animator>();
        currentRigidbody = GetComponent<Rigidbody>();
        currentPlayer = GetComponent<WaterKat.Player_N.Player>();

        currentCameraController = GetComponent<WaterKat.Player_N.CameraController>();
    }
    void Update()
    {
        float velMagnitude = currentRigidbody.velocity.magnitude;
        Vector3 normVel = Quaternion.Inverse(Quaternion.Euler(0,currentCameraController.CameraRotation.x,0)) * (Vector3.ClampMagnitude(currentRigidbody.velocity,30f)/10f);
        float xzVelocityMagnitude = Mathf.Abs(new Vector2(currentRigidbody.velocity.x, currentRigidbody.velocity.z).magnitude);
        animator.SetFloat("Velocity_X", normVel.x);
        animator.SetFloat("Velocity_Y", normVel.y);
        animator.SetFloat("Velocity_Z", normVel.z);
        animator.SetFloat("Velocity_M", velMagnitude);
        animator.SetFloat("Velocity_XZM", Mathf.Min(xzVelocityMagnitude,10)/10);


        animator.SetBool("Grounded", currentPlayer.Grounded);

        if (currentPlayer.Grounded)
        {

            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.Euler(0, currentCameraController.CameraRotation.x, 0), 360);
        }
        else
        {
            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, Quaternion.LookRotation(new Vector3(currentRigidbody.velocity.x,Mathf.Clamp(currentRigidbody.velocity.y/10f,-10f,10f),currentRigidbody.velocity.z)), 360);

        }
    }
}
