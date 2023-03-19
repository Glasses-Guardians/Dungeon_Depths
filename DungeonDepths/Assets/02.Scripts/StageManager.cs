using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    private static StageManager instance = null;
 
    [HideInInspector]
    public int stageIdx = 0; //  �������� �ε���
    [HideInInspector]
    public string registery; // ��� �Ӽ�
    [HideInInspector]
    public bool isClear = false;
    Stage1 stage1;
    
    public GameObject _player = null;
     public  bool isAporPortal=false;// ��Ż�� ���� �ߴ���
    public GameObject portal;

    public static StageManager Instance
    {
        get
        {
            if (null == instance)
            {   
                return null;
            }
            return instance;
        }

        
    }


    public void Awake()
    {
        portal = GameObject.Find("Portal");

        _player=GameObject.FindWithTag("Player");
        stage1 = GetComponent<Stage1>();   

    }




    public void Move(Vector3  Target)
    {
        _player.transform.position = Target;
    }
  

   
}
