using UnityEngine;

public class IdleState : BaseState
{
    private AICharacterController character;
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
        base.stateMachine = stateMachine;
        character = stateMachine.Owner.GetComponent<AICharacterController>();
    }
    public override void EnterState()
    {
        Debug.Log("Entering Idle State");
    }
    public override void UpdateState()
    {
    }
    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }

    
}