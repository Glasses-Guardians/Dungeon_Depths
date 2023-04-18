using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField]
    List<Map> mapInfoList = new List<Map>();
    Map curMap;
    GameObject player;

    public List<Map> GetMapInfoList()
    {
        return mapInfoList;
    }
    public void Awake()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        InitMapList();
    }

    void InitMapList()
    {
        var _map = GameObject.Find("Map").transform;
        for (int i = 0; i < _map.transform.childCount; i++)
        {
            mapInfoList.Add(_map.GetChild(i).GetComponent<Map>());
            if (i != 0)
                mapInfoList[i].gameObject.SetActive(false);
        }
        curMap = mapInfoList[0];
    }

    public void MoveStage() // �÷��̾�� ȣ�� OntrigerrEnter�� �̿� �±װ� ��Ż�̶��
	{
        // ���ǹ����� Ŭ������ �ε��� �˻��� �� Ŭ���� ���� ���� �����
        // ���������� ���� �ε����� ���ӿ�����Ʈ(�������� ��)�� ��Ȱ��ȭ �ѵ�
        // Ŭ���� ���� �̵� �� ��尡 ����� ���������� �ε����� ã�� �� Ȱ��ȭ
        // ��Ż�� �̿��� �÷��̾� ��ġ �缳��
	}

    public void SetStartPos(Map _selectedMap)
    {
        UIManager.Instance.OffWindow(Window.MAP);
        curMap.gameObject.SetActive(false);
        curMap = _selectedMap;
        player.transform.position = curMap.StartPosition.position;
    }

}
