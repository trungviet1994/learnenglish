using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class ScoreOfSingle
{
    public int m_scoreOfWord = 0;
    private int m_scoreOfUser = 0;
    public Text m_txtScoreOfUser;

    public int ScoreOfUser
    {
        get { return m_scoreOfUser; }
        set
        {
            m_scoreOfUser = value;
            if (m_txtScoreOfUser)
            {
                m_txtScoreOfUser.text = m_scoreOfUser.ToString();
            }
        }
    }

    public ScoreOfSingle()
    {
        this.m_scoreOfWord = 0;
        ScoreOfUser = 0;
    }

    public void SetScoreOfUser()
    {
        ScoreOfUser += (10 - m_scoreOfWord);
    }
    
    public void ResetAllScore()
    {
        this.m_scoreOfUser = 0;
        this.m_scoreOfWord = 0;
    }
    private void SaveAndResetScore()
    {
        if (DataManager.instance)
        {
            DataManager.instance.SetScoreInCategory(this.m_scoreOfUser);
        }
        ScoreOfUser = 0;
        m_scoreOfWord = 0;
    }
}
[System.Serializable]
public class ScoreOfMulti
{
    private int m_scoreOfBlue = 0;
    private int m_scoreOfRed = 0;

    public int ScoreOfBlue
    {
        get { return m_scoreOfBlue; }
        set
        {
            m_scoreOfBlue = value;
            if (m_txtScoreBlue)
            {
                m_txtScoreBlue.text = m_scoreOfBlue.ToString();
            }
        }
    }
    public int ScoreOfRed
    {
        get { return m_scoreOfRed; }
        set
        {
            m_scoreOfRed = value;
            if (m_txtScoreRed)
            {
                m_txtScoreRed.text = m_scoreOfRed.ToString();
            }
        }
    }
    public Text m_txtScoreBlue;
    public Text m_txtScoreRed;
    public ScoreOfMulti()
    {
        this.ScoreOfBlue = 0;
        this.ScoreOfRed = 0;
    }

    public void ResetAllScore()
    {
        this.ScoreOfRed = 0;
        this.ScoreOfBlue = 0;
    }
}

public class ScoreManager : MonoBehaviour
{
    private const int m_maxScoreOfWord = 10;

    private GameObject[] m_arrayScoreOfWord = new GameObject[m_maxScoreOfWord];
    public ScoreOfSingle m_singleScore = new ScoreOfSingle();
    //public ScoreOfMulti m_multiScore = new ScoreOfMulti();

    //public Transform m_containScoreOfWord;
    public GameObject m_objScoreOfSingle;
    //public GameObject m_objScoreOfMulti;
    
    //public ScoreOfSingle SingleScore
    //{
    //    get { return m_singleScore; }
    //    set { m_singleScore = value; }
    //}
   
    //public ScoreOfMulti MultiScore
    //{
    //    get { return m_multiScore; }
    //    set { m_multiScore = value; }
    //}

    //public VerticalLayoutGroup m_vertcalLayoutScoreOfWord;
    public void Initiate()
    {
        GetAllChildScoreWord();

        m_distanceMove = m_rectrfScoreOfUser.rect.width+10;
        //single score
        //m_anchorScoreWordStart = m_rectrfScoreWordParent.anchoredPosition;
        m_anchorScoreOfUserStart = m_rectrfScoreOfUser.anchoredPosition;

        //m_anchorScoreWordMoveTo = new Vector2(m_anchorScoreWordStart.x-m_distanceMove,m_anchorScoreWordStart.y);
        m_anchorScoreOfUserMoveTo = new Vector2(m_anchorScoreOfUserStart.x +m_distanceMove,m_anchorScoreOfUserStart.y);

        m_positionScoreUser = m_trfPositionScoreUser.position;
        //m_positionScoreRed = m_trfPositionScoreRed.position;
        //m_positionScoreBlue = m_trfPositionScoreBlue.position;

        //m_anchorScoreRedStart = m_rectrfScoreRed.anchoredPosition;
        //m_anchorScoreBlueStart = m_rectrfScoreBlue.anchoredPosition;
        //m_anchorScoreBlueMoveTo = new Vector2(m_anchorScoreBlueStart.x + m_distanceMove,m_anchorScoreBlueStart.y);
        //m_anchorScoreRedMoveTo = new Vector2(m_anchorScoreRedStart.x - m_distanceMove,m_anchorScoreRedStart.y);
    }
    #region MAIN...
    private void GetAllChildScoreWord()
    {
        //int children = m_containScoreOfWord.childCount;
        //for (int i = 0; i < children; ++i)
        //{
        //    m_arrayScoreOfWord[i] = m_containScoreOfWord.GetChild(i).gameObject;
        //}
    }

