using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoreSystem
{

    private readonly Dictionary<Type, Coremodule> coreModpack = new();

    #region Runtime
    //只有有需要函數的才去跑
    private readonly List<ICoreAwake> _awakes = new();
    private readonly List<ICoreUpdate> _updates = new();

    public void RuntimeAwake()
    {
        foreach (var item in _awakes)
        {
            item.CoreAwake();
        }
    }

    public void RuntimeUpdate()
    {
        foreach (var item in _updates)
        {
            item.CoreUpdate();
        }
    }
    #endregion



    public void Initialize(Coremodule[] mods, CoreSystem core)
    {
        foreach (var item in mods)
        {
            item.Register(core);
        }
    }

    public void Addmodules(Coremodule mod)
    {
        coreModpack.Add(mod.GetType(), mod);

        if (mod is ICoreAwake awake)
            _awakes.Add(awake);

        if (mod is ICoreUpdate update)
            _updates.Add(update);
    }

    public T GetCoremoduls<T>() where T : Coremodule
    {
        var module = coreModpack.Values.OfType<T>().FirstOrDefault();

        if (module)
            return module;

        Debug.LogWarning($"Do not found {typeof(T)} ");
        return null;
    }

}
