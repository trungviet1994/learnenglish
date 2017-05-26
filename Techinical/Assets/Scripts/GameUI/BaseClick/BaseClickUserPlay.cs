using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class BaseClickUserPlay : BaseClickButton
{
    [SerializeField]
    private eUserPlayMode m_userModePlay = eUserPlayMode.SINGLE_PLAY;

    //private RectTransform m_rtrfChild;
    //private Vector2 m_anchorPositionOfChild;

    //public override void InitStart()
    //{
    //    m_rtrfChild = transform.GetChild(0).GetComponent<RectTransform>();
    //    m_anchorPositionOfChild = new Vector2(m_rtrfChild.anchoredPosition.x,m_rtrfChild.anchoredPosition.y -20); 
    //}

    public override void OnClicked()
    {
        GamePlayConfig.Instance.UserPlayMode = m_userModePlay;
        ScreenManager.Instance.ShowScreenByType(eScreenType.LEVEL_MODE);
        base.OnClicked();
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (m_rtrfChild)
    //    {
    //        Debug.Log("ok");
    //        m_rtrfChild.anchoredPosition = new Vector2(m_anchorPositionOfChild.x, m_anchorPositionOfChild.y-20);
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (m_rtrfChild)
    //    {
    //        m_rtrfChild.anchoredPosition = m_anchorPositionOfChild;
    //    }
    //}
}
