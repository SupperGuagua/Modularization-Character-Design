using UnityEngine;

public class Statemachine : MonoBehaviour
{
    protected IState currentstate;

    public IState PreviousState { get; private set; }

    public virtual void Update()
    {
        currentstate?.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        currentstate?.PhysicUpdate();
    }

    public void Activate(IState startingstate)
    {
        currentstate = startingstate;
        currentstate.Enter();
    }

    public void ChangeState(IState newstate)
    {
        PreviousState = currentstate;
        currentstate?.Exit();
        currentstate = newstate;
        currentstate.Enter();
    }



}
