using UnityEngine;
using System.Collections;

public class HidePopupButton : BaseClickButton
{
    public BaseEffectPopupTop m_popup;
    public override void OnClicked()
    {
        base.OnClicked();
        if(m_popup)
        {
            m_popup.m_myDelegate = CallBack;
            m_popup.EffectClose();
        }
    }
    public void CallBack()
    {
        ScreenManager.Instance.HideCurrentPopup();
        PlayerPrefs.SetString("letterCards", "");
    }
}
