using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using Vuforia;

public class GameController : MonoSingleton<GameController>
{
    private const float m_timeDelayEnableImageTarget = 1.25f;

    public ScoreManager m_scoreManager;
    public KeyWord m_keyWord;
    //public Help m_help;
    public PopupCategory m_popUpCategory;
    public UICategory m_categoryManager;

    private bool isShowPopupContinue = false;
    // so cau hoi trong 1 package
    private int m_QuestionCountInPackage = 0;
    // so cau tra loi dung tren 1 package
    private int m_QuestionGoodAnswer = 1;
    public int QuestionCountInPackage
    {
        get { return m_QuestionCountInPackage; }
        set { m_QuestionCountInPackage = value;
            QuestionGoodAnswer = 1;
        }
    }

    public int QuestionGoodAnswer
    {
        get { return m_QuestionGoodAnswer; }
        set { m_QuestionGoodAnswer = value;
            //if (m_txtQuestionPerPackage)
            //{
            //    m_txtQuestionPerPackage.text = m_QuestionGoodAnswer.ToString() + "/" + m_QuestionCountInPackage.ToString();
            //}
        }
    }
    //public Text m_txtQuestionPerPackage;
    public bool m_allowTracked = false;
    // Use this for initialization
    void Start()
    {
        InitiateAll();
    }

    [ContextMenu("test")]
    public void Test()
    {
        string a = "hello";
        //string b= a.Replace(a[0].ToString(), "m");
        //Debug.Log(b);
        bool result = a.Contains("r");
        //Debug.Log("test:" + a.Contains("r"));
    }
    // init all in gameplay
    public void InitiateAll()
    {
        LetterSpawn.Instance.Initiate();
        LetterGoodSpawner.Instance.Initiate();
        m_keyWord = new KeyWord();
        m_scoreManager.Initiate();
        //m_help.Initiate();
        DisableTracking();
        //ScreenManager.Instance.ShowScreenByType(eScreenType.USER_MODE);
        SetMode(false);
    }

    // Bat dau choi game voi 1 package
    public void StartGame()
    {
        // get list question from data
        QuestionCountInPackage = QuestionManager.Instance.GetListQuestion();
        m_scoreManager.ResetAllScore();//reset all score
        m_scoreManager.SetUpUserMode();
        QuestionManager.Instance.SetUpWithModeLevel();// set up mode when have key word
        PlayWithNewWord();//create new words/question
        isShowPopupContinue = false;
        //LetterSpawn.Instance.ScaleAllToZero();
        LetterSpawn.Instance.SetupAllLetterToZero();
    }

    // create new words/ question
    // parameter : keyWord
    public void PlayWithNewWord(bool isPlayNewWord = true)
    {
        string m_strKeyWord = "no question";
        Help.Instance.LetterFailCount = 0;
        GamePlayConfig.Instance.GameStart = true;

        if (isPlayNewWord)
        {
            m_strKeyWord = QuestionManager.Instance.GetQuestion();
        }
        else
        {
            m_strKeyWord = QuestionManager.Instance.StrCurrentQuestion;
        }
       
        m_keyWord.StrKeyWord = m_strKeyWord;
        QuestionManager.Instance.ShowOfQuestion();
        //create prefabs letter good in gameplay
        LetterSpawn.Instance.CreateLetterGoodFromKeyWord(m_keyWord.IndexSpaceInWord, m_keyWord.StrKeyWord);
        StartEffectInGamePlay();
        StartTimeRemind();
        StartAutoShowLetter();
    }

    private void StartEffectInGamePlay()
    {
        Debug.Log("StartEffectInGamePlay");
        LetterSpawn.Instance.StartScaleArrayLetter();
        if(m_scoreManager)
        {
            m_scoreManager.DoMoveScore();
        }
        QuestionManager.Instance.DoShowMainImage();
        //m_scoreManager.OpenQperP();
    }

    public void EnableTracking()
    {
        m_allowTracked = true;
        //VuforiaManager.Instance.StartCameraAR();
    }

    private void DisableTracking()
    {
        m_allowTracked = false;
    }

