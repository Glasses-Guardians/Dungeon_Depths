using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    string playerTag = "Player";
	
    private void Awake()
    {
		
	}

	void OnEnable()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag(playerTag))
		{
			//UI.���� (�Ŵ������� �Լ�ȣ��)
		}
	}
	
}
