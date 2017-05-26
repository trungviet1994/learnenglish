using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Help : MonoSingleton<Help> {
    private int m_letterFailStep = 0;

    public int LetterFailCount
    {
        get { return m_letterFailStep; }
        set { m_letterFailStep = value; }
    }
    public void CheckLetterForHelp(bool _isGood = true)
    {
        if(_isGood)
        {
            return;
        }
        else
        {
            m_letterFailStep++;
            switch(m_letterFailStep)
            {
                case 3: // speak word for help
                    SpeakHelp();
                    break;
                case 6: // show letter 
                    ShowHelp();
                    break;
                case 9:
                    StartCoroutine(ShowHelpFinal(LetterGoodSpawner.Instance.ArrayLetterPrefabs));
                    break;
                default:
                    break;
            }
        }
    }

    // called invoke
    public void SpeakHelp()
    {
        if (GamePlayConfig.Instance.ModeLevel == eModeLevel.LEARN_LETTER)
        {
            AudioManager.Instance.PlayAudioByLetter(GameController.Instance.m_keyWord.StrKeyWord);
        }
        else
        {
            AudioManager.Instance.PlayAudioByName(GameController.Instance.m_keyWord.StrKeyWord);
        }
    }
    // called invoke
    public void ShowHelp()
    {
        // create prefabs & replace '*'
        if(GamePlayConfig.Instance.ModeLevel == eModeLevel.LEARN_LETTER)
        {
            // doc tu do len
            AudioManager.Instance.PlayAudioByLetter(GameController.Instance.m_keyWord.StrKeyWord);
        }
        else
        {
            AudioManager.Instance.PlayAudioByTypeName(eAudioName.HELP_SOUND);
            AudioManager.Instance.PlayAudioByName(GameController.Instance.m_keyWord.StrKeyWord);
            GameController.Instance.ReplaceKeyHideWithStepTwo();
        }
    }
    
    public IEnumerator ShowHelpFinal(GameObject[] _arrayLetterGood)
    {
        List<int> listKeyHide = GameController.Instance.m_keyWord.GetAllLetterHide();

        if (GamePlayConfig.Instance.ModeLevel == eModeLevel.LEARN_LETTER)
        {
            AudioManager.Instance.PlayAudioByLetter(GameController.Instance.m_keyWord.StrKeyWord);
        }
        else
        {
            AudioManager.Instance.PlayAudioByTypeName(eAudioName.HELP_SOUND);
        }
        // create prefabs & replace '*' show key word
        GameController.Instance.ReplaceAllLetter();
        //play audio word
        GameController.Instance.PlayAudioCurrentWord();
        LetterGoodSpawner.Instance.ScaleAllLetterGoodWithLoop();
        yield return new WaitForSeconds(1.5f);
        // an 1 tu trong list tu con an
        if(listKeyHide.Count > 0)
        {
            listKeyHide = Shuffle(listKeyHide);
        }
        // gamecontroller hide new key hide
        GameController.Instance.m_keyWord.HideLetterByIndex(listKeyHide[0]);
        // hide one letter 
        _arrayLetterGood[listKeyHide[0]].GetComponent<MyLetter>().Reset();
        yield break;
    }

    // xao trộn các phần tử trong mảng
    public List<int> Shuffle(List<int> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            int r = Random.Range(0, _list.Count);
            int pos = _list[r];
            _list[r] = _list[i];
            _list[i] = pos;
        }
        return _list;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
