using System;
using UnityEngine;

public class RecieverMod : Coremodule, IDIe, IUpdateRespawnPoint
{

    public event Action OnResetpoint;
    public event Action OnDeath;
    public event Action<int> OnUpdateRespawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            if (other.TryGetComponent(out IEffect effect))
            {
                effect.Effect();
            }
        }
        else if (other.gameObject.tag == "Resetpoint")
        {
            OnResetpoint?.Invoke();

            if (other.TryGetComponent(out IEffect effect))
            {
                effect.Effect();
            }
        }
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }

    public void RespawnUpdate(int where)
    {
        OnUpdateRespawnPoint?.Invoke(where);
    }

}
