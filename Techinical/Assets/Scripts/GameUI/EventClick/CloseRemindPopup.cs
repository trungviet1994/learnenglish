using UnityEngine;
using System.Collections;

public class CloseRemindPopup : BaseClickButton {

    public override void OnClicked()
    {
        GameController.Instance.StartTimeRemind();
        GameController.Instance.StartAutoShowLetter();
        ScreenManager.Instance.HideCurrentPopup();
        base.OnClicked();
    }
}
