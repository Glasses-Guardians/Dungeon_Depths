using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    bool isOpen;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            // UI ���� ��ȣ�ۿ�;
            Debug.Log("���ڰ����� ��");
            if (Input.GetButtonDown("Interaction") && !isOpen)
            {
                Debug.Log("��ȣ�ۿ�");
                OpenChest();
                Invoke("DisableChest", 2f);
            }
        }
    }

    public void OpenChest()
    {
        isOpen = true;
        anim.SetTrigger("Open");
        // �κ�ũ�� Ư��ī�� UI 1������ �����Ű��~
    }

    public void DisableChest()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }
}
