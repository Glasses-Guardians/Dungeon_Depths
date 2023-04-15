using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinalBoss : MonoBehaviour
{
    public enum FinalBossStates { Idle, AttackIdle, Trace, MeleeAttack1, MeleeAttack2, MeleeAttack3, Die };
    public StateMachine<FinalBoss> stateMachine;
    public Animator animator;
    public float comboDuration = 2.5f;
    [SerializeField] float moveSpeed = 6.5f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] Transform finalBossTransform;
    [SerializeField] Transform targetTransform;
    [SerializeField] bool isDead;

    public bool isSecondPhase;
    float meleeAttackRange = 5f;
    float meleeAttackAngle = 60f;

    float hpMax = 1000, hpCur;
    readonly int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    void Awake()
    {
        hpCur = hpMax;
        finalBossTransform = GetComponent<Transform>();
        targetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        stateMachine = new StateMachine<FinalBoss>();
        animator = GetComponent<Animator>();

        animator.SetTrigger("BossEnter");
        isSecondPhase = false;

        stateMachine.AddState((int)FinalBossStates.Idle, new FinalBossState.Idle());
        stateMachine.AddState((int)FinalBossStates.AttackIdle, new FinalBossState.AttackIdle());
        stateMachine.AddState((int)FinalBossStates.Trace, new FinalBossState.Trace());
        stateMachine.AddState((int)FinalBossStates.MeleeAttack1, new FinalBossState.MeleeAttack1());
        stateMachine.AddState((int)FinalBossStates.MeleeAttack2, new FinalBossState.MeleeAttack2());
        stateMachine.AddState((int)FinalBossStates.MeleeAttack3, new FinalBossState.MeleeAttack3());

        stateMachine.InitState(this, stateMachine.GetState((int)FinalBossStates.Idle));
    }

    private void Update()
    {
        //Debug.Log("�������� ���� ���� : " + stateMachine.CurrentState);
        stateMachine.Execute();
    }

    //public Vector3 MeleeCirclePoint(float angle)
    //{
    //    angle += transform.eulerAngles.y;
    //    return new Vector3(Mathf.Sign(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    //}
    //public Vector3 RangeCirclePoint(float angle)
    //{
    //    angle += transform.eulerAngles.y;
    //    return new Vector3(Mathf.Sign(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    //}

    public void GetHit(float _damage)
    {
        hpCur -= _damage;
        if(hpCur <= hpMax * 0.6)
        {
            isSecondPhase = true;
            //animator.SetTrigger("Stun");
        }
        else if(hpCur <= 0)
            Die();
    }

    public void Die()
    {
        //animator.SetTrigger("Die");
    }

    //������ �÷��̾� ������ �Ÿ��� ���ؼ� ��ȯ
    public float CheckDistance()
    {
        //Debug.Log("�÷��̾���� �Ÿ� ���ϱ�");
        return (finalBossTransform.position - targetTransform.position).magnitude;
    }

    // ������ ���¸� Trace ���·� ��ȯ�Ѵ�.
    public void CheckTraceState()
    {
        //Debug.Log("���� Ȯ��");
        stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.Trace));
    }
    // ������ ��ǥ�� �÷��̾� ��ǥ������ �̵���Ų��.
    public void Trace()
    {
        //Debug.Log("���� ����");
        finalBossTransform.position = Vector3.MoveTowards(finalBossTransform.position, targetTransform.position, moveSpeed * Time.deltaTime);
    }

    // ������ �÷��̾����� �ٶ󺸰Բ� ȸ����Ų��.
    public void Rotation()
    {
        //Debug.Log("ȸ��");
        Vector3 dir = targetTransform.position - finalBossTransform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        finalBossTransform.rotation = Quaternion.Lerp(finalBossTransform.rotation, rot, rotationSpeed * Time.deltaTime);
    }

    // �÷��̾���� �Ÿ��� Ȯ������ �̸� ���ݹݰ� �˻� �Լ��� �����Ѵ�.
    public void CheckAttackState()
    {
        Debug.Log("���ݻ��� Ȯ��");
        float dist = CheckDistance();
        int attackIndex;
        //Debug.Log("�÷��̾���� �Ÿ� : " + dist);
        if(IsPlayerInAttackSight(dist, out attackIndex))
        {
            if(attackIndex == 1)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.MeleeAttack1));
            else if(attackIndex == 2)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.MeleeAttack2));
            else if(attackIndex == 3)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.MeleeAttack3));
        }
        else Rotation();
    }

    //������ ���ݹݰ��� �˻��Ѵ�.
    public bool IsPlayerInAttackSight(float _dist, out int _attackIndex)
    {
        //Debug.Log("���� ���� �þ� Ȯ��");
        //RaycastHit hit;
        Ray ray = new Ray(finalBossTransform.position, finalBossTransform.forward);
        Vector3 dir = (targetTransform.position - finalBossTransform.position).normalized;
        // ��������1 ��Ÿ� ���� �����ִٸ�
        if(_dist < meleeAttackRange)
        {
            float dot = Vector3.Dot(finalBossTransform.forward, dir);
            float theta = Mathf.Acos(dot);
            float angle = Mathf.Rad2Deg * theta;

            //if(Physics.Raycast(ray, out hit, meleeAttackRange) && hit.collider.CompareTag("Player"))
            if(angle <=  meleeAttackAngle / 5)
            {
                _attackIndex = 1;
                return true;
            }
            //else if(angle >= -meleeAttackAngle && isSecondPhase && angle <= 0)
            //{
            //    _attackIndex = 3;
            //    return true;
            //}
            else if(angle <= meleeAttackAngle * 2)
            {
                _attackIndex = 2;
                return true;
            }
            else Rotation();
        }
        _attackIndex = 0;
        return false;
    }

    public bool ShouldCombo(out int comboIndex)
    {
        Debug.Log("�޺����� �˻�");
        float dist = CheckDistance();
        if(IsPlayerInAttackSight(dist, out comboIndex))
        {
            if(comboIndex == 0) return false;
            return true;
        }
        return false;
    }
}