    public void DoNextQuestion()
    {
        QuestionManager.Instance.NextQuestion();
        if(QuestionManager.Instance.CheckFinisCategory())
        {
            FinishPackageHandle();
            StopAutoShowLetter();
            return;
        }
        StartAutoShowLetter();
        QuestionGoodAnswer++;
        DoHideMainImage();// Effect change question 
        m_scoreManager.DoCloseScore();
    }

    public void DoHideMainImage()
    {
        //m_scoreManager.CloseQperP();
        QuestionManager.Instance.DoHideMainImage();
        LetterSpawn.Instance.ScaleAllToZero();
    }

    public bool CheckWordIsFinish()
    {
        return m_keyWord.CheckFinish();
    }
    

    public void OnFinishWord(bool _isWin ,bool _isAuto = false)
    {
        StartCoroutine(DoFinishWord(_isWin,_isAuto));
    }

    // Do something when finish words with coroutine
    private IEnumerator DoFinishWord(bool _isWin,bool _isAuto)
    {
        // disable all imagetarget
        DisableTracking();
        GamePlayConfig.Instance.GameStart = false;
        if (GamePlayConfig.Instance.TypeShowQuestion == QuestionType.QS_SOUND)
        {
            QuestionManager.Instance.SetStopSpeakQuestion();
        }
        QuestionManager.Instance.RemoveCurrentQuestion();
        QuestionGoodAnswer += 1;
        //Debug.Log(Time.realtimeSinceStartup);
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(Time.realtimeSinceStartup);
        //show key word hide,neu con
        ReplaceAllLetter();
        // effect with letter good
        LetterGoodSpawner.Instance.StartEffectFinishWord(_isAuto);
        // play audio one time
        if (_isWin)
        {
            AudioManager.Instance.PlayOneShotByTypeName(eAudioName.FINISH_WORD);
        }
        else
        {
            AudioManager.Instance.PlayOneShotByTypeName(eAudioName.FAIL_ANSWER);
        }
    }

    // call when finish effect done word
   // check finish package 
    public void CreateNewQuestion()
    {
        if (!isShowPopupContinue && m_scoreManager.CheckFinishCategory())
        {
            ScreenManager.Instance.ShowPopupScreen(ePopupType.CATE_POPUP);
            m_popUpCategory.SetUpShow(false, m_scoreManager.m_singleScore.ScoreOfUser); // show pop continue
            isShowPopupContinue = true;
            return;
        }
        if (QuestionManager.Instance.CheckFinisCategory())
        {
            // finish one package
            FinishPackageHandle();
            return;
        }
        PlayWithNewWord(); // chơi với từ mới
    }

