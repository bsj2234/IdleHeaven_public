using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> states = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> currentState;

    private EState queuedState;
    protected bool isTransitioningState = false;


    private void Start() 
    {
        currentState.EnterState();
    }
    private void Update() 
    {
        if(queuedState.Equals(currentState.StateKey))
        {
            currentState.UpdateState();
        }
        else if (!isTransitioningState)
        {
            TransitionToState(queuedState);
        }
    }

    public void TransitionToState(EState stateKey)
    {
        isTransitioningState = true;
        currentState.ExitState();
        currentState = states[stateKey];
        currentState.EnterState();
        isTransitioningState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
    private void OnTriggerStay(Collider other) 
    {
        currentState.OnTriggerStay(other);
    }
    private void OnTriggerExit(Collider other) 
    {
        currentState.OnTriggerExit(other);
    }
}
