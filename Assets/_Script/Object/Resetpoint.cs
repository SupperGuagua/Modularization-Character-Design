using Cysharp.Threading.Tasks;
using UnityEngine;

public class Resetpoint : BaseProp
{
    [SerializeField] private GameObject Particle;

    [SerializeField] private Collider2D trigger;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float spriteAlpha;
    [SerializeField] private float cooldown;

    public override void Effect()
    {
        base.Effect();

        Instantiate(Particle, gameObject.transform);
        DisableItem().Forget();
    }

    private async UniTask DisableItem()
    {
        trigger.enabled = false;
        SetAlpha(spriteAlpha);
        await UniTask.WaitForSeconds(cooldown);
        trigger.enabled = true;
        SetAlpha(1);
    }

    private void SetAlpha(float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

}
