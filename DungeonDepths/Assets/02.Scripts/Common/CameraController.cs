using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Vector3 delta = new Vector3(0f, 2f, 5f);    // ī�޶� offset �� : ��ġ ������
	public GameObject player = null;
	public Camera mainCamera;
	public Transform cameraPivotTr;
	Vector3 offset;
	float zoomSpeed = 10f;
	float rotationX = 0.0f;         // X�� ȸ����
	float rotationY = 0.0f;         // Y�� ȸ����
	float speed = 100.0f;           // ȸ���ӵ�

	void Awake()
	{
		player = GameObject.FindWithTag("Player").gameObject;
		
	}
	void Start()
	{
		transform.position = player.transform.position + delta;
		transform.LookAt(player.transform.position + Vector3.up * 1f);
		offset = transform.position - player.transform.position;
	}
	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
		Rotate();
		Zoom();
		offset = transform.position - player.transform.position;
	}
	void Zoom()
	{
		float distance = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		float newDeltaMagnitude = delta.magnitude - distance;
		newDeltaMagnitude = Mathf.Clamp(newDeltaMagnitude, 2f, 10f);

		delta = delta.normalized * newDeltaMagnitude;
	}
	void Rotate()
	{
		// ���콺�� ��������,
		// ���콺 ��ȭ���� ���, �� ���� ��ŸŸ�Ӱ� �ӵ��� ���ؼ� ȸ���� ���ϱ�
		float _rotationX = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
		float _rotationY = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

		// ȸ������ ���� x�� ȸ����(rotationY)�� ���Ͽ� �ּҰ��� �ִ밪�� ����
		rotationX += _rotationX;
		rotationY -= _rotationY;
		rotationY = Mathf.Clamp(rotationY, 5f, 70f);

		// �� ������ ȸ��
		transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);

		// ī�޶� ��ġ ����
		Vector3 _negDistance = new Vector3(0.0f, 0.0f, -delta.magnitude);
		Vector3 _position = transform.rotation * _negDistance + player.transform.position;
		transform.position = _position + Vector3.up * 1f;
	}
}
