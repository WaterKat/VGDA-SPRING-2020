using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.Player
{
    public class Movement : MonoBehaviour
    {
        Player CurrentPlayer;

        GameForce PlayerGameForce = GameForce.Zero;
        List<GameForce> LocalForceList = new List<GameForce>();

        //  float CharacterAngle;
        bool IsGrounded = false;
        public float speed = 10;

        private class GameForce
        {
            public Vector3 Direction;
            public float AccelerationTime;
            public float AntiMult;
            public Vector3 MaxVelocity;

            public static GameForce Zero
            {
                get
                {
                    GameForce TempGF = new GameForce
                    {
                        Direction = Vector3.zero,
                        AccelerationTime = 1f,
                        AntiMult = 1f,
                        MaxVelocity = Vector3.one
                    };
                    return TempGF;
                }
            }
        }

        private void Awake()
        {
            CurrentPlayer = GetComponent<Player>();

           // CurrentPlayer.InputActionMap.Player.Move.performed += ctx => ChangeVelocity(CurrentPlayer.InputActionMap.Player.Move.ReadValue<Vector2>());
        }

        private bool CheckIfGrounded()
        {
            Vector3 thing;
            IsGrounded = CurrentPlayer.CheckIfGrounded(out thing);
            return IsGrounded;
        }

        public PhysicMaterial Move;
        public PhysicMaterial Stay;

        /*
        private void ChangeVelocity(Vector2 inputVector2)
        {
            Vector3 Direction = new Vector3(inputVector2.x, 0, inputVector2.y);
            Vector3 DirectionalVelocity = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * Direction * speed);


            if (!CurrentPlayer.CheckIfGrounded())
            {
                GetComponent<Collider>().material = Move;
            }
            else
            {
                if (Direction.magnitude < 0.1f)
                {
                    GetComponent<Collider>().material = Stay;
                }
                else
                {
                    GetComponent<Collider>().material = Move;
                }
            }
            //Debug.Log("Moving");
            transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(DirectionalVelocity.x, transform.gameObject.GetComponent<Rigidbody>().velocity.y, DirectionalVelocity.z);
        }
        */

        
        private void Update()
        {
            bool Grounded = CurrentPlayer.CheckIfGrounded();

            float MovementVectorX = CurrentPlayer.InputActionMap.Gameplay.Move_X.ReadValue<float>();
            float MovementVectorY = CurrentPlayer.InputActionMap.Gameplay.Move_Y.ReadValue<float>();

            Vector3 Direction = new Vector3(MovementVectorX, 0, MovementVectorY);
            Vector3 DirectionalVelocity = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * Direction * speed);


            if (!CurrentPlayer.CheckIfGrounded())
            {
                GetComponent<Collider>().material = Move;
            }
            else
            {
                if (Direction.magnitude < 0.1f)
                {
                    GetComponent<Collider>().material = Stay;
                }
                else
                {
                    GetComponent<Collider>().material = Move;
                }
            }
            //Debug.Log("Moving");
            transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(DirectionalVelocity.x, transform.gameObject.GetComponent<Rigidbody>().velocity.y, DirectionalVelocity.z);
        }
        
    }
}