using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;

public class CardManager : MonoSingleton<CardManager>
{
    //public GameObject parent; // ������ �θ�
    [SerializeField]
    CardDatas cardDatas; // ��ũ���ͺ� ������Ʈ
    Transform cardParent;

    public GameObject cardPrefab;
    public GameObject player;
    [SerializeField]
    private List<CardData> normalCardList = new List<CardData>();
    [SerializeField]
    private List<CardData> rareCardList = new List<CardData>();
    private List<CardData> playerCardList = new List<CardData>();
    public void Awake()
    {
        
    }
    private void Start()
    {
        cardParent = GameObject.Find("Windows").transform.GetChild(2).GetChild(5).GetChild(0).transform;
        InitCardData();

        for (int i = 0; i < cardDatas.cardDataList.Count; i++)
        {
            GetCard(cardDatas.cardDataList[i]);
        }
    }
    void InitCardData()
    {
        foreach (var _data in cardDatas.cardDataList) // ��ũ���ͺ� ������Ʈ �����͸� ����Ʈ�� �Ű� ���� ��
        {
            if (_data.Rarity == CardRarity.NOMAL)
                normalCardList.Add(_data);
            else if(GameManager.Instance.CurPlayerClass == _data.CardClass || _data.CardClass == Class.NONE)
                rareCardList.Add(_data);
        }
    }
    public CardData NormalCard() // �������� ī��̱�
    {
        var _index = Random.Range(0, normalCardList.Count);
        var _card = normalCardList[_index];
        return _card;

    }
    public CardData RareCard() // ����ī�� ���� �̱�
    {
        var _index = Random.Range(0, rareCardList.Count);
        var _card = rareCardList[_index];
        return _card;
    }
    public CardData RandomCard()
    {
        var _random = Random.Range(0, 10);
        if (_random < 2)
            return RareCard();
        else
            return NormalCard();
    }
    public List<CardData> SelectRandomCards(int _cardNum)
    {
        var _selectedCards = new List<CardData>();
        var _cardData = CardManager.Instance.RandomCard();

        _selectedCards.Add(_cardData);
        while (_selectedCards.Count <= _cardNum)
        {
            _cardData = CardManager.Instance.RandomCard();
            if (!_selectedCards.Contains(_cardData))
                _selectedCards.Add(_cardData);
        }
        return _selectedCards;
    }
    public void GetCard(CardData _card) // ���� ���� ī��
    {
        InstantiateCard(_card);
        playerCardList.Add(_card);
        if(_card.Rarity == CardRarity.NOMAL)
            normalCardList.Remove(_card);
        else
            rareCardList.Remove(_card);
        // �ɷ� �Լ� ȣ��

        AbilityEffect aa = new AbilityEffect();
        aa.StatBoostEffect(player.GetComponent<PlayerBase>(), _card);
    }
    public void InstantiateCard(CardData _cardData)
    {
        var _cardObj = Instantiate(cardPrefab, cardParent);
        _cardObj.name = _cardData.CardName;
        var _card = _cardObj.GetComponent<Card>();
        _card.cardData = _cardData;
    }
}


