using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(CameraController))]
    public class RotatePlayer : MonoBehaviour
    {
        private Player currentPlayer;
        private CameraController currentCameraController;
        private Camera currentPlayerCamera;

        public GameObject main;

        private void Start()
        {
            currentPlayer = GetComponent<Player>();
            currentCameraController = GetComponent<CameraController>();
            currentPlayerCamera = currentCameraController.PlayerCamera;
        }
        void Update()
        {
            main.transform.LookAt(LookAtPoint());
        }

        public Vector3 LookAtPoint()
        {
            Ray CameraRay = new Ray();
            CameraRay.origin = currentPlayerCamera.transform.position + (Vector3.Project(currentPlayer.PlayerBody.transform.position - currentPlayerCamera.transform.position, currentPlayerCamera.transform.forward));
            CameraRay.direction = currentPlayerCamera.transform.forward;

            RaycastHit raycastHit;
            bool RaycastHitSomething = Physics.Raycast(CameraRay, out raycastHit, 100f, ~LayerMask.GetMask("Player"));
            if (RaycastHitSomething)
            {
                return raycastHit.point;
            }
            else
            {
                return currentPlayerCamera.transform.position + (currentPlayerCamera.transform.forward * 100f);
            }
        }
    }
}