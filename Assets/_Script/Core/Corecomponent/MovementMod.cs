using UnityEngine;

public class MovementMod : Coremodule
{

    public Rigidbody2D Rb { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    public bool CanSetVelocity { get; set; }

    public int FacingDirection { get; set; } = 1;

    [SerializeField] float distancedelta;
    private Vector2 workspace;


    public override void CoreAwake()
    {
        base.CoreAwake();

        Rb = GetComponentInParent<Rigidbody2D>();
        CanSetVelocity = true;
    }

    public override void CoreUpdate()
    {
        base.CoreUpdate();

        CurrentVelocity = Rb.linearVelocity;
    }

    public void SetVelocity_Zero(bool instant = true)
    {
        if (!instant)
        {
            workspace = Vector2.MoveTowards(CurrentVelocity, Vector2.zero, distancedelta);
            SetFinalVelocity();
        }
        else
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }
    }

    public void SetVelocityZero_ButOnlyX(bool instant = true)
    {
        if (!instant)
        {
            workspace.Set(Mathf.MoveTowards(CurrentVelocity.x, 0, distancedelta), 0);
            SetFinalVelocity();
        }
        else
        {
            workspace.Set(0, CurrentVelocity.y);
            SetFinalVelocity();
        }
    }

    public void SetVelocityZero_ButOnlyY(bool instant = true)
    {
        if (!instant)
        {
            workspace.Set(CurrentVelocity.x, Mathf.MoveTowards(CurrentVelocity.y, 0, distancedelta));
            SetFinalVelocity();
        }
        else
        {
            workspace.Set(CurrentVelocity.x, 0);
            SetFinalVelocity();
        }
    }

    public void SetVelocityX(float speed, float damping = 1)
    {
        workspace.Set(speed * damping, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float speed)
    {
        workspace.Set(CurrentVelocity.x, speed);
        SetFinalVelocity();
    }

    public void SetVelocity(float speed, Vector2 angle, int direction)
    {
        workspace.Set(angle.x * direction * speed, angle.y * speed);
        SetFinalVelocity();
    }


    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rb.linearVelocity = workspace;
            CurrentVelocity = workspace;
        }
    }


    public void CheckWhenToFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }


}
