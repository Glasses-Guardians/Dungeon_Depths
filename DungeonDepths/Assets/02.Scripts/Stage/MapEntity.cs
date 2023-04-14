using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public MapData mapData;
    [SerializeField]
    private Transform startPosition;    //�÷��̾� �� ���� ��ġ    
    private bool isClear = false;  //�� Ŭ���� ���� 
    public Transform StartPosition 
    { 
        get => startPosition; 
        set => startPosition = value; 
    }
    public bool IsClear
    {
        get => isClear;
        set => isClear = value;
    }
    public virtual void Awake()
    {
        Debug.Log("IsClear �ʱ� : " + isClear);
        startPosition = transform.GetChild(0).GetComponent<Transform>();
        gameObject.transform.position = mapData.Position;   // �� ��ġ �ʱ�ȭ
        // �ʱ�ȭ
    }
    public virtual void CheckIsClear() { }

   
}
