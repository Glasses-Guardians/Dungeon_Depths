using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    #region Monster Stats
    protected int damage;                   public int Damage { get { return damage; }}
    protected float attackDistance;         public float AttackDistance { get { return attackDistance; } }
    protected float traceDistance;          public float TraceDistance { get { return traceDistance; } }
    protected float traceDisOffset = 2f;
    protected float stopDistance = 0.2f;

    protected float moveSpeed;              public float MoveSpeed { get { return moveSpeed; } }
    protected float rotSpeed;               public float RotSpeed { get { return rotSpeed; } }
    protected float attackSpeed = 2f;       public float AttackSpeed { get { return attackSpeed; } }    // ���� ��
    protected float searchSpeed = 1f;       public float SearchSpeed { get { return searchSpeed; } }  // Ž�� ��
    protected int maxHp = 100;              public int MaxHp { get { return maxHp; } }
    protected int curHp;                    public int CurHp { get { return curHp; } set { if (curHp <= 0) curHp = 0; } }
    #endregion
    #region state���� ����
    public enum eMonsterState { Idle, Patrol, Trace, Attack, Die } // ���� ������ ����
    protected StateMachine<MonsterBase> sm;
    public WayPoints wayPoints;
    public float PrevTime { get; set; } = 0f;
    public float IdleTime { get; set; } = 0f;
    public float LastAttackTime { get; set; } = 0f;
    public float LastSearchTime { get; set; } = 0f;
    protected Animator anim;                public Animator Anim { get { return anim; } }
    GameObject target;
    protected GameObject curTarget;
    #endregion
    protected virtual void Awake()
    {
        wayPoints = GameObject.Find("WayPoints").GetComponent<WayPoints>();
        target = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        #region state ����
        sm = new StateMachine<MonsterBase>();
        sm.AddState((int)eMonsterState.Idle, new MonsterState.Idle());
        sm.AddState((int)eMonsterState.Patrol, new MonsterState.Patrol());
        sm.AddState((int)eMonsterState.Trace, new MonsterState.Trace());
        sm.AddState((int)eMonsterState.Attack, new MonsterState.Attack());
        sm.AddState((int)eMonsterState.Die, new MonsterState.Die());
        sm.InitState(this, sm.GetState((int)eMonsterState.Idle));
        #endregion

    }
    public abstract void Init();
    public abstract void GetHit();  // �´°�
    public void CheckIdleState()
    {
        if (curTarget != null)
        { 
            sm.ChangeState(sm.GetState((int)eMonsterState.Trace));
        }
        else if (IdleTime >= Random.Range(3f, 5f))
            sm.ChangeState(sm.GetState((int)eMonsterState.Patrol));
    }
    public void CheckPatrolState()
    {
        SearchTarget();
        if (curTarget != null && (CheckRadius(curTarget.transform.position, traceDistance)))
            sm.ChangeState(sm.GetState((int)eMonsterState.Trace));
        else if (wayPoints.CheckDestination(stopDistance,transform.position))
        { 
            sm.ChangeState(sm.GetState((int)eMonsterState.Idle));
        }
    }
    public void CheckTraceState()
    {
        if (CheckRadius(curTarget.transform.position, attackDistance))
        { 
            sm.ChangeState(sm.GetState((int)eMonsterState.Attack));
        }
        else if (CheckRadius(curTarget.transform.position, traceDistance))
        {
            MoveAndRotate(curTarget.transform.position);
        }
        else if (!CheckRadius(curTarget.transform.position, traceDistance) && curTarget == null)
        { 
            sm.ChangeState(sm.GetState((int)eMonsterState.Idle));
        }
    }
    public void CheckAttackState()
    {
        if (CheckRadius(curTarget.transform.position, attackDistance)) // ���� �Ÿ� üũ
        {
            if (Time.time > LastAttackTime) // ���� �ֱ� üũ
            {
                //ToDo ����. Player�� Damage�޴°� ȣ���ؿ���
                Vector3 dir = curTarget.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
                LastAttackTime = Time.time + attackSpeed;
            }
        }
        else
            sm.ChangeState(sm.GetState((int)eMonsterState.Trace));
    }
    public void MoveAndRotate(Vector3 _targetPos)
    {
        Vector3 dir = _targetPos - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
    }
    bool CheckRadius(Vector3 _targetPoint, float _radius)  //Ÿ�� ��ġ, �� �Ÿ�
    {
        Vector3 dir = _targetPoint - transform.position;
        float radiusSqr = _radius * _radius;        // r^2 : �ݰ� ���� ��
        if (dir.sqrMagnitude < radiusSqr)
            return true;
        return false;
    }
    public void SearchTarget()    //��ǥ�� Ž�� �� ����
    {
        if (Time.time < LastSearchTime) return;
        LastSearchTime = Time.time + searchSpeed;

        // �Ÿ� ���� ���� �÷��̾� �˻�
        if (CheckRadius(target.transform.position, traceDistance + traceDisOffset))
        {
            curTarget = target;
        }
        else curTarget = null;
        //Debug.Log("Current Target : " + curTarget);
    }
}