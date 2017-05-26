using UnityEngine;
using System.Collections;

public class StartMultiPlayButton : BaseClickButton {
    public BaseEffectScreen m_effectScreen;
    public override void OnClicked()
    {
        m_effectScreen.m_myDelegate = CallBackClose;
        m_effectScreen.CloseWindow();
        base.OnClicked();
    }
    public void CallBackClose()
    {
        DataManager.instance.GetListQuestionInMultiPlay();
        ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
        GameController.Instance.StartGame();
        //GamePlayConfig.Instance.GameStart = true;
    }
}
