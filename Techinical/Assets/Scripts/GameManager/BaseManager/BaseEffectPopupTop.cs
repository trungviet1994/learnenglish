using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class BaseEffectPopupTop : MonoBehaviour {
    private const float m_timeMove = 0.5f;
    private Ease m_easeTypeMove = Ease.OutCubic;

    public Image m_background;

    public delegate void CallBack();
    public CallBack m_myDelegate;
    private Color m_colorBackground;

    public RectTransform m_rectrfParentMove;
    private Vector2 m_anchorPositionStart = new Vector2();
    private Vector2 m_anchorPositionMoveToDown = new Vector2();

    void Awake()
    {
        m_anchorPositionStart = m_rectrfParentMove.anchoredPosition;
        m_anchorPositionMoveToDown = new Vector2(m_anchorPositionStart.x,m_anchorPositionStart.y+(m_rectrfParentMove.rect.height+75));
        m_colorBackground = m_background.color;
    }

    void OnEnable()
    {
        m_background.color = m_colorBackground;
        EffectMoveDown();
    }
    private void EffectMoveDown()
    {
        m_rectrfParentMove.anchoredPosition = m_anchorPositionMoveToDown;
        m_rectrfParentMove.DOAnchorPos(m_anchorPositionStart,m_timeMove).SetEase(m_easeTypeMove);
        DOTween.ToAlpha(() => m_background.color, x => m_background.color = x, 0, m_timeMove).From();
    }
    public void EffectClose()
    {
        m_rectrfParentMove.DOAnchorPos(m_anchorPositionMoveToDown, m_timeMove).SetEase(m_easeTypeMove).OnComplete(CallBackComplete);
        DOTween.ToAlpha(() => m_background.color, x => m_background.color = x, 0, m_timeMove);
    }

    public void CallBackComplete()
    {
        m_myDelegate();
    }
}