    private void FinishPackageHandle()
    {
        ScreenManager.Instance.ShowPopupScreen(ePopupType.CATE_POPUP);
        if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            m_popUpCategory.SetUpShow(true, m_scoreManager.m_singleScore.ScoreOfUser);
        }
        //else
        //{
        //    if (m_scoreManager.m_multiScore.ScoreOfRed >= m_scoreManager.m_multiScore.ScoreOfBlue)
        //    {
        //        m_popUpCategory.SetUpShow(true, m_scoreManager.m_multiScore.ScoreOfRed);
        //        //m_popUpCategory.SetUpShow(m_scoreManager.MultiScore.ScoreOfRed, m_scoreManager.MultiScore.ScoreOfBlue);
        //    }
        //    else
        //    {
        //        m_popUpCategory.SetUpShow(true, m_scoreManager.m_multiScore.ScoreOfBlue);
        //    }
        //}
    }

    #region REMIND_SCREEN
    public GameObject m_remindManager;
    public bool m_stopRemindForever = false;
    private const float m_timeRateShowRemind = 60.0f;
    private const float m_timeCallBackHideRemind = 30.0f;
    // show remindscreen for user tracking
    public void StartTimeRemind()
    {
        if (m_stopRemindForever)
        {
            return;
        }
        CancelInvoke("HideAfterTime");
        StopTimeRemind();
        InvokeRepeating("ShowRemind", m_timeRateShowRemind, m_timeRateShowRemind);
    }
    public void StopTimeRemind()
    {
        CancelInvoke("ShowRemind");
    }
    private void ShowRemind()
    {
        StopAutoShowLetter();
        ScreenManager.Instance.ShowPopupScreen(ePopupType.REMIND);
        Invoke("HideAfterTime", m_timeCallBackHideRemind);
    }
    public void HideAfterTime()
    {
        if(ScreenManager.Instance.CurrentPopup == ePopupType.REMIND)
        {
            ScreenManager.Instance.HideCurrentPopup();
            StopTimeRemind();
        }
        StartAutoShowLetter();
        StartTimeRemind(); // reset time show
    }

    public void DoStopRemind()
    {
        if (!m_stopRemindForever)
        {
            m_stopRemindForever = true;
            StopTimeRemind();
            if (ScreenManager.Instance.CurrentPopup == ePopupType.REMIND)
            {
                ScreenManager.Instance.HideCurrentPopup();
            }
            Destroy(m_remindManager);
        }
    }
    #endregion..........................................

    #region AUTO_PLAY
    private const float m_timeRateOfMode = 30.0f;
    //private int m_indexOfLetterInWord=0;
    // show letter with auto after 30s
    public void StartAutoShowLetter()
    {
        StopAutoShowLetter();
        InvokeRepeating("ShowOneLetter", m_timeRateOfMode, m_timeRateOfMode);
    }

    private void ShowOneLetter()
    {
        List<int> listKeyHide = m_keyWord.GetAllLetterHide();
 
        if (listKeyHide.Count <= 0)
        {
            StopAutoShowLetter();
            return;
        }
        int index = listKeyHide[0];
        m_keyWord.ReplaceKeyHide(index);
        m_scoreManager.DecreaseScoreOfWord();
        LetterGoodSpawner.Instance.EffectAutoShow(index, m_keyWord.GetLetterInKeywordByIndex(index));
        AudioManager.Instance.PlayAudioByTypeName(eAudioName.AUTO_OPEN_LETTER);
    }
    public void StopAutoShowLetter()
    {
        CancelInvoke("ShowOneLetter");
    }
    #endregion..................................................

    //kiem tra key word co nho hon 11 ki tu
    private bool CheckKeyWord(StringBuilder _keyWord)
    {
        if (_keyWord.Length <= GameConfig.MAX_LENGHT_WORD)
        {
            return true;
        }
        return false;
    }

    private void ReplaceAllLetterByListIndex(List<int> _listIndexKeyHide)
    {
        for (int i = 0; i < _listIndexKeyHide.Count; i++)
        {
            m_keyWord.ReplaceKeyHide(_listIndexKeyHide[i]);
            LetterGoodSpawner.Instance.ArrayLetterPrefabs[_listIndexKeyHide[i]].GetComponent<MyLetter>().SetUpStart(m_keyWord.GetLetterInKeywordByIndex(_listIndexKeyHide[i]), eBaseTeamType.NONE);
        }
    }

    public void ReplaceKeyHideWithStepTwo()
    {
        List<int> newListKeyHide = GameController.Instance.m_keyWord.GetKeyWordHideForHelp();
        if (newListKeyHide.Count >= 1)
        {
            ReplaceAllLetterByListIndex(newListKeyHide);
            LetterGoodSpawner.Instance.EffectUpLetterGood(newListKeyHide);
        }
    }

    public void ReplaceAllLetter()
    {
        List<int> listKeyHide = GameController.Instance.m_keyWord.GetAllLetterHide();
        if(listKeyHide.Count > 0)
        {
            ReplaceAllLetterByListIndex(listKeyHide);
        }
    }

    public void PlayAudioCurrentWord()
    {
        if (GamePlayConfig.Instance.ModeLevel != eModeLevel.LEARN_LETTER)
        {
            AudioManager.Instance.PlayAudioByName(m_keyWord.StrKeyWord);
        }else
        {
            PlayAudioWordByLetter(m_keyWord.StrKeyWord);
        }
    }

    public void PlayAudioWordByLetter(string _letter)
    {
        string wordOfLetter = DataManager.instance.GetWordFromLetterHead(_letter);
        AudioManager.Instance.PlayAudioByName(wordOfLetter);
    }

    #region HANDLE_PAUSE_UNPAUSE_....
    public void PauseHandle()
    {
        DisableTracking();
        //PauseAllEffectInGamePlay();
        AudioManager.Instance.PauseAudio();
        if(GamePlayConfig.Instance.TypeShowQuestion == QuestionType.QS_SOUND)
        {
            QuestionManager.Instance.SetStopSpeakQuestion();
        }
        StopAutoShowLetter();
        StopTimeRemind();
    }
    public void UnPauseHandle()
    {
        Invoke("EnableTracking",m_timeDelayEnableImageTarget);
        //PlayAllEffectInGamePlay();
        AudioManager.Instance.UnPauseAudio();
        if (GamePlayConfig.Instance.TypeShowQuestion == QuestionType.QS_SOUND)
        {
            QuestionManager.Instance.SetRespeakQuestion();
        }
        StartTimeRemind();
        StartAutoShowLetter();
    }

    public void ReplayHandle()
    {
        KillAllEffectInGamePlay();
        //m_scoreManager.CloseQperP();
        LetterSpawn.Instance.ScaleAllToZero();
        PlayWithNewWord(false);
    }

    private void PauseAllEffectInGamePlay()
    {
        DOTween.PauseAll();
    }
    private void PlayAllEffectInGamePlay()
    {
        DOTween.PlayAll();
    }

    private void KillAllEffectInGamePlay()
    {
        DOTween.KillAll();
    }
    #endregion

    #region MANAGER_MODE
    //public GameObject m_camera;
    //public GameObject m_imagetargetOfAnimal;

    public List<GameObject> m_listAnimalTracked = new List<GameObject>();
    //public void TEST()
    //{
    //    SetMode(true);
    //}
    public void RemoveAnimal(GameObject _animal)
    {
        if(_animal && m_listAnimalTracked.Contains(_animal))
        {
            m_listAnimalTracked.Remove(_animal);
        }
        if(m_listAnimalTracked.Count == 0)
        {
            //ContentManager.Instance.m_txtAnimalName.text = "";
        }
    }
    public void SetMode(bool _isAnimalPlay = true)
    {
        if (_isAnimalPlay)
        {
            //m_camera.SetActive(true);
            Screen.orientation = ScreenOrientation.Landscape;
            ScreenManager.Instance.ShowScreenByType(eScreenType.ANIMAL_SCREEN);
            GamePlayConfig.Instance.m_modePlay = ModePlay.PLAY_ANIMAL;
            //BlueImageTargetManager.instance.EnableAllImageTarget();
            //RedImageTargetManager.instance.DisableAllImageTarget();
            //m_imagetargetOfAnimal.SetActive(true);
        }
        else
        {
            //m_camera.SetActive(false);
            Screen.orientation = ScreenOrientation.Portrait;
            GamePlayConfig.Instance.m_modePlay = ModePlay.PLAY_GAME;
            ScreenManager.Instance.ShowScreenByType(eScreenType.USER_MODE);
            if(m_listAnimalTracked.Count >0)
            {
                for(int i=0;i<m_listAnimalTracked.Count;i++)
                {
                    m_listAnimalTracked[i].SetActive(false);
                }
            }
            m_listAnimalTracked.Clear();
        }
        AudioManager.Instance.PlayAudioBackgroundWithPlayGame(GamePlayConfig.Instance.m_modePlay);
    }

    public void ZoomInAnimalTraced()
    {
        if(m_listAnimalTracked.Count>0)
        {
            for (int i = 0; i < m_listAnimalTracked.Count; i++)
            {
                //m_listAnimalTracked[i].GetComponentInChildren<AnimalObject>().ZoomIn();
            }
        }
    }

    public void ZoomOutAnimalTraced()
    {
        if (m_listAnimalTracked.Count > 0)
        {
            for (int i = 0; i < m_listAnimalTracked.Count; i++)
            {
                //m_listAnimalTracked[i].GetComponentInChildren<AnimalObject>().ZoomOut();
            }
        }
    }
    #endregion
}
