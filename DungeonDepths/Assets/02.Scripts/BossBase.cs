using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossBase : MonoBehaviour
{
    // �ʿ��� ������: ���� ü��, �þ߰�, �̵��ӵ�, ��Ǻ� ���ݷ�, ���� ��, �ִϸ�����, �÷��̾� ��ġ Ž��, � ������ ������ Ȯ�� ����, ���� ��Ÿ�
    /* ��ȹ
     * ������ �����뿡 �÷��̾�� �� ���� �����ϹǷ� �������� �����ϸ� ������ Trace ����
     * ������ �������ݰ� ���Ÿ����� �ΰ��� �ݰ��� ������.
     * ���Ÿ� ������ �Ÿ��� �� ��ŭ ������ ����, �ٰŸ� ������ �Ÿ��� ª����� ���� �а� 
     * �÷��̾���� �Ÿ��� ������ �̻� �������� ������ �߰��ϵ��� �� �� 
     */

    public enum BossStates { Idle, Trace, FastTrace, MeleeAttack, RangeAttack, Die };
    public BossStates state;

    public Animator Animator { get; set; }
    public NavMeshAgent Agent { get; set; }

    #region ���� �������ͽ�
    public float BossHp { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackDamage { get; set; }
    #endregion

    #region ���� ���� ����
    public Transform TargetTransform { get; set; }
    public Transform BossTransform { get; set; }
    public float TraceRange { get; set; } // ���� �Ÿ��� �� ������ ��ŭ ������ ��
    #endregion  

    #region ���� ���� ����
    public float MeleeAngle { get; set; } // ���������� ������ �ޱ�
    public float RangeAngle { get; set; } // ���Ÿ� ������ ������ �ޱ�
    public float AttackDelay { get; set; }
    public float PrevAtkTime { get; set; }
    public float MeleeRange { get; set; }
    public float BeamRange { get; set; }
    #endregion

    [SerializeField] bool isDead = false;
    protected void CheckAlive() // ���� ��� ���� Ȯ��
    {
        if(BossHp <= 0)
        {
            isDead = true;
            state = BossStates.Die;
        }
    }
    protected void CheckState(float _dist)
    {
        Debug.Log("BossState : " + state);
        if(isDead) return;
        
        if(_dist <= TraceRange * 0.5f) // ����
        {
            if(_dist <= MeleeRange)
            {
                CheckPlayerInSight(_dist);
            }
            else if(_dist <= BeamRange)
            {
                int rand = Random.Range(0, 10);
                if(rand <= 1) // 20���� Ȯ���� ���Ÿ� ������ �غ�
                    CheckPlayerInSight(_dist);
            }
            else
                state = BossStates.Trace;
        }
        else // ���� ����
        {
            state = BossStates.FastTrace;
        }
    }

    protected void CheckPlayerInSight(float _dist)
    {
        RaycastHit hit;
        Ray ray = new Ray(BossTransform.position, BossTransform.forward);
        Vector3 dir = (TargetTransform.position - BossTransform.position).normalized;

        if(_dist <= MeleeRange) // ���� ���ݻ�Ÿ����� �ٰ��Դٸ�
        {
            if(Vector3.Angle(BossTransform.forward, dir) < MeleeAngle && Physics.Raycast(ray, out hit, MeleeRange))
            {
                if(hit.collider.CompareTag("Player"))
                    state = BossStates.MeleeAttack;
            }

        }
        else
        {
            if(Vector3.Angle(BossTransform.forward, dir) < RangeAngle && Physics.Raycast(ray, out hit, BeamRange))
            {
                if(hit.collider.CompareTag("Player"))
                    state = BossStates.RangeAttack;
            }
        }
    }

    protected void BossAction()
    {
        switch(state)
        {
            case BossStates.Idle:
                // ���(������ ����� ª�� �ð����� ����)
                break;
            case BossStates.Trace:
                // ����
                Agent.isStopped = false;
                TraceTarget(MoveSpeed);
                break;
            case BossStates.FastTrace:
                // ��������
                Agent.isStopped = false;
                TraceTarget(MoveSpeed * 2f);
                break;
            case BossStates.MeleeAttack:
                Agent.isStopped = true;
                //BossTransform.Translate(Vector3.zero);
                if(Time.time - PrevAtkTime < AttackDelay) break;
                MeleeAttack();
                break;
            case BossStates.RangeAttack:
                Agent.isStopped = true;
                //BossTransform.Translate(Vector3.zero);
                RangeAttack();
                //PrevAtkTime = Time.time;
                break;
            case BossStates.Die:
                Agent.isStopped = true;
                Die();
                break;
        }
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
    //_dist�� �÷��̾�� ���� ������ �Ÿ�

    protected void TraceTarget(float _speed)
    {
        Agent.destination = TargetTransform.position;
        Animator.SetFloat("MoveSpeed", _speed);
        if(_speed > MoveSpeed)
        {
            Animator.SetBool("FastTrace", true);
            Agent.speed = _speed;
        }
        else
        {
            Animator.SetBool("Trace", true);
            Agent.speed = _speed;
        }
        //Search();
        //Rotate();
        //Move();
    }

    void Search()
    {

    }

    void Rotate()
    {

    }

    void Move()
    {

    }

    protected void MeleeAttack()
    {
        Debug.Log("���� ���� ����");
        Animator.SetInteger("MeleeAttackIndex", Random.Range(0, 2));
        Animator.SetTrigger("MeleeAttack");
        PrevAtkTime = Time.time;
    }

    protected void RangeAttack()
    {
        Animator.SetTrigger("RangeAttack");
    }

    protected void GetDamage()
    {
        Debug.Log("���� �ǰ�");
        Animator.SetTrigger("GeDamage");
    }
    protected void Die()
    {
        Debug.Log("���� ���");
        Animator.SetTrigger("Die");
    }
}
