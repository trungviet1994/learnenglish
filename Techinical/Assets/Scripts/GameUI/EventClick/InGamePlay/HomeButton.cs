using UnityEngine;
using System.Collections;

public class HomeButton : BaseOptionClick {
    public override void OnClicked()
    {
        base.OnClicked();
    }
    public override void CallBackClose()
    {
        //GameController.Instance.ResetUI();
        //LetterSpawn.Instance.ResetToStart();
        ScreenManager.Instance.HideCurrentPopup();
        ScreenManager.Instance.ShowScreenByType(eScreenType.USER_MODE);
    }
}
