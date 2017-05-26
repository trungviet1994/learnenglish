using UnityEngine;
using System.Collections;

public class CloseOption : BaseClickButton {
    public override void OnClicked()
    {
        GameController.Instance.UnPauseHandle();
        ScreenManager.Instance.HideCurrentPopup();
        base.OnClicked();
    }
}
