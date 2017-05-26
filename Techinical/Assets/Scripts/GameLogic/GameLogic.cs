using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameLogic : MonoSingleton<GameLogic>
{
    #region TEST_AVAIRABLE
    public string m_letterTest = "";
    [ContextMenu("test letter!")]
    public void Test()
    {
        CheckLetterTrackable(m_letterTest, eBaseTeamType.TEAM_BLUE);
    }

    [ContextMenu("test one word")]
    public void TestOneWord()
    {
        isFinishTest = false;
        StartCoroutine(TestWord(m_letterTest));
    }
    public void TestWordInput(string _input)
    {
        isFinishTest = false;
        StartCoroutine(TestWord(_input));
    }

    float m_timeWait = 0;
    public IEnumerator TestWord(string _word)
    {
        m_timeWait = _word.Length * 1.25f + 6;
        for (int i = 0; i < _word.Length; i++)
        {
            if (isFinishTest)
            {
                yield break;
            }
            CheckLetterTrackable(_word[i].ToString(), eBaseTeamType.TEAM_BLUE);
            yield return new WaitForSeconds(1.25f);
        }
        yield break;
    }
    bool isFinishTest = false;
    #endregion

    #region XU_LY_LOGIC_GAME
    //check letter when tracking found letter
    //return : true:gameend , fasle:next tracking
    public void CheckLetterTrackable(string _letter, eBaseTeamType _team)
    {
        if (GameController.Instance.m_keyWord.CheckLetterIsGood(_letter)) //> good answer
        {
            // create prefabs good when letter no exist
            LetterSpawn.Instance.DoWithLetterGood(_letter, _team);
        }
        else                                                        //> fail anwser
        {
            LetterSpawn.Instance.DoWithLetterFail(_letter, _team);
            if (GamePlayConfig.Instance.TypeShowQuestion == QuestionType.QS_SOUND)
            {
                QuestionManager.Instance.SetRespeakQuestion();
            }
        }
        GameController.Instance.DoStopRemind();//check stop remind
        GameController.Instance.StartAutoShowLetter();
    }

    #endregion
}
