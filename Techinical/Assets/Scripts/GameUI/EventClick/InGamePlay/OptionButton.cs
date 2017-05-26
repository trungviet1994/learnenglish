using UnityEngine;
using System.Collections;

public class OptionButton : BaseOptionClick
{
    public override void OnClicked()
    {
        base.OnClicked();
    }
    public override void CallBackClose()
    {
        ScreenManager.Instance.ShowPopupScreen(ePopupType.SETTING);
    }

}
