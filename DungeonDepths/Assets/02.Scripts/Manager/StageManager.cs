using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{

    public int stageIdx = 0; //  �������� �ε���
    public GameObject[] stages; // ��������
    //[HideInInspector]
    //public string registery; // ��� �Ӽ�
    //[HideInInspector]
    //public bool isClear = false;
    //Stage1 stage1;

    public GameObject _player = null;
    public bool isAporPortal = false;// ��Ż�� ���� �ߴ���
    public GameObject portal;

    public void Awake()
    {
        

    }

    public void MoveStage() // �÷��̾�� ȣ�� OntrigerrEnter�� �̿� �±װ� ��Ż�̶��
	{
        // ���ǹ����� Ŭ������ �ε��� �˻��� �� Ŭ���� ���� ���� �����
        // ���������� ���� �ε����� ���ӿ�����Ʈ(�������� ��)�� ��Ȱ��ȭ �ѵ�
        // Ŭ���� ���� �̵� �� ��尡 ����� ���������� �ε����� ã�� �� Ȱ��ȭ
        // ��Ż�� �̿��� �÷��̾� ��ġ �缳��
	}


}
