using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    private static StageManager instance = null;



 
    [HideInInspector]
    public int StageIdx = 0; //  �������� �ε���
    [HideInInspector]
    public string registery; // ��� �Ӽ�
    [HideInInspector]
    public bool IsClear = false;
    [SerializeField]
    GameObject _player = null;
    [SerializeField]
    bool isAproPotal=false;// ��Ż�� ���� �ߴ���

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




    



    public void clear()
    {
       
        switch (StageIdx)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
        }
        
    }

   
}
