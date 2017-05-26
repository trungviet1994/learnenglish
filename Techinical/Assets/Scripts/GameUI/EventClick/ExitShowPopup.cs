using UnityEngine;
using System.Collections;

public class ExitShowPopup : BaseClickButton {
    public override void OnClicked()
    {
        ScreenManager.Instance.ShowPopupScreen(ePopupType.EXIT);
        base.OnClicked();
    }
}
