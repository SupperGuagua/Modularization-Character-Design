using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/DataSO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Move")]
    public float MoveSpeed;

    [Header("Jump")]
    public int NumberOfJump;
    public float Jumpforce;
    public float VariableJumpHeight = 0.3f;

    [Header("WallClimb")]
    public float Slidingspeed;
    public float Walljumpforce;
    public float WalljumpTime;
    public float WallGrabTime;
    public float WallclimbTime;
    public Vector2 WalljumpAngle;

    [Header("Dash")]
    public float Dashforce;
    public float DashTime;
    public float DashCooldown;

    [Header("Respawn")]
    public float RespawnDuration;

    [Header("Audio")]
    public AudioResource WalkingSFX;
    public AudioResource DashSFX;
    public AudioResource JumpSFX;
    public AudioResource CheckpointSFX;
    public AudioResource RespawnSFX;

    [Header("Particle")]
    public GameObject RespawnFX;
    public GameObject JumpDust;
    public GameObject RunDust;
    public GameObject AirJumpDust;


}
