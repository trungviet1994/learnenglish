using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILetter : MonoBehaviour {
    public Text m_txt_Letter;
    private string m_myLetter;
    [SerializeField]
    protected Image m_myImage;
    [SerializeField]
    protected Color m_NoneColor;
    [SerializeField]
    private Color m_colorLetterGood;
    //public bool isActive = false;
    private eBaseTeamType m_team;
    public eBaseTeamType Team
    {
        get { return m_team; }
    }
    public string MyLetter
    {
        get { return m_myLetter; }
        set { 
            m_myLetter = value;
            if (m_txt_Letter)
            {
                m_txt_Letter.text = m_myLetter;
            }
        }
    }
    //void OnEnable()
    //{
    //    Reset();
    //}

    public virtual void Reset()
    {
        MyLetter = "";
        m_myImage.color = m_NoneColor;
        //isActive = false;
    }
    
    private void SetUpColorWithLose(bool _isLetterFail)
    {
        if (_isLetterFail)
        {
            m_myImage.color = m_NoneColor;
        }
        else
        {
            m_myImage.color = m_colorLetterGood;
        }
    }
    public void SetUpStart(string _letter, eBaseTeamType _team, bool _isLetterFail = true)
    {
        this.transform.localScale = Vector3.one;
        GetComponent<RectTransform>().sizeDelta = LetterGoodSpawner.Instance.SizeOfGrid;
        MyLetter = _letter.ToUpper();
        m_team = _team;
        //isActive = true;
        switch(_team)
        {
            case eBaseTeamType.TEAM_BLUE:
                m_myImage.color = Color.green;
                break;
            case eBaseTeamType.TEAM_RED:
                m_myImage.color = Color.red;
                break;
            case eBaseTeamType.NONE: // set in single play mode
                SetUpColorWithLose(_isLetterFail);
                break;
        }
    }
}