    // reset all score && reset show popup continue
    public void ResetAllScore()
    {
        m_singleScore.ResetAllScore();
        //m_multiScore.ResetAllScore();
    }

    public void SetUpUserMode()
    {
        switch (GamePlayConfig.Instance.UserPlayMode)
        {
            case eUserPlayMode.SINGLE_PLAY:
                ActiveUserMode(true);
                break;
            case eUserPlayMode.MULTI_PLAY:
                ActiveUserMode(false);
                break;
        }
    }

    private void ActiveUserMode(bool _isActiveSingleMode)
    {
        m_objScoreOfSingle.SetActive(_isActiveSingleMode);
       // m_objScoreOfMulti.SetActive(!_isActiveSingleMode);
    }

    public void ResetScoreWithNewWord()
    {
        switch (GamePlayConfig.Instance.UserPlayMode)
        {
            case eUserPlayMode.SINGLE_PLAY:
                //EnableAllScoreOfWord();
                m_singleScore.m_scoreOfWord = 0;
                break;
            default:
                break;
        }
    }

    private void EnableAllScoreOfWord()
    {
        //m_vertcalLayoutScoreOfWord.enabled = true;
        for (int i = 0; i < m_arrayScoreOfWord.Length; i++)
        {
            if (!m_arrayScoreOfWord[i].activeInHierarchy)
            {
                //m_arrayScoreOfWord[i].transform.localScale = Vector3.one;
                m_arrayScoreOfWord[i].SetActive(true);
            }
        }
    }
    private void DisableAllScoreOfWord()
    {
        for (int i = 0; i < m_arrayScoreOfWord.Length; i++)
        {
            if (m_arrayScoreOfWord[i].activeInHierarchy)
            {
                //m_arrayScoreOfWord[i].transform.localScale = Vector3.one;
                m_arrayScoreOfWord[i].SetActive(false);
            }
        }
    }

    // goi 1 lan khi sai 1 ki tu
    // single mode
    public void DecreaseScoreOfWord()
    {
        //if (m_vertcalLayoutScoreOfWord.enabled)
        //{
        //    m_vertcalLayoutScoreOfWord.enabled = false;
        //}
        if (m_singleScore.m_scoreOfWord >= 0 & m_singleScore.m_scoreOfWord <= m_maxScoreOfWord)
        {
            //Transform trfScoreOfWord = m_arrayScoreOfWord[m_singleScore.m_scoreOfWord].transform;
            //MoveOneScoreWord(trfScoreOfWord);//effect
            m_singleScore.m_scoreOfWord += 1;
        }
        else
        {
            //game finish
            Debug.Log("het diem de tru roi !");
        }
    }

    private void MoveOneScoreWord(Transform _trfOneScoreWord)
    {
        _trfOneScoreWord.DOMoveX(_trfOneScoreWord.position.x - 0.85f, 2f).OnComplete(()=>DisableOneScoreWord(_trfOneScoreWord));
    }

    private void DisableOneScoreWord(Transform _trfOneScoreWord)
    {
        _trfOneScoreWord.gameObject.SetActive(false);
    }

