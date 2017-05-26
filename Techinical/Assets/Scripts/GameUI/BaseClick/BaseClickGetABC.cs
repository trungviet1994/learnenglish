using UnityEngine;
using System.Collections;

public class BaseClickGetABC : BaseClickButton {
    public QuestionType m_typeShowQuestion;
    [SerializeField]
    private int m_level = 0;
    public BaseEffectScreen m_effectScreen;
    public override void OnClicked()
    {
        //if (DataManager.instance)
        //{
        //    DataManager.instance.GetCategoriesInLevel(m_level);
        //    DataManager.instance.GetAllLetterQuestion();
        //}
      
        //GamePlayConfig.Instance.TypeShowQuestion = m_typeShowQuestion;
        //ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
        //GameController.Instance.StartGame();
        ScreenManager.Instance.m_generalScreen.Close();
        m_effectScreen.m_myDelegate = CallBackExecutive;
        m_effectScreen.CloseWindow();
        base.OnClicked();
    }
    public void CallBackExecutive()
    {
        if (DataManager.instance)
        {
            DataManager.instance.GetCategoriesInLevel(m_level);
            DataManager.instance.GetAllLetterQuestion();
        }
        GamePlayConfig.Instance.TypeShowQuestion = m_typeShowQuestion;
        ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
        GameController.Instance.StartGame();
    }
}
