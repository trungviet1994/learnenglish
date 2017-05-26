using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class KeyWord  {
    //private string m_strKeyWordHiden = "";
    //public string StrKeyWordHiden
    //{
    //    get { return m_strKeyWordHiden; }
    //    set { m_strKeyWordHiden = value; }
    //}

    

    private int m_indexSpaceInWord = -1;
    public int IndexSpaceInWord
    {
        get { return m_indexSpaceInWord; }
        set { m_indexSpaceInWord = value; }
    }

    #region AVAIRABLE

    private string m_strKeyWord = "";
    public string StrKeyWord
    {
        get { return m_strKeyWord; }
        set
        {
            m_strKeyWord = value;
            m_sbKeyWord = new StringBuilder(m_strKeyWord);
            m_sbKeyWordHiden = HidenKeyWord(m_sbKeyWord);
        }
    }
    public StringBuilder m_sbKeyWord;
    public StringBuilder m_sbKeyWordHiden;

    #endregion
    public KeyWord() {
        m_sbKeyWord = new StringBuilder();
        m_sbKeyWordHiden = new StringBuilder();
        StrKeyWord = "";
    }

    //private StringBuilder GetAllLettersInKeyWord(string _keyWord)
    //{
    //    char[] arrLetters = _keyWord.ToCharArray();
    //    StringBuilder _keyWords = new StringBuilder();
    //    for (int i = 0; i < arrLetters.Length; i++)
    //    {
    //        if (!_keyWord[i].ToString().Equals(GameConfig.SPLIT_LETTER))
    //        {
    //            _keyWords.Append(arrLetters[i]);
    //        }
    //        else
    //        {
    //            m_indexSpaceInWord = i;
    //        }
    //    }
    //    return _keyWords;
    //}
    
    private StringBuilder HidenKeyWord(StringBuilder _sbKeyWord)
    {
        StringBuilder sbKeyHide = new StringBuilder();
        for(int i=0;i< _sbKeyWord.Length;i++)
        {
            sbKeyHide.Insert(i,"*");
        }
        return sbKeyHide;
    }

    public bool CheckFinish()
    {
        bool result = m_sbKeyWord.ToString().Equals(m_sbKeyWordHiden.ToString());
        return result;
    }

    // lay vi tri space trong key word
    // tra ve: vi tri space trong list tu
    public int GetSpaceInKeyWord(StringBuilder _keyWord)
    {
        int _index = -1;
        for (int i = 0; i < _keyWord.Length; i++)
        {
            if (_keyWord[i].ToString().Equals(GameConfig.SPLIT_LETTER))
            {
                return _index = i;
            }
        }
        return _index;
    }

    public string GetLetterInKeywordByIndex(int _index)
    {
        return m_strKeyWord[_index].ToString();
    }
    /// <summary>
    /// lay vi tri cac tu cua keyword
    /// </summary>
    /// <param name="_letter"></param>
    /// <returns></returns>
    public List<int> GetIndexLetterInWord(string _letter)
    {
        List<int> m_listIndexOfLetter = new List<int>();
        for (int i = 0; i < m_strKeyWord.Length; i++)
        {
            if (m_strKeyWord[i].ToString().Equals(_letter))
            {
                m_listIndexOfLetter.Add(i);
            }
        }
        return m_listIndexOfLetter;
    }

    public bool CheckLetterTracked(string _letter)
    {
        return m_sbKeyWordHiden.ToString().Contains(_letter);
    }

    public bool CheckLetterIsGood(string _letter)
    {
        bool result = m_strKeyWord.Contains(_letter);
        return result;
    }

    // get all letter not tracked
    public List<int> GetKeyWordHideForHelp()
    {
        List<int> _listKeyHide = GetAllLetterHide();
        int range = (_listKeyHide.Count / 2);
        _listKeyHide = Shuffle(_listKeyHide);
        if (range > 0)
        {
            _listKeyHide = _listKeyHide.GetRange(0, range);
        }else
        {
            _listKeyHide.Clear();
        }
        return _listKeyHide;
    }

    // get all index letter hide start 
    public List<int> GetAllLetterHide()
    {
        List<int> listKeyHide = new List<int>();
        for (int i = 0; i < m_sbKeyWordHiden.Length; i++)
        {
            if (m_sbKeyWordHiden[i].ToString().Equals("*"))
            {
                listKeyHide.Add(i);
            }
        }
        return listKeyHide;
    }

    public List<int> Shuffle(List<int> _listKey)
    {
        for (int i = 0; i < _listKey.Count; i++)
        {
            int r = Random.Range(0, _listKey.Count);
            int pos = _listKey[r];
            _listKey[r] = _listKey[i];
            _listKey[i] = pos;
        }
        return _listKey;
    }
    
    public void ReplaceKeyHide(int _index)
    {
        m_sbKeyWordHiden.Replace(m_sbKeyWordHiden[_index], m_sbKeyWord[_index], _index, 1);
    }
    public void HideLetterByIndex(int _index)
    {
        m_sbKeyWordHiden.Replace(m_sbKeyWord[_index], '*', _index, 1);
    }
    ~KeyWord()
    {

    }
}