    public bool CheckFinishCategory()
    {
        if (m_singleScore.ScoreOfUser >= 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region EFFECT...
    private const Ease m_easeType = Ease.OutSine;
    [Header("Move all score word...")]
    [SerializeField]
    private float m_timeMoveToRoot =0.3f;
    private const float m_timeDelayMoveScoreWord = 0.15f;

    [Header("Effect single & multi...")]
    [Header("Move in start new question...")]
    public const float m_timeMoveScore = 0.75f;
    public float m_distanceMove = 2f;
    [Space(5)]
    [Header("(start)In Single Score..")]
    //public RectTransform m_rectrfScoreWordParent;
    public RectTransform m_rectrfScoreOfUser;
    //public Transform m_trfQPerP;


    //private Vector2 m_anchorScoreWordStart = new Vector2();
    private Vector2 m_anchorScoreOfUserStart = new Vector2();

    //private Vector2 m_anchorScoreWordMoveTo = new Vector2();
    public Vector2 m_anchorScoreOfUserMoveTo = new Vector2();

   // private Vector3 m_positionMoveToScoreWord = Vector3.zero;
    private Vector3 m_positionMoveToScoreUser = Vector3.zero;

    public Transform m_trfPositionScoreUser;
    //public Transform m_trfPositionScoreRed;
    //public Transform m_trfPositionScoreBlue;
    private Vector3 m_positionScoreUser = new Vector3();
    //private Vector3 m_positionScoreRed = new Vector3();
    //private Vector3 m_positionScoreBlue = new Vector3();
    //[Space(5)]
    //[Header("In Multi Score..")]
    //public RectTransform m_rectrfScoreRed;
    //public RectTransform m_rectrfScoreBlue;
    //private Vector2 m_anchorScoreRedStart = new Vector2();
    //private Vector2 m_anchorScoreBlueStart = new Vector2();
    //private Vector2 m_anchorScoreRedMoveTo = new Vector2();
    //private Vector2 m_anchorScoreBlueMoveTo = new Vector2();

    public void StartMoveAllScoreWordToRoot()
    {
        StartCoroutine(MoveAllScoreWordToRoot());
    }

    public void DoMoveScore()
    {
        ResetScoreWithNewWord();
        SingleScoreMove();
        //if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.MULTI_PLAY)
        //{
        //    //MultiScoreMove();
        //}
        //else
        //{
        //    ResetScoreWithNewWord();
        //    SingleScoreMove();
        //}
    }

    public void DoCloseScore()
    {
        if(GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            SingleScoreClose();
        }
        //else
        //{
        //    //MultiScoreClose();
        //}
    }
    private void SingleScoreMove()
    {
        MoveScoreOfUser();
        //MoveScoreOfWord();
    }
    //private void MultiScoreMove()
    //{
    //    MoveScoreRed();
    //    //MoveScoreBlue();
    //}
    private void SingleScoreClose()
    {
        //CloseScoreWord();
        CloseScoreUser();
    }
    //private void MultiScoreClose()
    //{
    //    CloseScoreRed();
    //    CloseScoreBlue();
    //}
    // move all "score word" 
    //change question when finish
    private IEnumerator MoveAllScoreWordToRoot()
    {
        //yield return new WaitForSeconds(0.5f);
        //for (int i = 0; i < m_arrayScoreOfWord.Length; i++)
        //{
        //    if (m_arrayScoreOfWord[i].activeInHierarchy)
        //    {
        //        Transform trfScoreWord = m_arrayScoreOfWord[i].transform;
        //        trfScoreWord.DOMove(m_positionScoreUser, m_timeMoveToRoot)
        //            .SetEase(m_easeType)
        //            .OnComplete(AddScoreOfUser);
        //    }
        //    yield return new WaitForSeconds(m_timeDelayMoveScoreWord);
        //}
        m_singleScore.ScoreOfUser += m_arrayScoreOfWord.Length;

        ManagerObject.Instance.SpawnObjectByType(eObjectType.par_pickup, ePoolName.ObjectPool).transform.position = m_positionScoreUser;
        // wait effect finish
        yield return new WaitForSeconds(0.75f);
        //save score
        if (GamePlayConfig.Instance.ModeLevel != eModeLevel.LEARN_LETTER)
        {
            DataManager.instance.SetScoreInCategory(m_singleScore.ScoreOfUser);
        }
        else
        {
            DataManager.instance.SetScoreInLearnLetter(m_singleScore.ScoreOfUser, GamePlayConfig.Instance.TypeShowQuestion);
        }
        DoCloseScore();
        DisableAllScoreOfWord();
        GameController.Instance.DoHideMainImage();
    }

    // scale user score && + score user 
    private void AddScoreOfUser()
    {
        m_singleScore.ScoreOfUser += 1;
    }

    //private void MoveScoreOfWord()
    //{
    //    //m_vertcalLayoutScoreOfWord.enabled = true;
    //    //m_rectrfScoreWordParent.anchoredPosition = m_anchorScoreWordMoveTo;
    //    //m_rectrfScoreWordParent.DOAnchorPos(m_anchorScoreWordStart, m_timeMoveScore)
    //    //    .SetEase(m_easeType)
    //    //    .OnComplete(OnCompleteMoveScoreWord);
    //}
    //private void OnCompleteMoveScoreWord()
    //{
    //    //m_vertcalLayoutScoreOfWord.enabled = false;
    //}
    private void MoveScoreOfUser()
    {
        m_rectrfScoreOfUser.anchoredPosition = m_anchorScoreOfUserMoveTo;
        m_rectrfScoreOfUser.DOAnchorPos(m_anchorScoreOfUserStart,m_timeMoveScore).SetEase(m_easeType);
    }
    //private void MoveScoreRed()
    //{
    //    //m_rectrfScoreRed.anchoredPosition = m_anchorScoreRedMoveTo;
    //    //m_rectrfScoreRed.DOAnchorPos(m_anchorScoreRedStart, m_timeMoveScore).SetEase(m_easeType);
    //}

    //private void MoveScoreBlue()
    //{
    //    //m_rectrfScoreBlue.anchoredPosition = m_anchorScoreBlueMoveTo;
    //    //m_rectrfScoreBlue.DOAnchorPos(m_anchorScoreBlueStart, m_timeMoveScore).SetEase(m_easeType);
    //}

    //// scale to start 
    //public void OpenQperP()
    //{
    //    //m_trfQPerP.DOScale(Vector3.one, 0.35f).SetEase(Ease.Linear).SetDelay(0.35f);
    //}
   
    //public void CloseQperP()
    //{
    //   // m_trfQPerP.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear);
    //}
    //private void CloseScoreWord()
    //{
    //    //m_rectrfScoreWordParent.DOAnchorPos(m_anchorScoreWordMoveTo, m_timeMoveScore).SetEase(m_easeType);
    //}
    private void CloseScoreUser()
    {
        m_rectrfScoreOfUser.DOAnchorPos(m_anchorScoreOfUserMoveTo, m_timeMoveScore).SetEase(m_easeType);
    }
    //private void CloseScoreRed()
    //{
    //    //m_rectrfScoreRed.DOAnchorPos(m_anchorScoreRedMoveTo, m_timeMoveScore).SetEase(m_easeType);
    //}
    //private void CloseScoreBlue()
    //{
    //    //m_rectrfScoreBlue.DOAnchorPos(m_anchorScoreBlueMoveTo, m_timeMoveScore).SetEase(m_easeType);
    //}
    #endregion

    #region MULTI_SCORE_ADD
    //[SerializeField]
    //private float m_timeMoveRootScoreAdd = 0.5f;
    //private Ease m_easeTypeMoveScoreAdd = Ease.Linear;
    // call in multiMode 
    //public void DoAddScoreOfMulti(int _scoreAdd, eBaseTeamType _team)
    //{
    //    //switch (_team)
    //    //{
    //    //    case eBaseTeamType.NONE:
    //    //        break;
    //    //    case eBaseTeamType.TEAM_BLUE:
    //    //        ManagerObject.Instance.SpawnObjectByType(eObjectType.par_pickup, ePoolName.ObjectPool).transform.position = m_positionScoreBlue;
    //    //        //m_multiScore.ScoreOfBlue += _scoreAdd;
    //    //        break;
    //    //    case eBaseTeamType.TEAM_RED:
    //    //        ManagerObject.Instance.SpawnObjectByType(eObjectType.par_pickup, ePoolName.ObjectPool).transform.position = m_positionScoreRed;
    //    //        //m_multiScore.ScoreOfRed += _scoreAdd;
    //    //        break;
    //    //}
    //}
    ////// callBack
    ////private void MoveToRootScoreAdd(Transform _trfScoreAddPrefabs, eBaseTeamType _team, int _scoreAdd)
    ////{
    ////    switch (_team)
    ////    {
    ////        case eBaseTeamType.NONE:
    ////            break;
    ////        case eBaseTeamType.TEAM_BLUE:
    ////            _trfScoreAddPrefabs.DOMove(m_rectrfScoreBlue.position, m_timeMoveRootScoreAdd)
    ////                .SetEase(m_easeType)
    ////                .OnComplete(() => OnCompleteMoveToRootScoreBlue(_trfScoreAddPrefabs, _scoreAdd));
    ////            break;
    ////        case eBaseTeamType.TEAM_RED:
    ////            _trfScoreAddPrefabs.DOMove(m_rectrfScoreRed.position, m_timeMoveRootScoreAdd)
    ////                .SetEase(m_easeType)
    ////                .OnComplete(() => OnCompleteMoveToRootScoreRed(_trfScoreAddPrefabs, _scoreAdd));
    ////            break;
    ////    }
    ////}
    //private void OnCompleteMoveToRootScoreRed(Transform _trfScoreAddPrefabs, int _scoreAdd)
    //{
    //    //m_multiScore.ScoreOfRed += _scoreAdd;
    //    ManagerObject.Instance.DespawnObject(_trfScoreAddPrefabs.gameObject, ePoolName.pool);
    //}
    //private void OnCompleteMoveToRootScoreBlue(Transform _trfScoreAddPrefabs, int _scoreAdd)
    //{
    //    //m_multiScore.ScoreOfBlue += _scoreAdd;
    //    ManagerObject.Instance.DespawnObject(_trfScoreAddPrefabs.gameObject, ePoolName.pool);
    //}

    #endregion
}
