using UnityEngine;

public class MapCore : MonoBehaviour
{
	public Transform Position { get; private set; }   
	public bool IsDestroyed { get; private set; }
	//TODO ����. �ı� �̺�Ʈ �׽�Ʈ ��
	public delegate void EventHandler();
	public event EventHandler OnEvent;
	// ��Ż ���� ���⼭ �ϵ���

	private void OnEnable()	// Ȱ��ȭ�� �� �ʱ�ȭ�ǵ���
    {
		IsDestroyed = false;
		Position = transform;
    }
    private void Start()
    {
		OnEvent += DestroyedCore;
	}
	private void DestroyedCore()
	{
		//TODO ����
		IsDestroyed = true;
		Debug.Log("MapCore���� �̺�Ʈ �߻� : " + IsDestroyed);
		//gameObject.SetActive(false);
	}
    private void OnDisable()
    {
		//�ӽ� �׽�Ʈ
		OnEvent();
	}
}
