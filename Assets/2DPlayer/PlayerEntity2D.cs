using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player2D
{
    public class PlayerEntity2D : MonoBehaviour
    {
        public DefaultInputActions PlayerInputActions;

        private void Awake()
        {
            PlayerInputActions = new DefaultInputActions();
        }

        private void OnEnable()
        {
            PlayerInputActions.Gameplay.Enable();
        }
        private void OnDisable()
        {
            PlayerInputActions.Gameplay.Disable();   
        }
    }
}