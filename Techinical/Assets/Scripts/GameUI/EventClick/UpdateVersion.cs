using UnityEngine;
using System.Collections;

public class UpdateVersion : BaseClickButton {
    private string BundleId;
    public BaseEffectPopupTop m_popup;
    public override void OnClicked()
    {
        if (UpdateManager.haveNewVersion)
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
            m_popup.m_myDelegate = CallBack;
            m_popup.EffectClose();
        }
        base.OnClicked();
    }
    public void CallBack()
    {
        ScreenManager.Instance.HideCurrentPopup();
    }
}
