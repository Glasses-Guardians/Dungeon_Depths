using System.Collections.Generic;
using UnityEngine;

public class NormalMap : Map
{
    [SerializeField]
    private MapCore core;
    public List<Vector3> boxSpawnPoints = new List<Vector3>();    // ���� ���� ���� Points
    public List<Vector3> EnemySpawnPoints = new List<Vector3>();  // ���� ���� Points    
    public MapCore Core     //�ھ�
    { 
        get; 
        private set; 
    }   

    public override void Awake()
    {
        base.Awake();
        core = GetComponentInChildren<MapCore>();
        /* TODO ����
         * ����, ���� ���� ��ġ ���� �� �Ľ�
         * ���� ��ġ ���� �� �Ľ�
         * core �ı� �̺�Ʈ ����
        */
        core.OnEvent += () => { IsClear = true; Debug.Log("Map �̺�Ʈ ȣ�� : " + IsClear); };
    }
    // TODO ���� ���� ���� �Լ� ����?
}
