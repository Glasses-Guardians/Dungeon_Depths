using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    string playerTag = "Player";
    // Start is called before the first frame update
    private void Awake()
    {

    }

    private void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.CompareTag(playerTag))
        {
            // Map UI �߻�
            // �̵��Լ�
        }
    }

    public void MoveStage()
	{

	}
}
