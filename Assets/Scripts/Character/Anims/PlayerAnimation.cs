using IdleHeaven;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private float speed = 0.0f;
    private float direction = 0.0f;
    private NavMeshAgent agent;
    private Attack attack;


    //cache
    Health health;
    float damage;
    AttackType type;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attack = GetComponent<Attack>();
    }

    //�ֹ̳׼� �����ϰ� ���� ������ �� �ֱ�
    public void HandleOnAttack(Health health, float damage, AttackType type)
    {
        anim.SetTrigger("Attack");
        this.health = health;
        this.damage = damage;
        this.type = type;
    }
    public void HandleAnimationAttack()
    {
        attack.DealDamage(health, damage, type);
    }


    Vector3 prevDir = Vector3.zero;
    void Update()
    {
        float curSpeed = agent.velocity.magnitude;

        if (curSpeed > 0.1f)
        {
            Vector3 dirDiff = transform.forward - prevDir;
            dirDiff = transform.InverseTransformDirection(dirDiff);
            float DeltaDir = dirDiff.x;
            anim.SetFloat("MovingDirection", DeltaDir);

            anim.SetBool("IsMoving", true);
            anim.SetBool("IsRunning", false);
            Debug.Log("Moving");
        }
        else
        {
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsRunning", false);
            Debug.Log("Not Moving");
        }
        if (curSpeed > 15f)
        {
            anim.SetBool("IsMoving" +
                "", false);
            anim.SetBool("IsRunning", true);
            Debug.Log("Running");
        }
        prevDir = transform.forward;
    }
}
