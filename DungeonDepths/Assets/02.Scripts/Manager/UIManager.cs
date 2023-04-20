using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    List<GameObject> windowList = new List<GameObject>();
    GameObject curWindow;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitWindowList();
    }
    void InitWindowList()   // �ʱ�ȭ
    {
        var windows = transform.GetChild(0);
        for (int i = 0; i < windows.transform.childCount; i++)
        {
            windowList.Add(windows.GetChild(i).gameObject);
            windowList[i].SetActive(false);
        }
        curWindow = windowList[0];
        curWindow.SetActive(true);
    }
    public void OnWindow(Window _name) // Full Window �ѱ�
    {
        windowList[(int)_name].SetActive(true);
        curWindow = windowList[(int)_name];
        GameManager.Instance.Pause();
    }
    public void OffWindow(Window _name) // Full Window ����
    {
        windowList[(int)_name].SetActive(false);
        curWindow = null;
        GameManager.Instance.Resume();
    }
    public void OnClickCloseBtn()   // UI ���� ��ư
    {
        if (curWindow != null)
            curWindow.SetActive(false);
        GameManager.Instance.Resume();
    }
    public void OnClickOptionBtn()  // �ɼ�
    {
        OnWindow(Window.OPTION);
    }
    public void OnClickRestartBtn()   // ���� ����-> restart ��ư 
    {
        OffWindow(Window.GAMEOVER);
        GameManager.Instance.LoadMenuScene();
        OnWindow(Window.MAINMENU);
    }
    public void OnClickMainMenuPlayBtn() // ���θ޴����� �÷��� �ϴ� ��ư
    {
        OffWindow(Window.MAINMENU);
        StartCoroutine(LoadingWindow());
        GameManager.Instance.LoadPlayScene();

    }

    IEnumerator LoadingWindow() 
    {
        windowList[(int)Window.LOADING].SetActive(true);
        yield return null;
        windowList[(int)Window.LOADING].SetActive(false);
    }
    public void ShowMapInfo()
    {

    }

    public void ShowCardInfo(CardData _cardData)
    {

    }
    
}
