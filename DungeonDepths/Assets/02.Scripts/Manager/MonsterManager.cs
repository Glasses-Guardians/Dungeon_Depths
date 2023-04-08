using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterManagerState;
public class MonsterManager : MonoSingleton<MonsterManager>
{

    //ToDo: ���Ͱ� Ư�� �������� ������ ���� ����� ������������ ��Ȱ
    // or �׳� �ƹ� ������������ ��Ȱ
    // �������� ���۽� ���͸� ������ ������� ������������ �и����� ����

    /*
     * Wait : ��������(���)���� �� ��� ����
     * Spawn : ��������(���)���� �� ���� ���� , ������
     * Finish : �÷��̾� ������� ���� ���ӿ��� / �������� Ŭ����
     * ���±�踦 ���� ���¸� �ٲܰ�� �ٷ� �ش� ���¿� �����Ѵ�(Enter ����)
     */
    public enum MonsterManagerStates { Wait, Spawn, Finish };

    public StateMachine<MonsterManager> stateMachine;

    public int totalMeleeNum; // ������ �ٰŸ� ���� �ִ� ��
    public int totalRangeNum; // ������ ���Ÿ� ���� �ִ� ��

    public GameObject spawnPointsGroup;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> meleeMonsters = new List<GameObject>();
    public List<GameObject> rangeMonsters = new List<GameObject>();


    public float respawnTime = 5f;
    public float prevTime;
    public float curTime;

    //public float rangeSpawnTime;
    //public bool isStageStart;
    //public bool isGameOver = false;

    public List<Vector3> deadSpots; // ���Ͱ� ���� ��ġ�� ������ ����Ʈ

    public bool isGameClear;
    public bool isPlayerDead = false; // �÷��̾� ����� Ȯ���� �÷���
    public bool isRespawn = false;
    void Awake()
    {
        
        prevTime = Time.time;
        //rangeSpawnTime = new WaitForSeconds(2.0f);

        stateMachine = new StateMachine<MonsterManager>();

        stateMachine.AddState((int)MonsterManagerStates.Wait, new MonsterManagerState.Wait());
        stateMachine.AddState((int)MonsterManagerStates.Spawn, new MonsterManagerState.Spawn());
        stateMachine.AddState((int)MonsterManagerStates.Finish, new MonsterManagerState.Finish());

        //ù ���� ����
        stateMachine.InitState(this, stateMachine.GetState((int)MonsterManagerStates.Wait));
        totalMeleeNum = 20;
        //totalRangeNum = 10;
        var spawnPointsGroup = GameObject.Find("SpawnPoints");
        if(spawnPointsGroup != null)
        {
            spawnPointsGroup.GetComponentsInChildren<Transform>(spawnPoints);
        }
        spawnPoints.RemoveAt(0);
    }

    private void Start()
    {
        //���͵��� ����
        stateMachine.ChangeState(stateMachine.GetState((int)MonsterManagerStates.Spawn));
    }
    private void Update()
    {
        // ���ӽ��� �� 
        if(Time.time - prevTime >= respawnTime && isRespawn)
        {
            stateMachine.Execute();
        }

        CheckFinish();
        if(stateMachine.CurrentState == stateMachine.GetState((int)MonsterManagerStates.Finish))
        {
            stateMachine.ChangeState(stateMachine.GetState((int)MonsterManagerStates.Wait));
        }
    }
    void CheckFinish()
    {
        if(isGameClear || isPlayerDead)
        {
            Debug.Log(stateMachine.CurrentState);
            //Finish���·� ����
            stateMachine.ChangeState(stateMachine.GetState((int)MonsterManagerStates.Finish));
            Debug.Log(stateMachine.CurrentState + "sasdasd");
        }
    }

    /*
     * ���� ��ü�� ������ ȣ�� 
     * ���� �ð��� ��ġ�� ���
     * ���� ����Ʈ���� ���� ���͸� ���ܽ�Ŵ
     */
    public void RemoveMonster(GameObject monster)
    {
        prevTime = Time.time; // ���Ͱ� ����� �ð� ���
        isRespawn = true;

        //����ִ� ���� ������ ����Ʈ�� ���� ��ü�� �����Ѵٸ�
        if(meleeMonsters.Contains(monster))
        {

            //�ش� ��ü�� ���� ��ġ�� ������ List�� ��´�.
            deadSpots.Add(monster.transform.position);

            //�ش� ��ü�� List���� �����Ѵ�.
            meleeMonsters.Remove(monster);
        }
    }
}