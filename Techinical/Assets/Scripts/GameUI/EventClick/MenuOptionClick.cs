using UnityEngine;
using System.Collections;

public class MenuOptionClick : BaseClickButton {
    public override void OnClicked()
    {
        UpdateManager.Instance.CheckForUpdate();
        ScreenManager.Instance.ShowPopupScreen(ePopupType.SETTING);
        base.OnClicked();
    }
}
