using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackMonster : MonsterBase
{
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void Init()
    {
        AttackDistance = 3f;
        TraceDistance = 10f;
        // damage ���� �Լ� ȣ��
        // moveSpeed ���� �Լ� ȣ��
        // attackSpeed ���� �Լ� ȣ��
        //monsterStats.CurHP = monsterStats.MaxHP;
    }
}