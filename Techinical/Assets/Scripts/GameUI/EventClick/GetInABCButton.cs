using UnityEngine;
using System.Collections;

public class GetInABCButton : BaseClickButton {
    public BaseEffectScreen m_effectScreen;
    public override void OnClicked()
    {
        if (m_effectScreen)
        {
            m_effectScreen.m_myDelegate = CallBackClose;
            m_effectScreen.CloseWindow();
        }
        base.OnClicked();
    }
    public void CallBackClose()
    {
        GamePlayConfig.Instance.ModeLevel = eModeLevel.LEARN_LETTER;
        ScreenManager.Instance.ShowScreenByType(eScreenType.GET_IN_ABC);
    }
}
