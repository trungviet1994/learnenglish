using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum eScreenType
{
    GAME_PLAY = 2,
    USER_MODE = 3,
    LEVEL_MODE = 4,
    CATEGORY = 5,
    GET_IN_ABC = 6,
    ANIMAL_SCREEN = 7
}

public enum ePopupType
{
    CATE_POPUP = 0,
    OPTION = 1,
    SETTING = 2,
    NONE = 3,
    ABOUT = 4,
    //UPDATE = 5,
    //DOWNLOAD = 6,
    REMIND = 7,
    EXIT = 8
}

[System.Serializable]
public class MyScreen
{
    public eScreenType m_screenType;
    public GameObject m_objectScreen;
}
[System.Serializable]
public class MyPopup
{
    public ePopupType m_popupType;
    public GameObject m_objectPopup;
}
public class ScreenManager : MonoSingleton<ScreenManager>
{
    public MyScreen[] m_arrayMyScreen;
    public MyPopup[] m_arrayMyPopup;
    public Dictionary<eScreenType, GameObject> m_dicScreen = new Dictionary<eScreenType, GameObject>();
    public Dictionary<ePopupType, GameObject> m_dicPopup = new Dictionary<ePopupType, GameObject>();

    private eScreenType m_currentScreen;
    private ePopupType m_currentPopup;
    private Stack m_myStackOfScreen = new Stack();

    public GeneralUIManager m_generalScreen;
    //public GameObject m_uiBackground;
    public eScreenType CurrentScreen
    {
        get { return m_currentScreen; }
        set { m_currentScreen = value; }
    }

    public ePopupType CurrentPopup
    {
        get
        {
            return m_currentPopup;
        }

        set
        {
            m_currentPopup = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        InitDictionary();
    }

    void Start()
    {
        CurrentPopup = ePopupType.NONE;
        //if (!PlayerPrefs.HasKey("letterCards"))
        //{
        //    ShowPopupScreen(ePopupType.DOWNLOAD);
        //}
        //else
        //{
        //    DestroyPopupByType(ePopupType.DOWNLOAD);
        //}
    }

    private void InitDictionary()
    {
        try
        {
            if (m_arrayMyScreen.Length > 0)
            {
                for (int i = 0; i < m_arrayMyScreen.Length; i++)
                {
                    m_dicScreen.Add(m_arrayMyScreen[i].m_screenType, m_arrayMyScreen[i].m_objectScreen);
                    m_arrayMyScreen[i].m_objectScreen.SetActive(false);
                }
            }

            if (m_arrayMyPopup.Length > 0)
            {
                for (int i = 0; i < m_arrayMyPopup.Length; i++)
                {
                    m_dicPopup.Add(m_arrayMyPopup[i].m_popupType, m_arrayMyPopup[i].m_objectPopup);
                    m_arrayMyPopup[i].m_objectPopup.SetActive(false);
                }
            }
        }
        catch
        {

        }
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(CurrentPopup != ePopupType.NONE)
            {
                HideCurrentPopup();
                return;
            }
            if(CurrentScreen == eScreenType.USER_MODE)
            {
                ShowPopupScreen(ePopupType.EXIT);
            }
            else
            {
                ShowScreenPrev();
            }
        }
#endif
    }

    private GameObject GetScreenByType(eScreenType type)
    {
        if (m_dicScreen.ContainsKey(type))
        {
            return m_dicScreen[type];
        }
#if UNITY_EDITOR
        Debug.Log("ko get duoc man hinh nay!");
#endif
        return null;
    }

    private GameObject GetPopupByType(ePopupType _type)
    {
        if (m_dicPopup.ContainsKey(_type))
        {
            return m_dicPopup[_type];
        }
#if UNITY_EDITOR
        Debug.Log("ko get duoc man hinh nay!");
#endif
        return null;
    }

    //public void DisableAllScreen()
    //{
    //    foreach (GameObject var in m_dicScreen.Values)
    //    {
    //        var.SetActive(false);
    //    }
    //}

