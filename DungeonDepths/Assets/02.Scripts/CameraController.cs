using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 _delta = new Vector3(0f, 2f, 5f);    // ī�޶� offset �� : ��ġ ������
    public GameObject _player = null;
    public Camera mainCamera;
    Vector3 offset;
    float zoomSpeed = 10f;
    float rotationX = 0.0f;         // X�� ȸ����
    float rotationY = 0.0f;         // Y�� ȸ����
    float speed = 100.0f;           // ȸ���ӵ�

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        _player = GameObject.FindWithTag("Player").gameObject;
    }
    void Start()
    {
        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform);
        offset = transform.position - _player.transform.position;
    }
    void Update()
    {
        transform.position = _player.transform.position + offset;
        Rotate();
        Zoom();
        offset = transform.position - _player.transform.position;
    }
    void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            mainCamera.fieldOfView += distance;
        }
    }
    void Rotate()
    {
        // ���콺�� ��������,

        // ���콺 ��ȭ���� ���, �� ���� ��ŸŸ�Ӱ� �ӵ��� ���ؼ� ȸ���� ���ϱ�
        rotationX = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
        rotationY = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

        // �� ������ ȸ��
        // Y���� ���콺�� ������ ī�޶�� �ö󰡾� �ϹǷ� �ݴ�� ����
        transform.RotateAround(_player.transform.position, Vector3.right, -rotationY);
        transform.RotateAround(_player.transform.position, Vector3.up, rotationX);

        // ȸ���� Ÿ�� �ٶ󺸱�
        transform.LookAt(_player.transform.position);
    }
}