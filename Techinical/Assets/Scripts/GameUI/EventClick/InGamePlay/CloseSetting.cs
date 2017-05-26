using UnityEngine;
using System.Collections;

public class CloseSetting : BaseClickButton {
    public override void OnClicked()
    {
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
