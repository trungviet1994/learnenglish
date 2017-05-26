using UnityEngine;
using System.Collections;

public class SaveButton : BaseClickButton
{
    //public UITabset tab;
    public override void OnClicked()
    {
        //tab.CloseSetting();
        //ScreenManager.Instance.ShowPopupScreen(ePopupType.OPTION);
        if (ScreenManager.Instance.CurrentScreen == eScreenType.USER_MODE)
        {
            ScreenManager.Instance.HideCurrentPopup();
        }
        else
        {
            ScreenManager.Instance.ShowPopupScreen(ePopupType.OPTION);
        }
        base.OnClicked();
    }
}
