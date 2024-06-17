using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, ICombat
{
    Combat combat = new Combat();
    private void Start()
    {
        combat.Init(transform);
    }
    public void Attack(Combat targetCombat, float damage)
    {
        targetCombat.DealDamage(targetCombat, damage);
    }

    public Combat GetCombat()
    {
        return combat;
    }

    public void TakeDamage(Combat attackerCombat, float damage)
    {
        combat.TakeDamage(damage);
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
