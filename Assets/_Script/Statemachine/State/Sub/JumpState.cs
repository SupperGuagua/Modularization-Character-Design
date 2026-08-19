using UnityEngine;

public class JumpState : AbilityState
{

    private float startTime;

    private int leftJumpTimes;
    private int LeftJumpTimes
    {
        get => leftJumpTimes;
        set => leftJumpTimes = Mathf.Max(value, 0);
    }

    private bool isWalljumping = false;

    public JumpState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
        LeftJumpTimes = playerData.NumberOfJump;
    }

    public bool CanJump()
    {
        if (LeftJumpTimes > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Enter()
    {
        base.Enter();

        startTime = Time.time;
        Debug.Log(LeftJumpTimes);
        playerSM.inAirstate.SetisJumping();
        Artdepartment.PlaySoundEffect(playerData.JumpSFX);
        Artdepartment.PlayParticle(playerData.JumpDust, Artdepartment.Particleposition.position, Artdepartment.Particleposition.rotation);

        if (playerSM.PreviousState == playerSM.WallCimbstate)
        {
            WallJump();
        }
        else
        {
            Jump();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isWalljumping)
            if (Time.time >= startTime + playerData.WalljumpTime)
                isAbilityDone = true;
    }

    public override void Exit()
    {
        base.Exit();

        isWalljumping = false;
    }

    private void WallJump()
    {
        isWalljumping = true;
        Movement.SetVelocity(playerData.Walljumpforce, playerData.WalljumpAngle, -Movement.FacingDirection);
        Movement.CheckWhenToFlip(-Movement.FacingDirection);
    }

    private void Jump()
    {
        LeftJumpTimes--;
        Movement.SetVelocityY(playerData.Jumpforce);
        isAbilityDone = true;
    }


    public void ResetJump() => LeftJumpTimes = playerData.NumberOfJump;

    public void ChangeJumpAmount(int number)
    {
        LeftJumpTimes += number;
    }


}
