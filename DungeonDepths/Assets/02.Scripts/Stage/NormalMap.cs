using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMap : MapEntity
{
    public List<Vector3> boxSpawnPoints = new List<Vector3>();    // ���� ���� ���� Points
    public List<Vector3> EnemySpawnPoints = new List<Vector3>();  // ���� ���� Points    
    [SerializeField]
    private MapCore core; public MapCore Core { get; private set; }//�ھ�

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
