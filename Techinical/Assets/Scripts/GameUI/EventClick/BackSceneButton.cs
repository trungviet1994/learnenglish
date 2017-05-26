using UnityEngine;
using System.Collections;

public class BackSceneButton : BaseClickButton {
    [SerializeField]
    private UICategory m_category;
    public bool onScreenHandler = true;
    public override void OnClicked()
    {
        if (onScreenHandler)
        {
            if (ScreenManager.Instance.CurrentScreen == eScreenType.GAME_PLAY)
            {
                if (m_category)
                {
                    m_category.SetUpCategory();
                }
                //GameController.Instance.ResetUI();
                //LetterSpawn.Instance.ResetToStart();
            }
            ScreenManager.Instance.ShowScreenPrev();
        }
        else    // close screen popup ...
        {
            ScreenManager.Instance.m_generalScreen.Close();
            BaseEffectScreen m_effectScreen = GameObject.FindObjectOfType<BaseEffectScreen>();
            if (m_effectScreen)
            {
                m_effectScreen.m_myDelegate = CallBackExecutive;
                m_effectScreen.CloseWindow();
            }
            if(ScreenManager.Instance.CurrentScreen == eScreenType.GAME_PLAY)
            {
                GameController.Instance.UnPauseHandle();
            }
        }
        base.OnClicked();
    }

    public void CallBackExecutive()
    {
        ScreenManager.Instance.HideCurrentPopup();
    }
}
