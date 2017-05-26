using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class BaseEffectScreen : MonoBehaviour {
    public RectTransform m_rectrfParent;

    private const float m_timeTransition = 0.35f;
    private const Ease m_easeType = Ease.OutSine;

    public delegate void MyDelegate();
    public MyDelegate m_myDelegate;

    private Vector2 m_anchorPositionMoveTo = new Vector2();
    private Vector2 m_anchorPositionStart = new Vector2();

    void Awake()
    {
        m_anchorPositionStart = m_rectrfParent.anchoredPosition;
        m_anchorPositionMoveTo = new Vector2(m_anchorPositionStart.x,m_anchorPositionStart.y- (m_rectrfParent.rect.height+75.0f));
        SetUpStartEffect();
    }

    public void OnEnable()
    {
        SetUpStartEffect();
    }
    private void SetUpStartEffect()
    {
        m_rectrfParent.anchoredPosition = m_anchorPositionMoveTo;
        DownMoveTop();
    }
   
    private void DownMoveTop()
    {
        m_rectrfParent.DOAnchorPos(m_anchorPositionStart, m_timeTransition).SetEase(m_easeType);
    }

    public void CloseWindow()
    {
        ScreenManager.Instance.m_generalScreen.Close();
        m_rectrfParent.DOAnchorPos(m_anchorPositionMoveTo, m_timeTransition).SetEase(m_easeType).OnComplete(CallBackClose);
    }
    private void CallBackClose()
    {
        m_myDelegate();
    }

    public void ClosePopup()
    {
        ScreenManager.Instance.HideCurrentPopup();
    }
}
