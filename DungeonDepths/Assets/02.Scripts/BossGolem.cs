using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossGolem : BossBase
{
    void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        
        BossHp = 10000;
        MoveSpeed = 1f;
        AttackDamage = 30f;
        
        TargetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        BossTransform = GetComponent<Transform>();
        TraceRange = 50f; // ������ ũ��� ����
        
        MeleeAngle = 60f;
        RangeAngle = 45f;
        AttackDelay = 3f;
        MeleeRange = 3f;
        BeamRange = 20f;
    }

    void Update()
    {
        CheckAlive();
        float dist = Vector3.Distance(TargetTransform.position, BossTransform.position);
        CheckState(dist);
        BossAction();
    }
}
