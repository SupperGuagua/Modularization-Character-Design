using UnityEngine;
using UnityEngine.InputSystem;

public class InputMod : Coremodule
{
    public int NorInputX { get; private set; }

    public bool DashInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpinputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float JumpInputStartime;

    public override void CoreUpdate()
    {
        base.CoreUpdate();

        CheckJumpInputHoldTime();
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        NorInputX = Mathf.RoundToInt(context.ReadValue<Vector2>().x);
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
            DashInput = true;

        if (context.canceled)
            DashInput = false;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStartime = Time.time;
            JumpinputStop = false;
        }

        if (context.canceled)
        {
            JumpinputStop = true;
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= JumpInputStartime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void UsedJumpInput() => JumpInput = false;
}
