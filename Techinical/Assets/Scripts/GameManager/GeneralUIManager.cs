using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
public enum eTextTitleType
{
    LEVEL = 1,
    EASY = 2,
    NORMAL = 3,
    HARD =4,
    LEARNABC=5,
    SETTING = 6,
    ABOUT = 7,
    PAUSE = 8,
    CATEGORY = 9
}

[System.Serializable]
public class TextTitleConfig
{
    public eTextTitleType m_textType;
    public GameObject m_value;
}
public class GeneralUIManager : MonoBehaviour
{
    public Image m_imgIconButton;
    [Header("image icon button...")]
    public Sprite m_imgClose;
    public Sprite m_imgBack;
    [Header("Set text tieng viet...")]
    public TextTitleConfig[] m_arraytextTitle;
    private Dictionary<eTextTitleType, GameObject> m_dictionaryTitleText = new Dictionary<eTextTitleType, GameObject>();
    
    public Transform m_topParent;
    [SerializeField]
    private float m_positionSetTopY;
    [SerializeField]
    private float m_startTopPositionY;
    [SerializeField]
    private float m_timeMove = 0.5f;
    [SerializeField]
    private Ease m_easeTypeMove = Ease.InElastic;
    private float m_distanceTop = 2.5f;

    private GameObject m_currentTitleText = null;
    
    [SerializeField]
    private BackSceneButton m_backSceneHandle;

    public RectTransform m_rectrfMoveParent;
    private Vector2 m_anchorPositionStart=new Vector2();
    private Vector2 m_anchorPositionMoveTo = new Vector2();

    void Awake()
    {
        // initiate position
        m_startTopPositionY = m_topParent.position.y;
        m_positionSetTopY = m_startTopPositionY + m_distanceTop;
        //new anchor position
        m_anchorPositionStart = m_rectrfMoveParent.anchoredPosition;
        m_anchorPositionMoveTo = new Vector2(m_anchorPositionStart.x, m_anchorPositionStart.y + m_rectrfMoveParent.rect.height);
        m_rectrfMoveParent.anchoredPosition = m_anchorPositionMoveTo;
        InitDictionary();
    }

    private void InitDictionary()
    {
        for(int i =0;i< m_arraytextTitle.Length;i++)
        {
            m_arraytextTitle[i].m_value.SetActive(false);
            m_dictionaryTitleText.Add(m_arraytextTitle[i].m_textType,m_arraytextTitle[i].m_value);
        }
    }
    private GameObject GetTitleTextByType(eTextTitleType _typeOfTitle)
    {
        if (m_dictionaryTitleText.ContainsKey(_typeOfTitle))
        {
            return m_dictionaryTitleText[_typeOfTitle];
        }
        return null;
    }

    public void SetupTitleTextWithType(eTextTitleType _typeOfTitle)
    {
        if(m_currentTitleText!=null)
        {
            m_currentTitleText.SetActive(false);
        }
        m_currentTitleText = GetTitleTextByType(_typeOfTitle);
        if (m_currentTitleText)
        {
            m_currentTitleText.SetActive(true);
        }
    }

    private void SetButtonBackIcon(bool _isBack)
    {
        if(_isBack)
        {
            m_imgIconButton.sprite = m_imgBack;
        }
        else
        {
            m_imgIconButton.sprite = m_imgClose;
        }
    }
    private void SetUpStartEffect()
    {
        m_rectrfMoveParent.anchoredPosition = m_anchorPositionMoveTo;
    }

    private void TopMoveDown()
    {
        m_rectrfMoveParent.DOAnchorPos(m_anchorPositionStart, m_timeMove).SetEase(m_easeTypeMove);
    }

    public void Close()
    {
        m_rectrfMoveParent.DOAnchorPos(m_anchorPositionMoveTo, m_timeMove).SetEase(m_easeTypeMove);
    }
    public void SetUp(eScreenType _screenType)
    {
        m_backSceneHandle.onScreenHandler = true;
        SetButtonBackIcon(true);
        switch (_screenType)
        {
            case eScreenType.USER_MODE:
                SetUpStartEffect();
                SetButtonBackIcon(false);
                break;
            case eScreenType.LEVEL_MODE:
                SetupTitleTextWithType(eTextTitleType.LEVEL);
                TopMoveDown();
                break;
            case eScreenType.GET_IN_ABC:
                TopMoveDown();
                SetupTitleTextWithType(eTextTitleType.LEARNABC);
                break;
            case eScreenType.CATEGORY:
                TopMoveDown();
                switch(GamePlayConfig.Instance.ModeLevel)
                {
                    case eModeLevel.EASY:
                        SetupTitleTextWithType(eTextTitleType.EASY);
                        break;
                    case eModeLevel.NORMAL:
                        SetupTitleTextWithType(eTextTitleType.NORMAL);
                        break;
                    case eModeLevel.HARD:
                        SetupTitleTextWithType(eTextTitleType.HARD);
                        break;
                }
                break;
        }
    }

    public void SetUp(ePopupType _popupType)
    {
        TopMoveDown();
        m_backSceneHandle.onScreenHandler = false;
        switch (_popupType)
        {
            case ePopupType.OPTION:
                SetupTitleTextWithType(eTextTitleType.PAUSE);
                break;
            case ePopupType.SETTING:
                SetupTitleTextWithType(eTextTitleType.SETTING);
                break;
            case ePopupType.ABOUT:
                SetupTitleTextWithType(eTextTitleType.ABOUT);
                break;
        }
        SetButtonBackIcon(false);
    }

}
