using Cysharp.Threading.Tasks;
using UnityEngine;

public class DashState : AbilityState
{
    private bool CanDash;

    private float originalgravity;
    private float lastTime;

    public DashState(PlayerSM playerSM, PlayerDataSO playerData, string Animname) : base(playerSM, playerData, Animname)
    {
    }

    public bool CheckwhenCanDash()
    {
        return CanDash && Time.time > lastTime + playerData.DashCooldown;
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        Dash().Forget();
    }

    public override void Exit()
    {
        base.Exit();

        Movement.SetVelocity_Zero();
    }

    private async UniTask Dash()
    {
        SetAllmovementZero();
        await UniTask.NextFrame();
        Artdepartment.PlaySoundEffect(playerData.DashSFX);
        Artdepartment.UseDashTrail(true);
        Movement.SetVelocityX(playerData.Dashforce, Movement.FacingDirection);
        await UniTask.WaitForSeconds(playerData.DashTime);
        Artdepartment.UseDashTrail(false);
        Movement.Rb.gravityScale = originalgravity;
        lastTime = Time.time;
        isAbilityDone = true;
    }

    private void SetAllmovementZero()
    {
        Movement.SetVelocity_Zero();
        originalgravity = Movement.Rb.gravityScale;
        Movement.Rb.gravityScale = 0;
    }

    public void ResetDash() => CanDash = true;
}
