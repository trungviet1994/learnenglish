using UnityEngine;
using System.Collections;

public class MyLetter : UILetter {
    public void SetUpStart(string _letter, eBaseTeamType _team)
    {
        MyLetter = _letter.ToUpper();
        switch (_team)
        {
            case eBaseTeamType.TEAM_BLUE:
                m_myImage.color = Color.green;
                break;
            case eBaseTeamType.TEAM_RED:
                m_myImage.color = Color.red;
                break;
            case eBaseTeamType.NONE:
                m_myImage.color = m_NoneColor;
                break;
        }
    }

    public override void Reset()
    {
        m_myImage.color = m_NoneColor;
        MyLetter = "";
        //Show();
    }

    public void Hide()
    {
        Color color = m_myImage.color;
        color.a = 0;
        m_myImage.color = color;
    }
    public void Show()
    {
        Color color = m_myImage.color;
        color.a = 1;
        m_myImage.color = color;
    }
}