    public void ShowScreenByType(eScreenType _type)
    {
        //QuestionManager.Instance.ActiveSpriteMainImage = true;
        GameObject objScreen = GetScreenByType(_type);
        HideScreenByType(CurrentScreen);
        if (objScreen)
        {
            objScreen.SetActive(true);
        }
        switch(_type)
        {
            case eScreenType.GAME_PLAY:
                AudioManager.Instance.StopAudioBackground();
                m_generalScreen.gameObject.SetActive(false);
                GamePlayConfig.Instance.GameStart = true;
                break;
            case eScreenType.USER_MODE:
                m_myStackOfScreen.Clear();
                AudioManager.Instance.PlayAudioBackground();
                m_generalScreen.gameObject.SetActive(false);
                //QuestionManager.Instance.ActiveSpriteMainImage = false;
                GamePlayConfig.Instance.GameStart = false;
                break;
            default:
                AudioManager.Instance.PlayAudioBackground();
                m_generalScreen.gameObject.SetActive(true);
                m_generalScreen.SetUp(_type);
                GamePlayConfig.Instance.GameStart = false;
                //QuestionManager.Instance.ActiveSpriteMainImage = false;
                break;
        }
        m_myStackOfScreen.Push(_type);
        CurrentScreen = _type;
        
        if(AnalyticsManager.Instance)
        {
            AnalyticsManager.Instance.ShowLogScreenByType(_type);
        }
    }

    public void ShowCurrentScreen()
    {
        GameObject objScreen = GetScreenByType(CurrentScreen);
        if (objScreen)
        {
            objScreen.SetActive(true);
        }
        
    }
    public void ShowScreenPrev()
    {
        eScreenType screenCurrent = (eScreenType)m_myStackOfScreen.Pop();
        if (screenCurrent != eScreenType.GAME_PLAY)
        {
            BaseEffectScreen effectScreen = GetScreenByType(screenCurrent).GetComponent<BaseEffectScreen>();
            //QuestionManager.Instance.ActiveSpriteMainImage = false;
            if (effectScreen)
            {
                effectScreen.m_myDelegate = CallBackCloseWindow;
                effectScreen.CloseWindow();
            }
        }
        else
        {
            //QuestionManager.Instance.ActiveSpriteMainImage = true;
            CallBackCloseWindow();
        }
    }
    private void CallBackCloseWindow()
    {
        HideCurrentPopup();
        eScreenType screenPrev = (eScreenType)m_myStackOfScreen.Pop();
        ShowScreenByType(screenPrev);
    }
    public void HideScreenByType(eScreenType type)
    {
        GameObject objScreen = GetScreenByType(type);

        if (objScreen)
        {
            objScreen.SetActive(false);
        }
    }

    #region POPUP..........................
    public void HideCurrentPopup()
    {
        if(CurrentPopup == ePopupType.NONE)
        {
            return;
        }
        GameObject objScreenPopup = GetPopupByType(CurrentPopup);
        if (objScreenPopup)
        {
            m_generalScreen.gameObject.SetActive(false);
            objScreenPopup.SetActive(false);
            CurrentPopup = ePopupType.NONE;
        }
        //ShowScreenByType(CurrentScreen);
        ShowCurrentScreen();
    }
    public void ShowPopupScreen(ePopupType _type)
    {
        GameObject objScreenPopup = GetPopupByType(_type);
        HideCurrentPopup();
        HideScreenByType(CurrentScreen);
        if(_type == ePopupType.OPTION || _type == ePopupType.SETTING )
        {
            m_generalScreen.gameObject.SetActive(true);
            m_generalScreen.SetUp(_type);
        }
        if (objScreenPopup)
        {           
            objScreenPopup.SetActive(true);
        }
        CurrentPopup = _type; 
    }

    //public void hidePopupScreen(ePopupType _type)
    //{
    //    GameObject objScreenPopup = GetPopupByType(_type);
    //    CurrentPopup = ePopupType.NONE;
    //    if (objScreenPopup)
    //    {
    //        objScreenPopup.SetActive(false);
    //    }
    //}

    public void DestroyPopupByType(ePopupType _type)
    {
        Destroy(GetPopupByType(_type));
        int index = GetIndexOfPopup(_type);
        m_arrayMyPopup = RemoveAt(m_arrayMyPopup, index);
    }

    private int GetIndexOfPopup(ePopupType _type)
    {
        for(int i=0;i<m_arrayMyPopup.Length;i++)
        {
            if (m_arrayMyPopup[i].m_popupType == _type)
            {
                return i;
            }
        }
        return 0;
    }
    public T[] RemoveAt<T>(T[] oArray, int idx)
    {
        T[] nArray = new T[oArray.Length - 1];
        for (int i = 0; i < nArray.Length; ++i)
        {
            nArray[i] = (i < idx) ? oArray[i] : oArray[i + 1];
        }
        return nArray;
    }
    #endregion...
}
