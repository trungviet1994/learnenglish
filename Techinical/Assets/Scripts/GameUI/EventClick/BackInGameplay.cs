using UnityEngine;
using System.Collections;

public class BackInGameplay : BaseOptionClick {
    public override void OnClicked()
    {
        base.OnClicked();
    }
    // xu ly sau khi finish effect
    public override void CallBackClose()
    {
        ScreenManager.Instance.ShowScreenPrev();
        GameController.Instance.m_categoryManager.SetUpCategory();
    }
}
