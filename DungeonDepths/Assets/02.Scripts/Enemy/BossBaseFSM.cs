using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BossBaseFSM : MonoBehaviour
{
    public enum BossStates { Idle, Trace, FastTrace, MeleeAttack, RangeAttack, Die };
    public StateMachine<BossBaseFSM> stateMachine;
    public Animator Animator { get; private set; }
    protected Rigidbody rbody;
    #region ���� �������ͽ�
    public float BossMaxHp { get; set; }
    public float BossCurHp { get; set; }
    public float MoveSpeed { get; set; }
    public float RotSpeed { get; set; }
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
    
    public bool isDead;
    public float delayTime = 2.5f;

    protected virtual void Awake() // �����߰� , �ʱ�ȭ
    {
        stateMachine = new StateMachine<BossBaseFSM>();
        Animator = GetComponent<Animator>();
        
        isDead = false;
        stateMachine.AddState((int)BossStates.Idle, new BossState.Idle());
        stateMachine.AddState((int)BossStates.Trace, new BossState.Trace());
        stateMachine.AddState((int)BossStates.FastTrace, new BossState.FastTrace());
        stateMachine.AddState((int)BossStates.MeleeAttack, new BossState.MeleeAttack());
        stateMachine.AddState((int)BossStates.RangeAttack, new BossState.RangeAttack());
        stateMachine.AddState((int)BossStates.Die, new BossState.Die());

        stateMachine.InitState(this, stateMachine.GetState((int)BossStates.Idle));
    }

    public abstract void GetHit(float _damage);
    
    // ���� ������� Ȯ��
    protected void CheckAlive()
    {
        if(BossCurHp <= 0)
        {
            isDead = true;
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.Die));
        }
    } 

    /// <summary>
    /// CheckDistance �޼ҵ��� ��ȯ���� true��� ���� ����
    /// false��� �Ϲ� ����
    /// </summary>
    public void CheckTraceState()
    {
        if(CheckDistance(TraceRange / 2))
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.FastTrace));
        else
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.Trace));
    }

    /// <summary>
    /// ������ �÷��̾� ������ �Ÿ��� �����ؼ� CheckPlayerInSight�Լ��� ����
    /// </summary>
    public void CheckAttackState()
    {
        float dist = (TargetTransform.position - BossTransform.position).magnitude;
        CheckPlayerInSight(dist);
    }

    public void Trace()
    {
        BossTransform.position = Vector3.MoveTowards(BossTransform.position, TargetTransform.position, MoveSpeed * Time.deltaTime);
    }
    public void Rotate()
    {
        Vector3 dir = TargetTransform.position - BossTransform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        BossTransform.rotation = Quaternion.Lerp(BossTransform.rotation, rot, RotSpeed * Time.deltaTime);
    }

    //public void MoveAndRotate()
    //{
    //    Vector3 dir = TargetTransform.position - BossTransform.position;
    //    BossTransform.position = Vector3.MoveTowards(BossTransform.position, TargetTransform.position, MoveSpeed * Time.deltaTime);
    //    Quaternion rot = Quaternion.LookRotation(dir);
    //    BossTransform.rotation = Quaternion.Lerp(BossTransform.rotation, rot, RotSpeed * Time.deltaTime);
    //}

    //public bool CheckRadius(float _radius)
    //{
    //    Vector3 dir = TargetTransform.position - BossTransform.position;
    //    float radiusSqr = _radius * _radius;

    //    // dir ���� ũ���� ������ ��ȯ�Ѵ�.
    //    // �÷��̾�� ���� ������ �Ÿ��� ������ ���޹��� Ư�� �ݰ��� �������� �۴ٸ�
    //    if(dir.sqrMagnitude < radiusSqr)
    //        return true;
    //    return false;
    //}

    // �÷��̾ ���� ��Ÿ����� ������ ���
    // �÷��̾ �þ߿� �����ƴ��� Ȯ��
    // �ƴ϶�� ������ ȸ���Ѵ�.
    public void CheckPlayerInSight(float _dist)
    {
        RaycastHit hit;
        Vector3 dir = TargetTransform.position - BossTransform.position;
        if(_dist <= MeleeRange) // �÷��̾ ���� ��Ÿ� ���� �ִٸ�
        {
            if(Physics.Raycast(BossTransform.position, BossTransform.forward, out hit, MeleeRange) && hit.collider.CompareTag("Player"))
            {
                stateMachine.ChangeState(stateMachine.GetState((int)BossStates.MeleeAttack));
            }
            else
            {
                Rotate();
            }
        }
        else if(6 < _dist && _dist < 13)
        {
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.Trace));
        }
        else if(_dist <= BeamRange)
        {
            int random = Random.Range(0, 100);
            if(Physics.Raycast(BossTransform.position, BossTransform.forward, out hit, BeamRange) && hit.collider.name == "PlayerSwordMan")
            {
                if(random <= 4 && Time.time - PrevAtkTime >= AttackDelay)
                {
                    stateMachine.ChangeState(stateMachine.GetState((int)BossStates.RangeAttack));
                }
            }
            else
            {
                Rotate();
            }
        }

    }


    //true�� ��ȯ�Ѵٸ� ���� ����
    //false�� ��ȯ�Ѵٸ� �Ϲ� ���� 
    public bool CheckDistance(float _slowDistance)
    {
        // ���� ������ �÷��̾� ������ �Ÿ�, ����
        Vector3 dir = TargetTransform.position - BossTransform.position;
        float sqrSlowDistance = _slowDistance * _slowDistance;
        // ���� ������ �÷��̾� ������ �Ÿ��� ����� �ִٸ� 
        if(dir.sqrMagnitude >= sqrSlowDistance)
            return true;
        return false;
    }

    //public bool GetDelayTime(float _time)
    //{
    //    if(Time.time - _time >= 3f)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
