using UnityEngine;


public interface ICoreAwake
{
    void CoreAwake();
}

public interface ICoreUpdate
{
    void CoreUpdate();
}

public interface IEffect
{
    void Effect();
}

public interface IDIe
{
    void Die();
}

public interface IUpdateRespawnPoint
{
    void RespawnUpdate(int where);
}
