using UnityEngine;

public class WallCimbState : AbilityState
{
    private float wallClimbTimer;
    private float wallGrabingTimer;

    public WallCimbState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
    }

    public override void Enter()
    {
        base.Enter();

        wallClimbTimer = 0;
        wallGrabingTimer = playerData.WallGrabTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        wallClimbTimer += Time.deltaTime;

        if (wallClimbTimer < playerData.WallclimbTime)
            return;


        if (!isFacingWall)
        {
            isAbilityDone = true;
        }
        else if (!CheckisGrabingtheWall())
        {
            isAbilityDone = true;
        }
        else if (InputMod.JumpInput)
        {
            InputMod.UsedJumpInput();
            playerSM.ChangeState(playerSM.Jumpstate);
        }
        else if (isGround && Movement.CurrentVelocity.y < 0.1f)
        {
            isAbilityDone = true;
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.Slidingspeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CheckisGrabingtheWall()
    {
        if (xInput != Movement.FacingDirection)
        {
            wallGrabingTimer -= Time.deltaTime;

            if (wallGrabingTimer <= 0)
                return false;
        }

        return true;
    }



}
