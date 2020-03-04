using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    public class AimGun : MonoBehaviour
    {
        private Player CurrentPlayer;
        private CameraController CurrentCameraController;
        private Camera PlayerCamera;
        private InputAction ShootInput;

        public GameObject Gun;
        public GameObject GunTemplate;

        public GameObject BulletTemplate;
        public float BulletShootDistance = 1;
        public float BulletSpeed = 10;

        private void Awake()
        {
            CurrentPlayer = GetComponent<Player>();
            ShootInput = CurrentPlayer.InputActionMap.Gameplay.Shoot;
            ShootInput.started += ctx => ShootGun();
        }

        private void Start()
        {
            CurrentCameraController = GetComponent<CameraController>();
            PlayerCamera = CurrentCameraController.PlayerCamera;
        }

        void ShootGun()
        {
            GameObject newBullet = Instantiate(BulletTemplate);
            newBullet.transform.position = Gun.transform.position + (Gun.transform.forward*BulletShootDistance);
            newBullet.transform.rotation = Gun.transform.rotation;
            newBullet.GetComponent<Rigidbody>().velocity = Gun.transform.forward * BulletSpeed;
            newBullet.SetActive(true);
        }

        private void Update()
        {
            Gun.transform.localPosition = CurrentCameraController.CameraQuaternion * GunTemplate.transform.localPosition;
            Gun.transform.LookAt(LookAtPoint());
        }

        public Vector3 LookAtPoint()
        {
            Ray CameraRay = new Ray();
            CameraRay.origin = PlayerCamera.transform.position + (Vector3.Project(CurrentPlayer.PlayerBody.transform.position - PlayerCamera.transform.position, PlayerCamera.transform.forward));
            CameraRay.direction = PlayerCamera.transform.forward;

            RaycastHit raycastHit;
            bool RaycastHitSomething = Physics.Raycast(CameraRay, out raycastHit, 100f, ~LayerMask.GetMask("Player"));
            if (RaycastHitSomething)
            {
                return raycastHit.point;
            }
            else
            {
                return PlayerCamera.transform.position + (PlayerCamera.transform.forward * 100f);
            }
        }
    }
}