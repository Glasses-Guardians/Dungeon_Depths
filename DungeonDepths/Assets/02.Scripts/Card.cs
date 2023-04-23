using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	public CardData cardData;
	[SerializeField]
	Image cardImg;
	[SerializeField]
	Text cardDesc;
	private Button button;
	

	private void Start()
	{
		cardImg = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
		cardDesc = transform.parent.parent.GetChild(1).GetChild(0).GetComponent<Text>();
		cardImg.sprite = cardData.Sprite;
        button = GetComponent<Button>(); //버튼 component 가져오기
        button.onClick.AddListener((ShowCardInfo)); //인자가 없을 때 함수 호출
    }

	void ShowCardInfo()
    {
		if(UIManager.Instance.WindowList[(int)EnumTypes.Window.OPTION] == UIManager.Instance.CurWindow)
			cardDesc.text = cardData.CardDesc;

	}
}
