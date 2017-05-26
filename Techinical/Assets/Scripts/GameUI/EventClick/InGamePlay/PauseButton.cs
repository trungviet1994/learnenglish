using UnityEngine;
using System.Collections;

public class PauseButton : BaseClickButton {
    public override void OnClicked()
    {
        if (GamePlayConfig.Instance.GameStart)
        {
            GameController.Instance.PauseHandle();
            ScreenManager.Instance.ShowPopupScreen(ePopupType.OPTION);
        }
        base.OnClicked();
    }
}
