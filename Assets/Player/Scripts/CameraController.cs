using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WaterKat.TimeKeeping;


namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    public class CameraController : MonoBehaviour
    {
        public Player CurrentPlayer;        //Reference to Player Class

        private InputAction Aim_XInput;     //Reference to the InputAction (Mouse_X) class from the new InputSystem
        private InputAction Aim_YInput;     //Reference to the InputAction (Mouse_Y) class from the new InputSystem

        private InputAction ZoomInput;      //Reference to the InputAction (Right Click) class from the new InputSystem


        public Camera PlayerCamera;         //Reference to the camera inside the Player prefab (can be accessed with Camera.main as well but this allows for more than 1 camera)
        public Quaternion CameraQuaternion  //This returns a camera rotation without any modification to the Z axis
        {
            get
            {
                return Quaternion.Euler(CameraRotation.y, CameraRotation.x, 0);
            }
        }

        public CameraData CameraDataA;      //First Camera Mode Template (Flight Mode)
        public CameraData CameraDataB;      //Second Camera Mode Template (Shooting Mode)

        [Range(0,1)]
        public float CameraTransition = 0;  //This is at point the camera is transitioning between templates
        public bool CameraTransitioning = true;         //This is a bool that determines whether or not the camera is being transitioned to mode 1. If it is, then new transitions shouldn't be able to start

        public Vector2 CameraRotation = Vector2.zero;   //This is the total camera rotation from Quaternion.Identity in degrees on X and Y Axis. the Z axis is ignored atm
        public float CameraDistance = 1;                //This is the currently deprecated Camera Distance feature that would allow for zooming in within the same camera mode.


        public GameObject Reticle;                      //This is the reticle game object that only appears currently in camera mode 1 (Shooting over the shoulder)


        private void Awake()                            //This gets references for Player,and the Aim_(X/Y) and Zoom Input Action classes, and also adds an event to the Zoom Input action 
        {
            CurrentPlayer = GetComponent<Player>(); 

            Aim_XInput = CurrentPlayer.InputActionMap.Gameplay.Aim_X;
            Aim_YInput = CurrentPlayer.InputActionMap.Gameplay.Aim_Y;

            ZoomInput = CurrentPlayer.InputActionMap.Gameplay.Zoom;
            ZoomInput.started += ctx => ToggleTransition();
        }

        private void Start()
        {
            ToggleTransition();                         //Just in case the camera isnt adjusted to a mode, this will start a transition into mode 1
            Cursor.lockState = CursorLockMode.Locked;   //Makes sure cursor cant leave the window
            Cursor.visible = false;                     //Makes the cursor invisible;
        }

        Ticker TransitionTicker = new Ticker();         //This is my self made Ticker() class that will only return true if the timer has been met 

        void ToggleTransition()
        {   
            CameraTransitioning = !CameraTransitioning; //If the camera is transitioning cancel the transition, if the camera isn't transitioning, start the transition
            TransitionTicker.ResetTick();               //Reset the Timer on the Ticker;
            StopCoroutine(TransitionCamera());          //If there's a transition coroutine running, cancel it
            StartCoroutine(TransitionCamera());         //Start a new transition Coroutine 
        }

        public IEnumerator TransitionCamera()
        {
            while (!TransitionTicker.TryTick())         //While the ticker has not been set off, repeat
            {
                if (CameraTransitioning)                //If We're trying to transition to mode A then subtract from the transition float
                {
                    CameraTransition += -4f*Time.deltaTime;
                }
                else                                   //If We're trying to transition to mode B then add to the transition float
                {
                    CameraTransition += 4f*Time.deltaTime;
                }
                CameraTransition = Mathf.Clamp(CameraTransition, 0, 1); //Clamps the value in case it is out of bounds (0-1)

                yield return new WaitForEndOfFrame();
            }
        }

        private void LateUpdate()
        {
            float LocalTransition = CameraTransition;   // Kinda unnecessary, but it creates a copy of the transition (just in case we wanted to modify it)

            CameraRotation.x = Mathf.Repeat(CameraRotation.x + 360f, 720f) - 360f;  //This makes sure that the camera rotation stays within the bounds of -360 to 360, otherwise it would rotate 1000 degrees or more
            CameraRotation.y = Mathf.Repeat(CameraRotation.y + 360f, 720f) - 360f;

            Vector2 LerpedSensitivity = Vector2.Lerp(CameraDataA.CameraRotationSensitivity, CameraDataB.CameraRotationSensitivity, LocalTransition); //This creates a lerped mouse/stick sensitiviy value so the camera reacts differently based on the template

            CameraRotation.x += Aim_XInput.ReadValue<float>() * LerpedSensitivity.x;        //This takes the lerped sensitivity and multiplies it with the input from the input manager (MouseX/Y or right stick)
            CameraRotation.y += -Aim_YInput.ReadValue<float>() * LerpedSensitivity.y;

            /* Currently Disabled (Gamepad doesn't have an easy zoom feature)
            float LerpedDistanceSensitivity = Mathf.Lerp(CameraDataA.CameraDistanceSensitivity, CameraDataB.CameraDistanceSensitivity, LocalTransition);    //This gets a lerped sensitivity value for the scroll wheel

           CameraDistance += -Input.mouseScrollDelta.y * LerpedDistanceSensitivity; //This uses the scroll wheel to change camera distance
            */       

            //This Lerps the rotation bounds between the templates

            //Vector2 LerpedXBounds = Vector2.Lerp(CameraDataA.CameraRotationXBounds, CameraDataB.CameraRotationXBounds, LocalTransition);
            Vector2 LerpedYBounds = Vector2.Lerp(CameraDataA.CameraRotationYBounds, CameraDataB.CameraRotationYBounds, LocalTransition);    //usually between -90,90 This stops the camera from rotating past reasonable bounds set in the templates
            //Vector2 LerpedZBounds = Vector2.Lerp(CameraDataA.CameraDistanceBounds, CameraDataB.CameraDistanceBounds, LocalTransition);

            //This clamps the Y rotation between bounds

            //CameraRotation.x = Mathf.Clamp(CameraRotation.x, LerpedXBounds.x, LerpedXBounds.y);   //We want a full 360 x movement currently, thiss is useful if we want to restrict X rotation
            CameraRotation.y = Mathf.Clamp(CameraRotation.y, LerpedYBounds.x, LerpedYBounds.y);     
            //CameraDistance = Mathf.Clamp(CameraDistance, LerpedZBounds.x, LerpedZBounds.y);
                       
                //Moved some code to a new method to Lerp Camera Component values
                CameraLerp(CameraDataA.TemplateCamera, CameraDataB.TemplateCamera, LocalTransition, PlayerCamera);

            Vector3 LerpedCameraPositionPreRotation = Vector3.Lerp(CameraDataA.transform.localPosition, CameraDataB.transform.localPosition, LocalTransition);      //Finally this gets the lerped local position between camera templates
            Quaternion LerpedCameraRotationPreRotation = Quaternion.Lerp(CameraDataA.transform.localRotation, CameraDataB.transform.localRotation, LocalTransition);//And this gets the lerped local rotations between camera templates.

            //PlayerCamera.transform.localPosition = PlayerCamera.transform.localPosition * CameraDistance;     //Distance is currently disabled
            PlayerCamera.transform.localPosition = Quaternion.Euler(CameraRotation.y,CameraRotation.x,0) * LerpedCameraPositionPreRotation; //This finally rotates the local position by our desired position from the mouse/axis
            PlayerCamera.transform.rotation = LerpedCameraRotationPreRotation * Quaternion.Euler(CameraRotation.y, CameraRotation.x, 0);    //This rotates any preset rotation by the input mouse/axis rotation

            Reticle.SetActive(CameraTransition == 0);   //if the camera mode is shooting mode then turn on the reticle;
        }

        public void CameraLerp(Camera CameraA, Camera CameraB, float input, Camera TargetCamera)    //This lerps camera component values (field of view, nearclipplane,farclipplane)
        {
            TargetCamera.fieldOfView = Mathf.Lerp(CameraA.fieldOfView, CameraB.fieldOfView, input);
            TargetCamera.nearClipPlane = Mathf.Lerp(CameraA.nearClipPlane, CameraB.nearClipPlane, input);
            TargetCamera.farClipPlane = Mathf.Lerp(CameraA.farClipPlane, CameraB.farClipPlane, input);
        }

        /*  DEBUG CODE
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawRay(new Ray(transform.position,transform.forward*5));
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
        }
        */
    }
}