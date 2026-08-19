using UnityEngine;

public abstract class Coremodule : MonoBehaviour, ICoreAwake, ICoreUpdate
{

    protected CoreSystem core;

    public void Register(CoreSystem core)
    {
        this.core = core;
        core.Addmodules(this);
    }

    public virtual void CoreAwake() { }

    public virtual void CoreUpdate() { }
}
