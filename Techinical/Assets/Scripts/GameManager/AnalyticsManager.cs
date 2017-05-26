using UnityEngine;
using System.Collections;

public class AnalyticsManager : MonoSingleton<AnalyticsManager> {
    public GoogleAnalyticsV3 m_myAnalytics;

    public void ShowLogScreenMenu()
    {
        m_myAnalytics.LogScreen("Menu Game");
    }
    public void ShowLogScreenCategory()
    {
        m_myAnalytics.LogScreen("Category");
    }
    public void ShowLogScreenGameplay()
    {
        m_myAnalytics.LogScreen("Gameplay");
    }
    public void ShowLogScreenSinglePlay()
    {
        m_myAnalytics.LogScreen("Single Play");
    }
    public void ShowLogScreenMultiPlay()
    {
        m_myAnalytics.LogScreen("Multi Play");
    }

    public void ShowLogScreenByType(eScreenType _screenType)
    {
        switch (_screenType)
        {
            case eScreenType.USER_MODE:
                ShowLogScreenMenu();
                break;
            case eScreenType.CATEGORY:
                ShowLogScreenCategory();
                if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
                {
                    ShowLogScreenSinglePlay();
                }
                else
                {
                    ShowLogScreenMultiPlay();
                }
                break;
            case eScreenType.GAME_PLAY:
                ShowLogScreenGameplay();
                break;
        }
    }
    public void ShowLogScoreOfSingle(int _score)
    {
        m_myAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("HighScore")
            .SetEventAction(GamePlayConfig.Instance.ModeLevel.ToString())
            .SetEventLabel(DataManager.instance.m_choosenCategory.m_category)
            .SetEventValue(_score)
            );

    }
}
