using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public interface IMovable
{
}
public interface IMovableAI: IMovable
{
    public NavMeshAgent GetAgent();
}

[RequireComponent (typeof(StateMachine))]
public class AICharacterController : MonoBehaviour, IMovableAI, ICombat
{
    private StateMachine stateMachine;
    public Transform[] patrolWaypoints;
    public Transform chaseTarget;
    [SerializeField] private NavMeshAgent Agent_agent;
    public IMovable movement;
    private Weapon weapon;

    private Combat combat = new Combat();

    public event Action<Transform> EnemyFoundHandler;

    private List<ICombat> attackAbleEnemies = new List<ICombat>();

    //IsGrounded(Walk,Run)	IsFlying	IsSwimming	IsClimbing	IsJumping IsBurrowing	IsSliding	IsSwinging	IsCreeping	IsRolling	IsFloating	IsGliding	IsSoaring

    private void Start()
    {
        combat.Init(transform);

        stateMachine = GetComponent<StateMachine>();
        stateMachine.Init(transform);

        var idleState = new IdleState(stateMachine);
        var patrolState = new PatrolState(stateMachine, patrolWaypoints);
        var chaseState = new ChaseState(stateMachine, chaseTarget);
        var attackState = new AttackState(stateMachine);
        var deadState = new DeadState(stateMachine);

        stateMachine.AddState(idleState);
        stateMachine.AddState(patrolState);
        stateMachine.AddState(chaseState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(deadState);

        stateMachine.ChangeState<IdleState>();
    }


    public NavMeshAgent GetAgent()
    {
        return Agent_agent;
    }

    public Combat GetCombat()
    {
        return combat;
    }

    public void ChangeToAuto()
    {
        stateMachine.ChangeState<AttackState>();
    }

    private void OnFoundEnemy(ICombat target)
    {

        attackAbleEnemies.Add(target);
        Debug.Log($"Enemy found{target.GetTransform().name}");

        if (stateMachine.CurrentState != stateMachine.GetState<AttackState>())
        {
            stateMachine.GetState<ChaseState>().SetTarget(PeekNearestEnemy().GetTransform());
            stateMachine.ChangeState<ChaseState>();
        }

        if (EnemyFoundHandler != null)
        {
            EnemyFoundHandler.Invoke(PeekNearestEnemy().GetTransform());
        }
        else
        {
            Debug.Log("Enemy found but handler is null");
        }
    }

    public ICombat PeekNearestEnemy()
    {
        if(attackAbleEnemies.Count == 0)
        {
            return null;
        }
        attackAbleEnemies.Sort((lhs, rhs) => (rhs.GetDistance(transform.position).CompareTo(lhs.GetDistance(transform.position))));
        var result = attackAbleEnemies[attackAbleEnemies.Count - 1];
        //attackAbleEnemies.RemoveAt(attackAbleEnemies.Count - 1);
        Debug.Log($"Pop AttackableEnemy {result.GetTransform().name}");
        return result;
    }

    public void PopEnemy(ICombat combat)
    {
        if (attackAbleEnemies.Count == 0)
        {
            Debug.Log("Tried pop but empty");
            return;
        }
        attackAbleEnemies.Remove(combat);

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{name} Trigger Enter");
        if(other.CompareTag("Enemy"))
        {
            OnFoundEnemy(other.transform.GetComponent<ICombat>());
        }
    }

    public void DealDamage(ICombat enemyCombat, float damage)
    {
        enemyCombat.TakeDamage(combat, damage);
    }
    public void TakeDamage(ICombat enemyCombat, float damage)
    {
        combat.TakeDamage(damage);
    }

    private void OnAttack()
    {
        weapon.OnCollide();
    }
    private void OnTakeDamage()
    {
    }

    public void TakeDamage(Combat attackerCombat, float damage)
    {
        combat.TakeDamage(damage);
    }

    public void Attack(Combat targetCombat, float damage)
    {
        combat.DealDamage( targetCombat,damage);
    }

    public bool IsDead()
    {
        return combat.IsDead();
    }

    public float GetDistance(Vector3 origin)
    {
        return Vector3.Distance(origin, transform.position);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}