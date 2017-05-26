using UnityEngine;
using System.Collections;

public class BaseClickLevel : BaseClickButton {
    [SerializeField]
    private int m_level = 1;
    [SerializeField]
    private eModeLevel m_modeLevel = eModeLevel.EASY;

    public UICategory m_categoryManager;
    public BaseEffectScreen m_effectScreen;

    public override void OnClicked()
    {
        m_effectScreen.m_myDelegate = CallBackExecutive;
        m_effectScreen.CloseWindow();
        base.OnClicked();
    }
    public void CallBackExecutive()
    {
        DataManager.instance.GetCategoriesInLevel(m_level);
        GamePlayConfig.Instance.TypeShowQuestion = QuestionType.QS_IMAGE;
        GamePlayConfig.Instance.ModeLevel = m_modeLevel;
        ScreenManager.Instance.ShowScreenByType(eScreenType.CATEGORY);

        if (m_categoryManager)
        {
            m_categoryManager.SetUpCategory();
        }
    }
}
