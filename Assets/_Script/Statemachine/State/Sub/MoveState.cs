using UnityEngine;
using UnityEngine.UIElements;

public class MoveState : GroundState
{

    public MoveState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
        Artdepartment.OnAnimationTrigger += AnimationTrigger;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.CheckWhenToFlip(xInput);

        if (xInput == 0)
        {
            playerSM.ChangeState(playerSM.Idlestate);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        Movement.SetVelocityX(playerData.MoveSpeed * xInput);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Artdepartment.PlayParticle(playerData.RunDust, Artdepartment.Particleposition.position, playerSM.transform.rotation);
        Artdepartment.PlaySoundEffect(playerData.WalkingSFX);
    }
}
