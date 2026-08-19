using UnityEngine;

public class AbilityState : BaseState
{

    protected bool isAbilityDone;
    protected bool isFacingWall;

    public AbilityState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Docheck()
    {
        base.Docheck();

        isFacingWall = Collision.isFacingWall();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {

            if (isGround && Movement.CurrentVelocity.y < 0.01f)
            {
                playerSM.ChangeState(playerSM.Idlestate);
            }
            else if (isFacingWall && xInput == Movement.FacingDirection)
            {
                playerSM.ChangeState(playerSM.WallCimbstate);
            }
            else
            {
                playerSM.ChangeState(playerSM.inAirstate);
            }
        }
    }
}
