using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class PlayerSM : Statemachine
{

    public CoreSystem Core { get; private set; }

    public IdleState Idlestate { get; private set; }
    public MoveState Movestate { get; private set; }
    public JumpState Jumpstate { get; private set; }
    public inAirState inAirstate { get; private set; }
    public WallCimbState WallCimbstate { get; private set; }
    public DashState Dashstate { get; private set; }


    [SerializeField] private PlayerDataSO _playerData;
    public PlayerDataSO PlayerData => _playerData;

    private RecieverMod Reciever;
    private MovementMod Movement;
    private ArtdepartmentMod ArtdepartmentMod;

    private Transform currentRespawnPoint;

    private void Awake()
    {
        SetCoreSystem();

        Core.RuntimeAwake();
        InitStates();

        Movement = Core.GetCoremoduls<MovementMod>();
        Reciever = Core.GetCoremoduls<RecieverMod>();
        ArtdepartmentMod = Core.GetCoremoduls<ArtdepartmentMod>();
    }

    private void OnEnable()
    {
        Reciever.OnResetpoint += HandleResetpoint;
        Reciever.OnUpdateRespawnPoint += UpdateRespawnPoint;
        Reciever.OnDeath += HandleDeath;
    }

    private void ODisable()
    {
        Reciever.OnResetpoint -= HandleResetpoint;
        Reciever.OnUpdateRespawnPoint -= UpdateRespawnPoint;
        Reciever.OnDeath -= HandleDeath;
    }

    private void Start()
    {
        currentRespawnPoint = Gamemanager.instance.RespawnPoints[0];
        Activate(Idlestate);
    }

    public override void Update()
    {
        base.Update();

        Core.RuntimeUpdate();
    }

    private void SetCoreSystem()
    {
        Core = new CoreSystem();

        Core.Initialize(GetComponentsInChildren<Coremodule>(), Core);
    }

    private void InitStates()
    {
        Idlestate = new IdleState(this, PlayerData, "Idle");
        Movestate = new MoveState(this, PlayerData, "Run");
        Jumpstate = new JumpState(this, PlayerData, "Jump");
        inAirstate = new inAirState(this, PlayerData, "Fall");
        WallCimbstate = new WallCimbState(this, PlayerData, "Wallclimb");
        Dashstate = new DashState(this, PlayerData, "Dash");
    }

    private void HandleResetpoint()
    {
        Jumpstate.ResetJump();
        Dashstate.ResetDash();
    }

    private void UpdateRespawnPoint(int where)
    {
        currentRespawnPoint = Gamemanager.instance.RespawnPoints[where];
        ArtdepartmentMod.PlaySoundEffect(PlayerData.CheckpointSFX);
    }

    private void HandleDeath()
    {
        Respawn().Forget();
    }

    private async UniTask Respawn()
    {
        Movement.SetVelocity_Zero();
        Movement.Rb.simulated = false;
        await UniTask.WaitForSeconds(PlayerData.RespawnDuration);
        ArtdepartmentMod.PlaySoundEffect(PlayerData.RespawnSFX);
        gameObject.transform.position = currentRespawnPoint.position;
        ArtdepartmentMod.PlayParticle(PlayerData.RespawnFX, currentRespawnPoint.position, currentRespawnPoint.rotation);
        Movement.Rb.simulated = true;
    }
}