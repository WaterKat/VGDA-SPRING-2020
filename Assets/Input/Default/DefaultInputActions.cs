// GENERATED AUTOMATICALLY FROM 'Assets/Input/Default/DefaultInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""954c3dff-e870-49e0-bf79-1c4cb7525f91"",
            ""actions"": [
                {
                    ""name"": ""Move_X"",
                    ""type"": ""Value"",
                    ""id"": ""8d59928d-ed55-4137-a556-01eca7277d0f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move_Z"",
                    ""type"": ""Value"",
                    ""id"": ""24a28a19-703c-4f54-9fc7-34a63548817f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move_Y"",
                    ""type"": ""Value"",
                    ""id"": ""c5c27381-8404-4505-93a2-d12df2121ad4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim_X"",
                    ""type"": ""Value"",
                    ""id"": ""3fffd66e-68b2-49c1-b563-8c083ecb6f12"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim_Y"",
                    ""type"": ""Value"",
                    ""id"": ""e1cb5ab0-32cb-45ad-b7dd-a541ae95feed"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4d9af663-62a3-44fa-9596-45fe3902f916"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""ec6090ce-2339-4c66-bb65-d4b197c4d312"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""452bb2e9-8e18-40d0-ba15-3e9e87ab5249"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""abc4030a-a51b-4e9c-94db-3a0ec27e04f1"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3.5)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc248752-a44d-4a76-876e-915057c55b3b"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.125)"",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Aim_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d12e3870-c147-4079-a446-c44bb47d569e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41a2dea2-15f3-43bf-affb-72dc4c07bccb"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fc37cc5-93c1-4b4b-beff-0ac43e56abbc"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left/Right LH [Keyboard]"",
                    ""id"": ""d56a68d0-47ab-47b9-8eb5-65cc0bdecad4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_X"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7d3dcf2b-2cc0-4b25-955b-6d84edb310f0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""92c99286-8a8d-45b4-ab42-03fd2e46af4b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left/Right RH [Keyboard]"",
                    ""id"": ""88c5d437-4815-4200-9f98-6ef77d419455"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_X"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ecb93536-e188-4f33-8c21-317179df501a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2be05ac1-715c-4aed-b9e0-76acc41ff084"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""87a6f91b-05fe-452e-8f7d-380d2cf156d3"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move_Z"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left/Right LH [Keyboard]"",
                    ""id"": ""ed65ef06-d88c-4ce4-ae69-855787b174a4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_Z"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cc05b613-ead5-4ca2-80c0-f67b47a35ec4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_Z"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9aa7fcf4-05ae-42a3-b9c2-de99b8799e44"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_Z"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left/Right RH [Keyboard]"",
                    ""id"": ""ce815e4d-fbbc-44f7-b2b5-768381741f9c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_Z"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ab780631-0eaf-44ec-83c3-1c8388d36ffe"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_Z"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""04042a5e-a06a-420a-a139-020eff83375f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_Z"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b53810e3-856b-4fee-b4a2-92e7804a228f"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3.5)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e3bf7de-881c-49e8-99a8-c725ae937242"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.125)"",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Aim_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74c65918-148e-4ff0-abb8-eeb39c452e4d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d5a405d-1a7c-4686-b8f0-4a45ed2cc9d2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Shift/Space [Keyboard]"",
                    ""id"": ""3d7b03c5-0f13-4816-b0e3-071a149009c8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_Y"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""aecb6e8b-7f46-48f1-a4db-de8c93ebe209"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""10abaab3-80df-4672-9d32-a6c057b3d9a2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left/Right Bumper [Gamepad]"",
                    ""id"": ""bb1317ba-959b-4ac6-87c0-7022ef8e7ebf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_Y"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6ea61798-7d4b-4d51-a7b4-09727f1b26df"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""04d569dc-0c4b-40bb-9ba8-79ae7f8dfb31"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7bbeed7c-1caf-4dbb-ace2-149ec010fa75"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""3d169ee5-bb46-4776-b4e8-ac79682c5783"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""0ad73569-ea9f-432b-a99e-bf91e01871fb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""500e203e-2595-4725-a7e7-74fc54ac6555"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard_Mouse"",
            ""bindingGroup"": ""Keyboard_Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move_X = m_Gameplay.FindAction("Move_X", throwIfNotFound: true);
        m_Gameplay_Move_Z = m_Gameplay.FindAction("Move_Z", throwIfNotFound: true);
        m_Gameplay_Move_Y = m_Gameplay.FindAction("Move_Y", throwIfNotFound: true);
        m_Gameplay_Aim_X = m_Gameplay.FindAction("Aim_X", throwIfNotFound: true);
        m_Gameplay_Aim_Y = m_Gameplay.FindAction("Aim_Y", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Zoom = m_Gameplay.FindAction("Zoom", throwIfNotFound: true);
        m_Gameplay_Boost = m_Gameplay.FindAction("Boost", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move_X;
    private readonly InputAction m_Gameplay_Move_Z;
    private readonly InputAction m_Gameplay_Move_Y;
    private readonly InputAction m_Gameplay_Aim_X;
    private readonly InputAction m_Gameplay_Aim_Y;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Zoom;
    private readonly InputAction m_Gameplay_Boost;
    public struct GameplayActions
    {
        private @DefaultInputActions m_Wrapper;
        public GameplayActions(@DefaultInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move_X => m_Wrapper.m_Gameplay_Move_X;
        public InputAction @Move_Z => m_Wrapper.m_Gameplay_Move_Z;
        public InputAction @Move_Y => m_Wrapper.m_Gameplay_Move_Y;
        public InputAction @Aim_X => m_Wrapper.m_Gameplay_Aim_X;
        public InputAction @Aim_Y => m_Wrapper.m_Gameplay_Aim_Y;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Zoom => m_Wrapper.m_Gameplay_Zoom;
        public InputAction @Boost => m_Wrapper.m_Gameplay_Boost;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move_X.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_X;
                @Move_X.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_X;
                @Move_X.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_X;
                @Move_Z.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_Z;
                @Move_Z.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_Z;
                @Move_Z.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_Z;
                @Move_Y.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_Y;
                @Move_Y.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_Y;
                @Move_Y.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_Y;
                @Aim_X.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_X;
                @Aim_X.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_X;
                @Aim_X.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_X;
                @Aim_Y.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_Y;
                @Aim_Y.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_Y;
                @Aim_Y.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_Y;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Zoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                @Boost.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBoost;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move_X.started += instance.OnMove_X;
                @Move_X.performed += instance.OnMove_X;
                @Move_X.canceled += instance.OnMove_X;
                @Move_Z.started += instance.OnMove_Z;
                @Move_Z.performed += instance.OnMove_Z;
                @Move_Z.canceled += instance.OnMove_Z;
                @Move_Y.started += instance.OnMove_Y;
                @Move_Y.performed += instance.OnMove_Y;
                @Move_Y.canceled += instance.OnMove_Y;
                @Aim_X.started += instance.OnAim_X;
                @Aim_X.performed += instance.OnAim_X;
                @Aim_X.canceled += instance.OnAim_X;
                @Aim_Y.started += instance.OnAim_Y;
                @Aim_Y.performed += instance.OnAim_Y;
                @Aim_Y.canceled += instance.OnAim_Y;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Newaction;
    public struct UIActions
    {
        private @DefaultInputActions m_Wrapper;
        public UIActions(@DefaultInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_Keyboard_MouseSchemeIndex = -1;
    public InputControlScheme Keyboard_MouseScheme
    {
        get
        {
            if (m_Keyboard_MouseSchemeIndex == -1) m_Keyboard_MouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard_Mouse");
            return asset.controlSchemes[m_Keyboard_MouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove_X(InputAction.CallbackContext context);
        void OnMove_Z(InputAction.CallbackContext context);
        void OnMove_Y(InputAction.CallbackContext context);
        void OnAim_X(InputAction.CallbackContext context);
        void OnAim_Y(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
