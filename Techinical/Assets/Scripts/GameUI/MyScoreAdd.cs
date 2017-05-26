using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyScoreAdd : UILetter {
    public void SetUpStart(string _letter, eBaseTeamType _team)
    {
        MyLetter = _letter;
        switch(_team)
        {
            case eBaseTeamType.NONE:
                m_myImage.color = m_NoneColor;
                break;
            case eBaseTeamType.TEAM_BLUE:
                m_myImage.color = Color.green;
                break;
            case eBaseTeamType.TEAM_RED:
                m_myImage.color = Color.red;
                break;
        }
    } 
}
