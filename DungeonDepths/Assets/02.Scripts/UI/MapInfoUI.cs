using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfoUI : MonoBehaviour
{
    public List<Sprite> mapPreviewList = new List<Sprite>();
    Text mapName;
    Text mapDifficulty;
    Image mapImage;
    Map selectedMap;

    public List<Button> buttonList = new List<Button>(); // ��ư ����Ʈ

    void Awake()
    {
        mapName = GameObject.Find("Text-ThemeTitle").GetComponent<Text>();
        mapDifficulty = GameObject.Find("Text-DifficultyTitle").GetComponent<Text>();
        mapImage = GameObject.Find("MapPreviewImg").GetComponent<Image>(); 
        for (int i = 0; i < buttonList.Count; i++)
        {
            // ��ư�� �� ������ ����
            int mapIndex = i;
            buttonList[i].onClick.AddListener(() =>
            {
                OnClick(mapIndex);
            });
        }
    }

    private void OnClick(int _mapIndex)
    {
        // �� ���� ����Ʈ���� ���õ� �� ������ ������
        selectedMap = StageManager.Instance.GetMapInfoList()[_mapIndex];

        // ���õ� �� ������ ����Ͽ� �� ������ ǥ��
        string _mapName = selectedMap.mapData.MapName;
        var _difficulty = selectedMap.mapData.Difficulty;
        mapName.text = "Name : " + _mapName;
        mapDifficulty.text = "Difficulty : " + _difficulty;

        mapImage.sprite = mapPreviewList[_mapIndex];
    }

    public void OnClickSelectBtn()
    {
        selectedMap.gameObject.SetActive(true);
        StageManager.Instance.StartStageMap(selectedMap);
    }


   
}
