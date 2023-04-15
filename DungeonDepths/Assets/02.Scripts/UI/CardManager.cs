using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardRarity { NOMAL, RARE };
public class CardManager : MonoBehaviour
{
	public List<GameObject> cardObjList = new List<GameObject>(); // ������ ����Ʈ
	public GameObject parent; // ������ �θ�
	public List<CardData> cardList = new List<CardData>(); // ��ũ���ͺ� ������Ʈ���� �̾ƿ� ������ ����Ʈ
	public CardDatas cardDatas; // ��ũ���ͺ� ������Ʈ

	public void Awake()
	{
	}

	private void Start()
	{
		InitCardData();
	}

	private void InitCardData() 
	{
		foreach (var _data in cardDatas.cardDataList) // ��ũ���ͺ� ������Ʈ �����͸� ����Ʈ�� �Ű� ���� ��
		{
			cardList.Add(_data);
		}

		for (int i = 0; i < cardList.Count; i++) // Ǯ�Ŵ������� �����Ҵ��� �����տ� (��ũ���ͺ� ������Ʈ���� �ű� ����Ʈ)������ �Ű��ֱ�
		{
			GameObject _list = parent.transform.GetChild(i).gameObject;
			var _cardObjList = _list.GetComponent<Card>();
			_cardObjList.cardData.CardName = cardList[i].CardName;
			_cardObjList.cardData.CardDesc = cardList[i].CardDesc;
			_cardObjList.cardData.Rarity = cardList[i].Rarity;
			_cardObjList.cardData.Value = cardList[i].Value;
			_cardObjList.cardData.Sprite = cardList[i].Sprite;
			cardObjList.Add(_list);
		}
	}

}
