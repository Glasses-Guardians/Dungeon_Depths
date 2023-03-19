using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
	#region Enemy Stats
	protected int damage;				public int Damage { get; }
	protected float attackDistance;		public int AttackDistance { get; }
	protected float traceDistance;		public int TraceDistance { get; }
	protected float traceDisOffset = 2f;
	protected float stopDistance = 0.2f;
	protected float moveSpeed;			public int MoveSpeed { get; }
	protected float rotSpeed;			public int RotSpeed { get; }
	protected float attackSpeed = 2f;	public int AttackSpeed { get; }    // ���� ��
	protected float searchSpeed = 1f;	public int SearchSpeed { get; }	// Ž�� ��
	protected int maxHp;				public int MaxHp { get; }
	protected int curHp;				public int CurHp { get { return curHp; } set { if (curHp <= 0) curHp = 0; } }
	#endregion
	#region state���� ����
	public enum eEnemyState { Idle, Patroll, Trace, Attack, Die } // ���� ������ ����
	protected StateMachine<EnemyBase> sm;
	public WayPoints wayPoints;
	protected float prevTime = 0f;			public float PrevTime { get; set; }
	protected float idleTime = 0f;			public float IdleTime { get; set; }
	protected float lastAttackTime = 0f;	public int LastAttackTime { get; set; }
	protected float lastSearchTime = 0f;	public int LastSearchTime { get; set; }
	protected GameObject target;        
	protected GameObject curTarget;			public GameObject CurTarget { get; set; }
	#endregion
	protected virtual void Awake()
	{
		#region state ����
		sm = new StateMachine<EnemyBase>();
		sm.AddState((int)eEnemyState.Idle, new EmemyState.Idle());
		sm.AddState((int)eEnemyState.Patroll, new EmemyState.Patroll());
		sm.AddState((int)eEnemyState.Trace, new EmemyState.Trace());
		sm.AddState((int)eEnemyState.Attack, new EmemyState.Attack());
		sm.AddState((int)eEnemyState.Die, new EmemyState.Die());
		sm.InitState(this, sm.GetState((int)eEnemyState.Idle));
		#endregion
		wayPoints = GetComponent<WayPoints>();
		target = GameObject.Find("Player");
	}
	public abstract void Init();	
	public abstract void GetHit();	// �´°�
	public void CheckIdleState()
	{
		if (target != null)
			sm.ChangeState(sm.GetState((int)eEnemyState.Trace));
		else if (idleTime >= Random.Range(3f, 5f))
			sm.ChangeState(sm.GetState((int)eEnemyState.Patroll));
	}
	public void CheckPatrolState()
	{
		Search();
		if (target != null)
			sm.ChangeState(sm.GetState((int)eEnemyState.Trace));
		else if (wayPoints.CheckDestination(stopDistance))
			sm.ChangeState(sm.GetState((int)eEnemyState.Idle));
	}
	public void CheckTraceState()
	{
		if (CheckRadius(target.transform.position, attackDistance))
			sm.ChangeState(sm.GetState((int)eEnemyState.Attack));
		else if (CheckRadius(target.transform.position, traceDistance))
		{ 
			sm.ChangeState(sm.GetState((int)eEnemyState.Attack));
			MoveAndRotate(target.transform.position);
		}
		else if(!CheckRadius(target.transform.position, traceDistance))
			sm.ChangeState(sm.GetState((int)eEnemyState.Idle));
	}
	public void CheckAttackState()
	{
		if (CheckRadius(target.transform.position, attackDistance))	// ���� �Ÿ� üũ
		{
			if (Time.time > lastAttackTime)	// ���� �ֱ� üũ
			{
				//ToDo ����. Player�� Damage�޴°� ȣ���ؿ���
				Vector3 dir = target.transform.position - transform.position;
				transform.rotation = Quaternion.LookRotation(dir);
				lastAttackTime = Time.time + attackSpeed;
			}
		}
		else 
			sm.ChangeState(sm.GetState((int)eEnemyState.Trace));
	}
	public void MoveAndRotate(Vector3 _targetPos)
	{
		Vector3 dir = _targetPos - transform.position;
		transform.position = Vector3.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
		Quaternion rot = Quaternion.LookRotation(dir);
		Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
	}
	bool CheckRadius(Vector3 _point, float _radius)  //Ÿ�� ��ġ, �� �Ÿ�
	{
		Vector3 dir = _point - transform.position;
		float radiusSqr = _radius * _radius;		// r^2 : �ݰ� ���� ��
		if (dir.sqrMagnitude < radiusSqr)
			return true;
		return false;
	}
	public void Search()	//��ǥ�� Ž�� �� ����
	{
		if (Time.time < lastSearchTime) return;
		lastSearchTime = Time.time + searchSpeed;

		// �Ÿ� ���� ���� �÷��̾� �˻�
		if (CheckRadius(target.transform.position, traceDistance + traceDisOffset))
		{
			curTarget = target;
		}
		else curTarget = null;
	}
}

