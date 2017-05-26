using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class LetterSpawn : MonoSingleton<LetterSpawn>
{
    private const float timeScaleLetterTracked = 0.25f;
    private const float m_sizeLetterPrefabsFail = 95.0f;
    private const float m_sizeLetterPrefabsGood = 100.0f;
    private Vector2 m_sizeOfLetterFailPrefabs  =new Vector2();
    // get object trong mang letter fail
    private int m_indexOfLetterFail = 0;
    //private List<string> m_listStringLetterFail = new List<string>();
    [SerializeField]
    private string m_allLetterFailTrackingFail = "";

    //public delegate void d_myCheckLetterHelp(bool _isAnswerGood);
    //public event d_myCheckLetterHelp onCheckLetterHelp;

    public GameObject[] m_arrayLetterFail; // from scene
    public GameObject[] ArrayLetterFail
    {
        get { return m_arrayLetterFail; }
        set { m_arrayLetterFail = value; }
    }
    public int IndexOfLetterFail
    {
        get { return m_indexOfLetterFail; }
        set { m_indexOfLetterFail = value; }
    }

    public void Initiate()
    {
        m_sizeOfLetterFailPrefabs = Vector2.one * m_sizeLetterPrefabsFail;
        m_positionStartLetterPrefabs = new Vector3(0, -1 * (GamePlayConfig.Instance.ScreenPosition.y - 1.0f));
    }

    // create letter prefabs from keyword
    // show in scene
    public void CreateLetterGoodFromKeyWord(int _indexOfSpace, string _keyWord)
    {
        if (GamePlayConfig.Instance.ModeLevel == eModeLevel.LEARN_LETTER)
        {
            _keyWord = DataManager.instance.GetWordFromLetterHead(_keyWord);
            if (_keyWord == null)
            {
                _keyWord = GameController.Instance.m_keyWord.StrKeyWord;
            }
            LetterGoodSpawner.Instance.SpawnLetterGoodPrefabs(_keyWord.Length, _indexOfSpace, _keyWord, true);
        }
        else
        {
            LetterGoodSpawner.Instance.SpawnLetterGoodPrefabs(_keyWord.Length, _indexOfSpace);
        }
    }

    // create prefabs letter good when tracking
    // check letter Exist ?
    public void DoWithLetterGood(string _letter, eBaseTeamType _team)
    {
        // get show
        if (GameController.Instance.m_keyWord.CheckLetterTracked(_letter))
        {
            AudioManager.Instance.PlayAudioByLetter(_letter);
            LetterGoodSpawner.Instance.EffectWithLetterExist(_letter);
        }
        else
        {
            List<int> listIndexLetterInWord = GameController.Instance.m_keyWord.GetIndexLetterInWord(_letter);
            AudioManager.Instance.PlayLetterWithAnswer(_letter, true);
            for (int i = 0; i < listIndexLetterInWord.Count; i++)
            {
                GameObject letterPrefabs = ManagerObject.Instance.SpawnObjectByType(eObjectType.Letter_Prefabs, ePoolName.pool);
                letterPrefabs.GetComponent<UILetter>().SetUpStart(_letter, _team,false);
                
                MoveUpLetterPrefab(letterPrefabs.transform, LetterGoodSpawner.Instance.GetTranformLetterByIndex(listIndexLetterInWord[i]), _team);
                //replace key word hide 
                GameController.Instance.m_keyWord.ReplaceKeyHide(listIndexLetterInWord[i]);
            }
            listIndexLetterInWord.Clear();
        }
    }

    // create prefabs letter fail when tracking
    // return : true : da ton tai , false :chua ton tai
    public void DoWithLetterFail(string _letter, eBaseTeamType _team)
    {
        if (CheckLetterFailIsTracked(_letter))   // existed
        {
            AudioManager.Instance.PlayAudioByLetter(_letter);
            EffectWithLetterFailExist(_letter);
        }
        else
        {
            AudioManager.Instance.PlayLetterWithAnswer(_letter, false);
            GameObject letterPrefabs = ManagerObject.Instance.SpawnObjectByType(eObjectType.Letter_Prefabs, ePoolName.pool);
            letterPrefabs.GetComponent<UILetter>().SetUpStart(_letter, _team);
            MoveUpLetterPrefab(letterPrefabs.transform, m_arrayLetterFail[IndexOfLetterFail].transform, _team, false);
            
            if(GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
            {
                GameController.Instance.m_scoreManager.DecreaseScoreOfWord();
            }
            //onCheckLetterHelp(false);
            IndexOfLetterFail++;
            m_allLetterFailTrackingFail += _letter;
        }
    }
    private void EffectWithLetterFailExist(string _letter)
    {
        int index = m_allLetterFailTrackingFail.IndexOf(_letter);
        ArrayLetterFail[index].transform.DOScale(Vector3.one * 1.5f,0.4f)
            .SetLoops(2,LoopType.Yoyo);
    }

    private bool CheckLetterFailIsTracked(string _letter)
    {
        bool result = m_allLetterFailTrackingFail.Contains(_letter);
        return result;
    }

    public void ResetToStart()
    {
        IndexOfLetterFail = 0;
        ResetAllMyLetter();
        m_allLetterFailTrackingFail = "";
    }
    private void ResetAllMyLetter()
    {
        for(int i=0;i< m_arrayLetterFail.Length;i++)
        {
            MyLetter myLetter = m_arrayLetterFail[i].GetComponent<MyLetter>();
            if(myLetter )
            {
                myLetter.Reset();
            }
        }
        for (int i = 0; i < LetterGoodSpawner.Instance.ArrayLetterPrefabs.Length; i++)
        {
            MyLetter myLetter = LetterGoodSpawner.Instance.ArrayLetterPrefabs[i].GetComponent<MyLetter>();
            if (myLetter)
            {
                myLetter.Reset();
            }
        }
    }

    #region EFFECT_CALL_FUNCTION
    public void StartScaleArrayLetter()
    {
        //StartScaleArrayLetterGood();
        LetterGoodSpawner.Instance.EffectScaleOpenAllLetterGood();
        StartScaleArrayLetterFail();
    }

    //// scale all letter tracked
    //public void DoScaleLetterTracked(GameObject[] _arrayLetterTracked)
    //{
    //    for (int i = 0; i < _arrayLetterTracked.Length; i++)
    //    {
    //        _arrayLetterTracked[i].transform.DOScale(m_valueScaleToLetterTracked, m_timeScaleLetterTracked)
    //             .SetEase(m_easeScaleLetterTracked).SetLoops(2, LoopType.Yoyo).SetAutoKill();
    //    };
    //}

    #endregion

    #region EFFECT_MOVE_DEFINE...
    [Space(5)]
    [Header("Move Letter Prefabs...")]
    private const float m_timeMoveUpLetterPrefab = 0.3f;
    private const float m_timeMoveDownLetterPrefab = 0.35f;
    private const float m_timeWaitMoveDown = 0.5f;
    private const float m_valueScaleLetterPrefab = 2.5f;

    public Ease m_easeTypeMoveUpLetterPrefab = Ease.OutCirc;
    public Ease m_easeTypeMoveDownLetterPrefab = Ease.OutCirc;
    public Ease m_easeTypeScaleLetterPrefab = Ease.OutCirc;
    private Vector3 m_positionStartLetterPrefabs = new Vector3();
    public void MoveUpLetterPrefab(Transform _trfLetterPrefab, Transform _trfMoveTo, eBaseTeamType _team, bool _isAnswerGood = true)
    {
        _trfLetterPrefab.position = m_positionStartLetterPrefabs;
        _trfLetterPrefab.DOMove(Vector3.zero, m_timeMoveUpLetterPrefab)
            .SetEase(m_easeTypeMoveUpLetterPrefab)
            .OnComplete(() => MoveDownLetterPrefab(_trfLetterPrefab, _trfMoveTo,_team, _isAnswerGood));
        _trfLetterPrefab.DOScale(Vector3.one * m_valueScaleLetterPrefab, m_timeMoveUpLetterPrefab)
            .SetEase(m_easeTypeScaleLetterPrefab);
    }
    
    // move to root letter prefabs and check finish word or not
    private void MoveDownLetterPrefab(Transform _trfLetterPrefab, Transform _trfMoveTo,eBaseTeamType _team,bool _isAnswerGood)
    {
        _trfLetterPrefab.DOMove(_trfMoveTo.position, m_timeMoveUpLetterPrefab)
            .SetEase(m_easeTypeMoveUpLetterPrefab)
            .OnComplete(() => DespawnWhenComplete(_trfLetterPrefab, _trfMoveTo,_isAnswerGood))
            .SetDelay(m_timeWaitMoveDown);
        _trfLetterPrefab.DOScale(Vector3.one, m_timeMoveUpLetterPrefab)
            .SetEase(m_easeTypeScaleLetterPrefab)
            .SetDelay(m_timeWaitMoveDown);

        //if(GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.MULTI_PLAY)
        //{
        //    int scoreAddInMulti = -1;
        //    if(_isAnswerGood)
        //    {
        //        scoreAddInMulti = 2;
        //    }
        //    GameController.Instance.m_scoreManager.DoAddScoreOfMulti(scoreAddInMulti,_team);
        //}
        
    }

    private bool CheckIsFinish()
    {
        bool result = false;
        //check finish word or not
        if (IndexOfLetterFail >= 10)
        {
            GameController.Instance.OnFinishWord(false); // your lose
            result = true;
        }
        else if(GameController.Instance.CheckWordIsFinish())
        {
            GameController.Instance.OnFinishWord(true);
            result = true;
        }
        return result;
    }

    private void DespawnWhenComplete(Transform _trfLetter, Transform _trfRootLetter,bool _isGoodAnswer)
    {
        if (_trfLetter)
        {
            UILetter myLetter = _trfLetter.GetComponent<UILetter>();
            if (myLetter)
            {
                OnCompleteMoveDownLetter(_trfRootLetter, myLetter.MyLetter, myLetter.Team,_isGoodAnswer);
                ManagerObject.Instance.DespawnObject(_trfLetter.gameObject, ePoolName.pool);
            }
        }
    }
    // set up show letter on screen
    private void OnCompleteMoveDownLetter(Transform _trfLetterRoot, string _letter, eBaseTeamType _team,bool _isGoodAnswer)
    {
        ManagerObject.Instance.SpawnObjectByType(eObjectType.par_pickup, ePoolName.ObjectPool).transform.position = _trfLetterRoot.position;
        _trfLetterRoot.GetComponent<MyLetter>().SetUpStart(_letter, _team);

        if(!CheckIsFinish())
        {
            Help.Instance.CheckLetterForHelp(_isGoodAnswer);
        }
        // check for help

    }
    #endregion

    #region EFFECT_SCALE...
    [Space(10)]
    [Header("Scale Letter Fail & Good in Scene")]
    private const float m_timeScale = 0.3f;
    private const float m_timeWaitScale = 0.1f;
    private const Ease m_easeScale = Ease.OutBack;
    private void StartScaleArrayLetterFail()
    {
        StartCoroutine(ScaleArrayLetterFailWithCoroutine(Vector3.one, Ease.OutBack));
    }
    public void SetupAllLetterToZero()
    {
        for(int i=0;i<ArrayLetterFail.Length;i++)
        {
            ArrayLetterFail[i].transform.localScale = Vector3.zero;
        }
    }
    public void ScaleAllToZero()
    {
        StartCoroutine(ScaleArrayLetterFailWithCoroutine(Vector3.zero,Ease.OutCubic));
        LetterGoodSpawner.Instance.EffectScaleSCloseAllLetterGood();
        //StartCoroutine(ScaleArrayLetterGoodWithCoroutine(LetterGoodSpawner.Instance.ArrayLetterPrefabs, Vector3.zero, 0f));
    }
    private IEnumerator ScaleArrayLetterFailWithCoroutine(Vector3 _valueScaleTo,Ease _easeTypeScale)
    {
        int i = 0;
        int j = ArrayLetterFail.Length / 2;
        yield return new WaitForSeconds(m_timeWaitScale);
        while (true)
        {
            if (i >= ArrayLetterFail.Length / 2 || j >= ArrayLetterFail.Length)
            {
                break;
            }
            ArrayLetterFail[i].transform.DOScale(_valueScaleTo, m_timeScale)
                .SetEase(_easeTypeScale);
            ArrayLetterFail[j].transform.DOScale(_valueScaleTo, m_timeScale)
                .SetEase(_easeTypeScale);
            i++;
            j++;
            AudioManager.Instance.PlayAudioByTypeName(eAudioName.TICK);
            yield return new WaitForSeconds(m_timeWaitScale);
        }
    }

    //private IEnumerator ScaleArrayLetterGoodWithCoroutine(GameObject[] _arrayLetterGood, Vector3 _valueScaleTo,float _timeDelay)
    //{
    //    yield return new WaitForSeconds(_timeDelay);
    //    for (int i = 0; i < _arrayLetterGood.Length; i++)
    //    {
    //        _arrayLetterGood[i].transform.DOScale(_valueScaleTo, m_timeScaleArrayLetter).SetEase(m_easeScaleArrayLetter);
    //        AudioManager.Instance.PlayAudioByTypeName(eAudioName.TICK);
    //        yield return new WaitForSeconds(m_timeWaitCoroutine);
    //    }
    //    if(GamePlayConfig.Instance.ModeLevel = eModeLevel.LEARN_LETTER)
    //    {
            
    //    }
    //}
    #endregion
}
