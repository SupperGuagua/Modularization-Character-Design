using System.Runtime.InteropServices;
using UnityEngine;

public class BaseState : IState
{

    protected PlayerSM playerSM;
    protected PlayerDataSO playerData;
    protected CoreSystem core;

    protected readonly string Animname;

    protected int xInput;
    protected bool isGround;


    private MovementMod movement;
    protected MovementMod Movement =>
        movement ??= core.GetCoremoduls<MovementMod>();

    private CollisionMod collision;
    protected CollisionMod Collision =>
        collision ??= core.GetCoremoduls<CollisionMod>();

    private InputMod inputMod;
    protected InputMod InputMod =>
        inputMod ??= core.GetCoremoduls<InputMod>();

    private ArtdepartmentMod artdepartment;
    protected ArtdepartmentMod Artdepartment =>
        artdepartment ??= core.GetCoremoduls<ArtdepartmentMod>();


    public BaseState(PlayerSM playerSM, PlayerDataSO playerData, string Animname)
    {
        this.playerSM = playerSM;
        this.playerData = playerData;
        this.Animname = Animname;
        core = playerSM.Core;
    }

    public virtual void Enter()
    {
        Docheck();
        PlayAnimation();
        Debug.Log($"{GetType().Name} Enter");
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {
        Docheck();
    }

    public virtual void PhysicUpdate()
    {

    }

    public virtual void Docheck()
    {
        xInput = InputMod.NorInputX;
        if (Collision) isGround = Collision.isGround;
    }

    public virtual void PlayAnimation()
    {
        Artdepartment.Anim.Play(Animname);
    }

    public virtual void AnimationTrigger() { }


}
