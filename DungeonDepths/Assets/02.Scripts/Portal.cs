using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    string playerTag = "Player";
	[SerializeField]
	ParticleSystem[] ps = new ParticleSystem[4];
    private void Awake()
    {
		// ����Ŭ �迭�� �Űܴ��
	}

	private void OnEnable()
	{
		// �� �׸� �޾ƿͼ� �׸��� �´� ���� ��Ż Ȱ��ȭ �����ֱ�
	}

	private void OnDisable()
	{
		// ��Ȱ��ȭ	
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag(playerTag))
		{
			//UI.���� (�Ŵ������� �Լ�ȣ��)
		}
	}
	
}
