using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0)
        {
            playerSM.ChangeState(playerSM.Movestate);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        Movement.SetVelocity_Zero(false);
    }

}
