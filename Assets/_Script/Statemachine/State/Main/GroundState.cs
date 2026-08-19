using UnityEngine;

public class GroundState : BaseState
{
    protected bool dashinput;
    protected bool jumpInput;

    public GroundState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerSM.Jumpstate.ResetJump();
        playerSM.Dashstate.ResetDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (dashinput && playerSM.Dashstate.CheckwhenCanDash())
        {
            playerSM.ChangeState(playerSM.Dashstate);
        }
        else if (jumpInput && playerSM.Jumpstate.CanJump())
        {
            InputMod.UsedJumpInput();
            playerSM.ChangeState(playerSM.Jumpstate);
        }
        else if (!isGround)
        {
            playerSM.ChangeState(playerSM.inAirstate);
        }

    }

    public override void Docheck()
    {
        base.Docheck();

        jumpInput = InputMod.JumpInput;
        dashinput = InputMod.DashInput;
    }

}
