using UnityEngine;
using System.Collections;

public class DownloadButton : BaseClickButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        PlayerPrefs.SetString("letterCards", "");
        Application.OpenURL("http://armplay.com");
        ScreenManager.Instance.HideCurrentPopup();
    }
}