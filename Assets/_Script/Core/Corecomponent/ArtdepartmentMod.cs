using System;
using UnityEngine;
using UnityEngine.Audio;

public class ArtdepartmentMod : Coremodule
{

    public event Action OnAnimationTrigger;

    [SerializeField] private TrailRenderer[] dashTrails;
    [SerializeField] public Transform Particleposition;

    public Animator Anim { get; private set; }

    private AudioSource Audiosource;

    public override void CoreAwake()
    {
        base.CoreAwake();

        Anim = GetComponent<Animator>();
        Audiosource = GetComponent<AudioSource>();
    }


    public void UseDashTrail(bool isEmitting)
    {
        foreach (var item in dashTrails)
        {
            item.emitting = isEmitting;
        }
    }

    public void PlayParticle(GameObject particle, Vector2 position, Quaternion rotation)
    {
        Instantiate(particle, position, rotation);
    }

    public void PlaySoundEffect(AudioResource audioClip)
    {
        Audiosource.resource = audioClip;
        Audiosource.Play();
    }


    public void AnimationTrigger()
    {
        OnAnimationTrigger?.Invoke();
    }

}
