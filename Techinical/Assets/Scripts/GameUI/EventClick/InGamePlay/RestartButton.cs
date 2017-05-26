using UnityEngine;
using System.Collections;

public class RestartButton : BaseOptionClick {
    public override void OnClicked()
    {
        ////restart game in gameplay
        //GameController.Instance.ReplayHandle();
        //ScreenManager.Instance.HideCurrentPopup();
        base.OnClicked();
    }
    public override void CallBackClose()
    {
        //restart game in gameplay
        GameController.Instance.ReplayHandle();
        ScreenManager.Instance.HideCurrentPopup();
    }
}
