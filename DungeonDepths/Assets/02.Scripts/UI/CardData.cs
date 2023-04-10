using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData// ������ ī�� ������
{
	[SerializeField] CardRarity rarity; public CardRarity Rarity { get { return rarity; } }
	[SerializeField] string cardName; public string CardName { get{ return cardName; } }
	[SerializeField] string cardDesc; public string CardDesc { get { return cardDesc; } }
	[SerializeField] Sprite sprite; public Sprite Sprite { get { return sprite; } }
}
