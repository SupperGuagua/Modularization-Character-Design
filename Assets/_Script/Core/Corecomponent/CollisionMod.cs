using UnityEngine;

public class CollisionMod : Coremodule
{

    [Header("Check Transform")]
    [SerializeField] private Transform _groundcheck;
    [SerializeField] private Transform _upperwallcheck;
    [SerializeField] private Transform _lowerwallcheck;

    [Header("Check Variables")]
    [SerializeField] private float groundcheckRadius = 0.3f;
    [SerializeField] private float wallcheckDistance = 0.3f;
    [SerializeField] private LayerMask whatisground;

    public Transform Groundcheck { get => _groundcheck; private set => _groundcheck = value; }
    public Transform UpperWallcheck { get => _upperwallcheck; private set => _upperwallcheck = value; }
    public Transform LowerWallcheck { get => _lowerwallcheck; private set => _lowerwallcheck = value; }

    private MovementMod Movement;

    public override void CoreAwake()
    {
        base.CoreAwake();

        Movement = core.GetCoremoduls<MovementMod>();
    }


    public bool isGround
    {
        get => Physics2D.OverlapCircle(Groundcheck.position, groundcheckRadius, whatisground);
    }

    public bool isFacingWall()
    {
        RaycastHit2D upperHit = Physics2D.Raycast(UpperWallcheck.position,
            Vector2.right * Movement.FacingDirection, wallcheckDistance, whatisground);

        RaycastHit2D lowerHit = Physics2D.Raycast(LowerWallcheck.position,
            Vector2.right * Movement.FacingDirection, wallcheckDistance, whatisground);

        return upperHit && lowerHit;
    }



    //Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Groundcheck.position, groundcheckRadius);
        Gizmos.DrawWireSphere(UpperWallcheck.position + new Vector3(wallcheckDistance, 0, 0), 0.05f);
        Gizmos.DrawWireSphere(LowerWallcheck.position + new Vector3(wallcheckDistance, 0, 0), 0.05f);
    }

}
