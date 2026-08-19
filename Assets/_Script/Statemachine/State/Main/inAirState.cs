using UnityEngine;

public class inAirState : BaseState
{

    private bool jumpinputStop;
    private bool isJumping;

    protected bool jumpInput;
    protected bool dashinput;
    protected bool isFacingWall;

    public inAirState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
    }

    public override void Docheck()
    {
        base.Docheck();

        dashinput = InputMod.DashInput;
        jumpInput = InputMod.JumpInput;
        jumpinputStop = InputMod.JumpinputStop;
        isFacingWall = Collision.isFacingWall();
        CheckMultiplier();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isGround && Movement.CurrentVelocity.y < 0.01f)
        {
            if (xInput != 0)
            {
                playerSM.ChangeState(playerSM.Movestate);
            }
            else
            {
                playerSM.ChangeState(playerSM.Idlestate);
            }
        }
        else if (dashinput && playerSM.Dashstate.CheckwhenCanDash())
        {
            playerSM.ChangeState(playerSM.Dashstate);
        }
        else if (isFacingWall && xInput == Movement.FacingDirection)
        {
            playerSM.ChangeState(playerSM.WallCimbstate);
        }
        else if (jumpInput && playerSM.Jumpstate.CanJump())
        {
            InputMod.UsedJumpInput();
            Artdepartment.PlayParticle(playerData.AirJumpDust, Artdepartment.Particleposition.position, Artdepartment.Particleposition.rotation);
            playerSM.ChangeState(playerSM.Jumpstate);
        }
        else
        {
            Movement.CheckWhenToFlip(xInput);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        if (xInput != 0)
            Movement.SetVelocityX(playerData.MoveSpeed * xInput);
    }

    public override void PlayAnimation()
    {
        if (playerSM.PreviousState == playerSM.Jumpstate)
        {
            Artdepartment.Anim.Play("JumptoFall.JumptoFall");
        }
        else
        {
            base.PlayAnimation();
        }
    }


    private void CheckMultiplier()
    {
        if (isJumping)
        {
            if (jumpinputStop)
            {
                Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.VariableJumpHeight);
                isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public void SetisJumping() => isJumping = true;
}
