using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : BaseClickButton
{
    public string BundleId;
    public Text m_txtStatus;
    Color colorText;
    float m_timeShow = 1.25f;
    void Awake()
    {
        m_txtStatus.gameObject.SetActive(false);
        colorText = m_txtStatus.color;
        colorText.a = 0;
    }
    public override void OnClicked()
    {
        base.OnClicked();
        if(UpdateManager.haveNewVersion)
        {
            ScreenManager.Instance.HideCurrentPopup();
#if UNITY_ANDROID
            Application.OpenURL("market://details?id=" + BundleId);
#elif UNITY_IPHONE
                    Application.OpenURL("itms-apps://itunes.apple.com/app/id"+BundleId);
#elif UNITY_EDITOR
                    Application.OpenURL("https://play.google.com/store/apps/details?id=com.armplay.wordsforkids");
#endif
        }
        else
        {
            ShowStatus();
        }
    }

    private void ShowStatus()
    {
        m_txtStatus.gameObject.SetActive(true);
        m_txtStatus.DOKill();
        m_txtStatus.color = colorText;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(DOTween.ToAlpha(() => m_txtStatus.color, x => m_txtStatus.color = x, 1, m_timeShow));
        mySequence.InsertCallback(m_timeShow + 0.5f, HideStatus);
    }
    private void HideStatus()
    {
        DOTween.ToAlpha(() => m_txtStatus.color, x => m_txtStatus.color = x, 0, m_timeShow);
    }
    private void DeactiveStatus()
    {
        m_txtStatus.gameObject.SetActive(false);
    }
}
