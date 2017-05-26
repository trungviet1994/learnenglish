using UnityEngine;
using System.Collections;

public class BaseOptionClick : BaseClickButton {
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

    public virtual void CallBackClose()
    {

    }
}
