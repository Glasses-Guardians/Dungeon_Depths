using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalBossState
{
    #region ���� : ���
    class Idle : State<FinalBoss>
    {
        float stateDuration;
        float stateEnterTime;

        public override void Enter(FinalBoss f)
        {

            Debug.Log("��� ���� ����");
            if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle))
                stateDuration = 2f;
            else
                stateDuration = 3f;
            
            stateEnterTime = Time.time;
            f.animator.SetFloat("MoveSpeed", 0);
            //f.shouldCombo = false;
            for(int i = 1; i <= 3; i++)
                f.animator.SetBool("Combo" + i, false);
        }
        public override void Execute(FinalBoss f)
        {
            Debug.Log("��� ���� ��");
            if(Time.time - stateEnterTime < stateDuration) return;
            //f.Rotation();
            //f.Trace();
            f.CheckTraceState();
        }
        public override void Exit(FinalBoss f)
        {
            //Debug.Log("��� ���� ����");
        }
    }
    #endregion

    #region �ļ�Ÿ ���� ����
    class AttackIdle : State<FinalBoss>
    {
        int comboIndex;
        float firstAtkTime;
        public override void Enter(FinalBoss f)
        {
            firstAtkTime = Time.time;
        }
        public override void Execute(FinalBoss f)
        {
            // 0.7�� + @ ���� ������
            if(Time.time - firstAtkTime < 0.8f) return;

            //f.Rotation();

            // �޺� ������ �Ҽ� �ִ� �ð� ����
            // ������ �÷��̾ ��Ÿ� �ȿ� �ְ�
            // �̸� ������ �����ߴٸ�
            if(Time.time - firstAtkTime <= f.comboDuration && f.ShouldCombo(out comboIndex))
            {
                //if(f.ShouldCombo(out comboIndex))
                //{
                //if(f.precedingAttacks[comboIndex - 1])
                //{

                //Debug.Log("�޺� ���� : " + comboIndex);
                //f.animator.SetTrigger("Combo" + comboIndex);
                if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack1))
                    f.animator.SetBool("Combo1", true);
                else if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack2))
                    f.animator.SetBool("Combo2", true);
                //else if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack3))
                else
                    f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Idle));
                //f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Idle));
                //f.precedingAttacks[comboIndex - 1] = false;
                //}
                //}
            }
            else
            {
                Debug.Log("�޺� ���� ����");
                f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Idle));
            }
        }
        public override void Exit(FinalBoss f)
        {

        }
    }
    #endregion
    class Trace : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
            Debug.Log("�߰ݻ��� ����");
            f.animator.SetFloat("MoveSpeed", 6.5f);
        }
        public override void Execute(FinalBoss f)
        {
            //Debug.Log("�߰ݻ��� ����");
            f.Rotation();
            f.CheckAttackState();
            f.Trace();
        }
        public override void Exit(FinalBoss f)
        {
            //Debug.Log("�߰ݻ��� ����");
        }
    }
    /// <summary>
    /// ������ ������ ��� ���Ӱ��� ����
    /// �÷��̾ ��Ÿ����� �ִٰ� �ǴܵǸ� ������ ���� ��
    /// �ٽ� �÷��̾���� �Ÿ��� �Ǵ��ϰ� ������ �÷��̾ ��Ÿ�
    /// ���� �ִٸ� �ļӰ����� �����Ѵ�.
    /// ���ݻ��� -> �Ÿ� üũ -> �ļӰ��� or �������� ��ȯ
    /// </summary>
    class MeleeAttack1 : State<FinalBoss>
    {
        //int comboIndex;
        //float firstAtkTime;
        public override void Enter(FinalBoss f)
        {
            Debug.Log("����1 ����");
            // ���� ��� ����
            f.animator.SetTrigger("Attack1Trigger");
            // ùŸ ���ݸ���� ����� �ð��� ����Ѵ�.
            // firstAtkTime = Time.time;
            //f.precedingAttacks[0] = true;
        }
        public override void Execute(FinalBoss f)
        {
            //Debug.Log("����1 ������");
            f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle));
            //if(Time.time - firstAtkTime <= f.comboDuration)
            //{
            //    if(f.ShouldCombo(out comboIndex))
            //        Debug.Log("�޺� ���� : " + comboIndex);
            //        f.animator.SetTrigger("Combo" + comboIndex);
            //}
            //f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Idle));
        }

        public override void Exit(FinalBoss f)
        {
            //Debug.Log("����1 ����");
        }
    }
    class MeleeAttack2 : State<FinalBoss>
    {
        //int comboIndex;
        public override void Enter(FinalBoss f)
        {
            Debug.Log("����2 ����");
            f.animator.SetTrigger("Attack2Trigger");
            //f.precedingAttacks[1] = true;
        }
        public override void Execute(FinalBoss f)
        {
            //if(f.ShouldCombo(out comboIndex))
            //    f.animator.SetTrigger("Combo" + comboIndex);
            //else f.animator.SetBool("isAttack2", false);
            f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle));
        }
        public override void Exit(FinalBoss f)
        {
            //Debug.Log("����2 ����");
        }
    }
    class MeleeAttack3 : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
        }
        public override void Execute(FinalBoss f)
        {
        }
        public override void Exit(FinalBoss f)
        {

        }
    }


    class Die : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {

        }
        public override void Execute(FinalBoss f)
        {
        }
        public override void Exit(FinalBoss f)
        {

        }
    }
}
