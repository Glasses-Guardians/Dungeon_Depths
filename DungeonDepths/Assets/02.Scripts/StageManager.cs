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




    public void MoveToStage()
    {
        BackToVillage();
        switch (StageIdx)
        {
            case 0:
                MoveToStage1();
                break;
            case 1:
                MoveToStage2();
                break;
            case 2:
                break;
        }

    }



    public void clear()
    {
        BackToVillage();
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

    public void BackToVillage()
    {
        _player.transform.position = new Vector3(0, 0, 0);
    }

    public void MoveToStage1()
    {
        _player.transform.position = new Vector3(0, 0, 0);
    }


    public void MoveToStage2()
    {
        _player.transform.position = new Vector3(0, 0, 0);
    }
    public void MoveToStage3()
    {
        _player.transform.position = new Vector3(0, 0, 0);
    }
}
