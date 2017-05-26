using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class CategoryButton : BaseClickButton, IPointerDownHandler, IPointerUpHandler
{
    public UICategoryTile m_categoryTile;
    private BaseEffectScreen m_effectScreen;
    void OnEnable()
    {
        m_effectScreen = GameObject.FindObjectOfType<BaseEffectScreen>();
    }
    public override void OnClicked()
    {
        if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            if (m_effectScreen)
            {
                m_effectScreen.m_myDelegate = CallBackExecutive;
                m_effectScreen.CloseWindow();
                ScreenManager.Instance.m_generalScreen.Close();
            }
        }
        else
        {
            m_categoryTile.ChooseCategory();
        }
        base.OnClicked();
    }

    public void CallBackExecutive()
    {
        m_categoryTile.ChooseCategory();
        ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
        GameController.Instance.StartGame();
        base.OnClicked();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.transform.localScale = Vector3.one * 0.925f;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.parent.transform.localScale = Vector3.one;
    }
}
