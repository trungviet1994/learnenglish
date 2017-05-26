using UnityEngine;
using System.Collections;

public class MenuAboutClick : BaseClickButton
{
    public override void OnClicked()
    {
        ScreenManager.Instance.ShowPopupScreen(ePopupType.ABOUT);
        base.OnClicked();
    }
}
