// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Controllers.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controllers : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controllers()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controllers"",
    ""maps"": [
        {
            ""name"": ""Left"",
            ""id"": ""6cda840b-0dcc-4b6d-9d4d-68a5cee628e6"",
            ""actions"": [
                {
                    ""name"": ""joystick"",
                    ""type"": ""Value"",
                    ""id"": ""87af59e3-f6da-429d-9a7d-7a3edc41fe3c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""triggerDown"",
                    ""type"": ""Button"",
                    ""id"": ""370a1c68-ddc7-4afb-a7a7-2ce97c63a025"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""triggerUp"",
                    ""type"": ""Button"",
                    ""id"": ""675a9611-524b-4f1d-bc7c-83f858bd2cdd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99360488-c823-41ec-ab28-25fe8e308571"",
                    ""path"": ""<XRController>{LeftHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""759350c4-aa52-4e56-a070-f0e05515f3be"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""triggerDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21b96f1e-55a6-411e-a84a-1e4c47c915bf"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""triggerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Right"",
            ""id"": ""5b2b4d36-3850-482f-afc2-eb5b4727c2f8"",
            ""actions"": [
                {
                    ""name"": ""joystick"",
                    ""type"": ""Value"",
                    ""id"": ""e93dcffc-1b9f-4a5e-9f59-b3539e0d332d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""triggerDown"",
                    ""type"": ""Button"",
                    ""id"": ""d305c616-35fb-4ae0-b2dc-f8ba1a26bde4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""triggerUp"",
                    ""type"": ""Button"",
                    ""id"": ""e66b2d6b-b597-4684-8d20-2d508a1ee791"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ae2d34e-4d40-42db-974f-71813e97bfaa"",
                    ""path"": ""<XRController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb4a271d-9fb2-4ca2-ba75-fff5e108a48c"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""triggerDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26fe2718-5cc0-4e75-81d7-78b6800efd7e"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""triggerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Left
        m_Left = asset.FindActionMap("Left", throwIfNotFound: true);
        m_Left_joystick = m_Left.FindAction("joystick", throwIfNotFound: true);
        m_Left_triggerDown = m_Left.FindAction("triggerDown", throwIfNotFound: true);
        m_Left_triggerUp = m_Left.FindAction("triggerUp", throwIfNotFound: true);
        // Right
        m_Right = asset.FindActionMap("Right", throwIfNotFound: true);
        m_Right_joystick = m_Right.FindAction("joystick", throwIfNotFound: true);
        m_Right_triggerDown = m_Right.FindAction("triggerDown", throwIfNotFound: true);
        m_Right_triggerUp = m_Right.FindAction("triggerUp", throwIfNotFound: true);
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

    // Left
    private readonly InputActionMap m_Left;
    private ILeftActions m_LeftActionsCallbackInterface;
    private readonly InputAction m_Left_joystick;
    private readonly InputAction m_Left_triggerDown;
    private readonly InputAction m_Left_triggerUp;
    public struct LeftActions
    {
        private @Controllers m_Wrapper;
        public LeftActions(@Controllers wrapper) { m_Wrapper = wrapper; }
        public InputAction @joystick => m_Wrapper.m_Left_joystick;
        public InputAction @triggerDown => m_Wrapper.m_Left_triggerDown;
        public InputAction @triggerUp => m_Wrapper.m_Left_triggerUp;
        public InputActionMap Get() { return m_Wrapper.m_Left; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LeftActions set) { return set.Get(); }
        public void SetCallbacks(ILeftActions instance)
        {
            if (m_Wrapper.m_LeftActionsCallbackInterface != null)
            {
                @joystick.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystick;
                @joystick.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystick;
                @joystick.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystick;
                @triggerDown.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerDown;
                @triggerDown.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerDown;
                @triggerDown.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerDown;
                @triggerUp.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerUp;
                @triggerUp.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerUp;
                @triggerUp.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerUp;
            }
            m_Wrapper.m_LeftActionsCallbackInterface = instance;
            if (instance != null)
            {
                @joystick.started += instance.OnJoystick;
                @joystick.performed += instance.OnJoystick;
                @joystick.canceled += instance.OnJoystick;
                @triggerDown.started += instance.OnTriggerDown;
                @triggerDown.performed += instance.OnTriggerDown;
                @triggerDown.canceled += instance.OnTriggerDown;
                @triggerUp.started += instance.OnTriggerUp;
                @triggerUp.performed += instance.OnTriggerUp;
                @triggerUp.canceled += instance.OnTriggerUp;
            }
        }
    }
    public LeftActions @Left => new LeftActions(this);

    // Right
    private readonly InputActionMap m_Right;
    private IRightActions m_RightActionsCallbackInterface;
    private readonly InputAction m_Right_joystick;
    private readonly InputAction m_Right_triggerDown;
    private readonly InputAction m_Right_triggerUp;
    public struct RightActions
    {
        private @Controllers m_Wrapper;
        public RightActions(@Controllers wrapper) { m_Wrapper = wrapper; }
        public InputAction @joystick => m_Wrapper.m_Right_joystick;
        public InputAction @triggerDown => m_Wrapper.m_Right_triggerDown;
        public InputAction @triggerUp => m_Wrapper.m_Right_triggerUp;
        public InputActionMap Get() { return m_Wrapper.m_Right; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RightActions set) { return set.Get(); }
        public void SetCallbacks(IRightActions instance)
        {
            if (m_Wrapper.m_RightActionsCallbackInterface != null)
            {
                @joystick.started -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystick;
                @joystick.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystick;
                @joystick.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystick;
                @triggerDown.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerDown;
                @triggerDown.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerDown;
                @triggerDown.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerDown;
                @triggerUp.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerUp;
                @triggerUp.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerUp;
                @triggerUp.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerUp;
            }
            m_Wrapper.m_RightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @joystick.started += instance.OnJoystick;
                @joystick.performed += instance.OnJoystick;
                @joystick.canceled += instance.OnJoystick;
                @triggerDown.started += instance.OnTriggerDown;
                @triggerDown.performed += instance.OnTriggerDown;
                @triggerDown.canceled += instance.OnTriggerDown;
                @triggerUp.started += instance.OnTriggerUp;
                @triggerUp.performed += instance.OnTriggerUp;
                @triggerUp.canceled += instance.OnTriggerUp;
            }
        }
    }
    public RightActions @Right => new RightActions(this);
    public interface ILeftActions
    {
        void OnJoystick(InputAction.CallbackContext context);
        void OnTriggerDown(InputAction.CallbackContext context);
        void OnTriggerUp(InputAction.CallbackContext context);
    }
    public interface IRightActions
    {
        void OnJoystick(InputAction.CallbackContext context);
        void OnTriggerDown(InputAction.CallbackContext context);
        void OnTriggerUp(InputAction.CallbackContext context);
    }
}
