using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IHeroActions
{
    public Vector2 MovementValue { get; private set; }
    public bool HoverButtonDown { get; private set; }

    public event Action JumpEvent;
    public event Action DashEvent;
    public event Action HoverEvent;

    private Controls controls;


    private void Start()
    {
        controls = new Controls();
        controls.Hero.SetCallbacks(this);

        controls.Hero.Enable();

        HoverButtonDown = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context) {}

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        JumpEvent?.Invoke();
    }

    private void OnDestroy()
    {
        controls.Hero.Disable();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DashEvent?.Invoke();
    }

    public void OnHover(InputAction.CallbackContext context)
    {
        HoverButtonDown = context.performed;
        
        if (!context.performed)  return;
        
        HoverEvent?.Invoke();
    }
}
