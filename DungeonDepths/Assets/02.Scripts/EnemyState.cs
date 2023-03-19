using UnityEngine;

namespace EmemyState
{
	class Idle : State<EnemyBase>
	{
		public override void Enter(EnemyBase e)
		{
			e.IdleTime = Time.time - e.PrevTime;
			e.Search();
		}
		public override void Execute(EnemyBase e)
		{
			e.CheckIdleState();
		}
		public override void Exit(EnemyBase e) { }
	}

	class Patroll : State<EnemyBase>
	{
		public override void Enter(EnemyBase e)
		{
			e.wayPoints.MoveNextPoint();
		}
		public override void Execute(EnemyBase e)
		{
			e.MoveAndRotate(e.wayPoints.destinationPoint);
			e.CheckPatrolState();
		}
		public override void Exit(EnemyBase e) {}
	}
	class Trace : State<EnemyBase>
	{
		public override void Enter(EnemyBase e){}
		public override void Execute(EnemyBase e)
		{
			e.CheckTraceState();
		}
		public override void Exit(EnemyBase e) {}
	}
	class Attack : State<EnemyBase>
	{
	//Attack������ ����� : trace���·� change
		public override void Enter(EnemyBase e) {}
		public override void Execute(EnemyBase e)
		{
			e.CheckAttackState();
			// �����ϴ� ��
		}
		public override void Exit(EnemyBase e) {}
	}
	class Die : State<EnemyBase>
	{
		public override void Enter(EnemyBase e) {}
		public override void Execute(EnemyBase e) {}
		public override void Exit(EnemyBase e) {}
	}
}