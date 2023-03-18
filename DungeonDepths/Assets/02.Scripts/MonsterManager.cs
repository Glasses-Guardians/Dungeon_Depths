using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterManagerState;
public class MonsterManager : MonoSingleton<MonsterManager>{
    /*
     * Wait : ��������(���)���� �� ��� ����
     * Spawn : ��������(���)���� �� ���� ��ġ
     * Regeneration : ���� ������
     * Finish : �÷��̾� ������� ���� ���ӿ��� / �������� Ŭ����
     */
    public enum MonsterManagerStates { Wait, Spawn, Finish };

    public StateMachine<MonsterManager> stateMachine;

    public int totalMeleeNum; // ������ �ٰŸ� ���� �ִ� ��
    public int totalRangeNum; // ������ ���Ÿ� ���� �ִ� ��

    public GameObject spawnPointsGroup;

    //���� ���� ������
    public List<Transform> spawnPoints = new List<Transform>();

    //����ִ� ���� ���� ����Ʈ
    public List<GameObject> meleeMonsters = new List<GameObject>();

    //����ִ� ���Ÿ� ���� ����Ʈ
    public List<GameObject> rangeMonsters = new List<GameObject>();

    public List<float> respawnTimes = new List<float>();
    //public List<GameObject> aliveMonsters = new List<GameObject>();

    public float respawnTime = 5f;
    public float prevTime;
    public float curTime;

    //public float rangeSpawnTime;
    //public bool isStageStart;
    //public bool isGameOver = false;

    public List<Vector3> deadSpots; // ���Ͱ� ���� ��ġ�� ������ ����Ʈ

    public bool isGameOver;
    public bool isPlayerDead;
    public bool isRespawn = false;
    void Awake() {

        prevTime = Time.time;
        //rangeSpawnTime = new WaitForSeconds(2.0f);

        stateMachine = new StateMachine<MonsterManager>();

        //����� ���µ� �߰�
        stateMachine.AddState((int)MonsterManagerStates.Wait, new MonsterManagerState.Wait());
        stateMachine.AddState((int)MonsterManagerStates.Spawn, new MonsterManagerState.Spawn());
        stateMachine.AddState((int)MonsterManagerStates.Finish, new MonsterManagerState.Finish());

        //ù ���� ����
        stateMachine.InitState(this, stateMachine.GetState((int)MonsterManagerStates.Wait));
        totalMeleeNum = 20;
        //totalRangeNum = 10;
        var spawnPointsGroup = GameObject.Find("SpawnPoints");
        if(spawnPointsGroup != null) {
            spawnPointsGroup.GetComponentsInChildren<Transform>(spawnPoints);
        }
        spawnPoints.RemoveAt(0);
    }

    private void Start() 
    {
        stateMachine.ChangeState(stateMachine.GetState((int)MonsterManagerStates.Spawn));
    }
    private void Update() {
        if (Time.time - prevTime >= respawnTime && isRespawn) {
            stateMachine.Execute();
        }
        
        CheckFinish();
        if (stateMachine.currentState == stateMachine.GetState((int)MonsterManagerStates.Finish)) {
            stateMachine.ChangeState(stateMachine.GetState((int)MonsterManagerStates.Wait));
        }
    }
    void CheckFinish() {
        if (isGameOver || isPlayerDead) {
            Debug.Log(stateMachine.currentState);
            stateMachine.ChangeState(stateMachine.GetState((int)MonsterManagerStates.Finish));
            Debug.Log(stateMachine.currentState + "sasdasd");
        }
    }
  
    // ���� Ŭ�������� �ش� ��ü�� ������ ȣ��
    public void RemoveMonster(GameObject monster) {
        prevTime = Time.time;
        isRespawn = true;
        //����ִ� ���� ������ ����Ʈ�� ���� ��ü�� �����Ѵٸ�
        if (meleeMonsters.Contains(monster)) {
            
            //�ش� ��ü�� ���� ��ġ�� ������ List�� ��´�.
            deadSpots.Add(monster.transform.position);
            
            //�ش� ��ü�� List���� �����Ѵ�.
            meleeMonsters.Remove(monster);
        }
    }
}