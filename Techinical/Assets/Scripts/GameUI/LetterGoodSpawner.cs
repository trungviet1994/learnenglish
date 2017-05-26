using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;

//create letter prefabs & disable all letter prefabs
public class LetterGoodSpawner : MonoSingleton<LetterGoodSpawner>
{
    private int m_maxLetterInLine = 11;
    // array all letter good (max 11 letter)
    private GameObject[] m_arrayAllLetterGood;
    // array letter good prefabs of letter
    private GameObject[] m_arrayLetterPrefabs = new GameObject[0];
    public GameObject[] ArrayLetterPrefabs
    {
        get { return m_arrayLetterPrefabs; }
        set { m_arrayLetterPrefabs = value; }
    }
    public Transform m_letterGoodParent;
    public Vector2 SizeOfGrid { 
        get 
        {
            if (m_gridLetterGood)
            {
                return m_gridLetterGood.cellSize;
            }
            else
            {
                return Vector2.zero;
            }
        } 
    }
    public GridLayoutGroup m_gridLetterGood;
    private const float m_maxGridSize = 140;
    private const float m_minGridSize = 115;
    private GameObject m_spaceObject;

    public void Initiate()
    {
        m_myStartPosition = m_letterGoodParent.position;
        CreateAllLetterGood();
    }

    // run as start
    // create max prefabs letters
    public void CreateAllLetterGood()
    {
        m_arrayAllLetterGood = new GameObject[m_maxLetterInLine];
        for (int i = 0; i < m_maxLetterInLine; i++)
        {
            GameObject letterPrefabs = ManagerObject.Instance.SpawnObjectByType(eObjectType.ui_Letter_Good, ePoolName.pool);
            letterPrefabs.transform.SetParent(m_letterGoodParent);
            letterPrefabs.SetActive(false);
            m_arrayAllLetterGood.SetValue(letterPrefabs, i);
        }
    }
    //set up gird size with screen
    private void SetUpGridSize(int _letterCount)
    {
        if (_letterCount <= 8)
        {
            m_gridLetterGood.cellSize = Vector2.one * m_maxGridSize;
        }
        else
        {
            float newSize = m_maxGridSize - ((_letterCount - 8) * 10);
            m_gridLetterGood.cellSize = Vector2.one * newSize;
        }
    }

    public void SpawnLetterGoodPrefabs(int _letterCount, int _indexOfSpace, string _keywordOfLetterHead = "",bool _isHideLetterHead = false)
    {
        DisableAllLetterSpawned();
        m_gridLetterGood.enabled = true;

        m_arrayLetterPrefabs = new GameObject[_letterCount];
        SetUpGridSize(_letterCount);
        for (int i = 0; i < _letterCount; i++)
        {
            m_arrayAllLetterGood[i].SetActive(true);
            m_arrayAllLetterGood[i].transform.localScale = Vector3.zero;
            m_arrayLetterPrefabs.SetValue(m_arrayAllLetterGood[i], i);
        }

        if (_indexOfSpace != -1)
        {
            m_spaceObject = m_arrayLetterPrefabs[_indexOfSpace];
            m_spaceObject.GetComponent<MyLetter>().Hide();
            m_arrayLetterPrefabs = RemoveIndices(m_arrayLetterPrefabs, _indexOfSpace); // split space array letter
        }
        LetterSpawn.Instance.ResetToStart();
        if(_isHideLetterHead)
        {
            for(int i=1;i<_keywordOfLetterHead.Length;i++)
            {
                m_arrayLetterPrefabs[i].GetComponent<MyLetter>().SetUpStart(_keywordOfLetterHead[i].ToString(), eBaseTeamType.NONE);
            }
        }
    }

    public Transform GetTranformLetterByIndex(int _indexInList)
    {
        if (_indexInList > m_arrayLetterPrefabs.Length || m_arrayLetterPrefabs[_indexInList] == null)
        {
#if UNITY_EDITOR
            Debug.Log("khong co position cua index nay!");
#endif
            return null;
        }
        else
        {
            return m_arrayLetterPrefabs[_indexInList].transform;
        }
    }

    // disable all letter spawned
    public void DisableAllLetterSpawned()
    {
        for (int i = 0; i < m_arrayLetterPrefabs.Length; i++)
        {
            if (m_arrayLetterPrefabs[i] != null)
            {
                m_arrayLetterPrefabs[i].SetActive(false);
            }
        }
        if (m_spaceObject)
        {
            ManagerObject.Instance.DespawnObject(m_spaceObject, ePoolName.pool);
            m_spaceObject = null;
        }
        m_arrayLetterPrefabs = new GameObject[0];
    }

    // remove 1 phan tu trong mang
    private GameObject[] RemoveIndices(GameObject[] IndicesArray, int RemoveAt)
    {
        GameObject[] newIndicesArray = new GameObject[IndicesArray.Length - 1];
        int i = 0;
        int j = 0;
        while (i < IndicesArray.Length)
        {
            if (i != RemoveAt)
            {
                newIndicesArray[j] = IndicesArray[i];
                j++;
            }
            i++;
        }
        return newIndicesArray;
    }

    #region EFFECT...
    [Space(10)]
    [Header("Effect...")]
    private float m_timeMoveUp = 0.5f;
    private float m_timeMoveToRoot = 0.5f;
    private float m_timeWaitMoveToRoot = 1f;
    [SerializeField]
    private Ease m_easeType = Ease.OutExpo;
    [SerializeField]
    private Vector3 m_myStartPosition;
    public Transform m_trfMoveToRoot;

    // Effect finish word - MOVE UP 
    public void StartEffectFinishWord(bool _isAuto)
    {
        m_gridLetterGood.enabled = false;
        Sequence mySequence = DOTween.Sequence();
        float timeCallBack = m_timeMoveUp + m_timeWaitMoveToRoot;
        m_letterGoodParent.DOMoveY(0, m_timeMoveUp).SetEase(m_easeType).OnComplete(() => OnCompleteMoveUp(_isAuto));
        mySequence.InsertCallback(timeCallBack, () => StartMoveAllToRoot(_isAuto));
        ScaleAllLetter();
    }

    private void OnCompleteMoveUp(bool _isAuto)
    {
        GameController.Instance.PlayAudioCurrentWord();
        ManagerObject.Instance.SpawnObjectByType(eObjectType.par_cogratulation, ePoolName.ObjectPool).transform.position = Vector3.zero;
        if (!_isAuto &&GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            // start effect and save score 
            GameController.Instance.m_scoreManager.StartMoveAllScoreWordToRoot();
        }
    }
    //scale when finish
    private void ScaleAllLetter()
    {
        Vector3 scaleToValue = Vector3.one * 1.3f;
        for (int i = 0; i < ArrayLetterPrefabs.Length; i++)
        {
            ArrayLetterPrefabs[i].transform.DOScale(scaleToValue, m_timeMoveUp).SetEase(m_easeType).SetAutoKill(true);
        }
    }
    // move Down all letter good
    // change question when finish
    private void StartMoveAllToRoot(bool _isAuto)
    {
        StartCoroutine(MoveAllToRoot(_isAuto));
    }
    // move all letter good to root
    // change question when finish with multi mode
    private IEnumerator MoveAllToRoot(bool _isAuto)
    {
        for (int i = 0; i < ArrayLetterPrefabs.Length; i++)
        {
            ArrayLetterPrefabs[i].transform.DOMove(m_trfMoveToRoot.position, m_timeMoveToRoot).SetEase(m_easeType);
            ArrayLetterPrefabs[i].transform.DOScale(Vector3.one, m_timeMoveToRoot).SetEase(m_easeType).SetAutoKill();
            yield return new WaitForSeconds(0.15f);
        }
        //reset all
        m_letterGoodParent.position = m_myStartPosition;
        if(GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.MULTI_PLAY || _isAuto)
        {
            GameController.Instance.DoHideMainImage();
        }
    }

    private const float m_timeScale = 0.25f;
    private const float m_timeDelay = 0.1f;
    private IEnumerator EffectScaleWhenSpawn(Vector3 m_valueScaleTo,Ease _easeTypeScale)
    {
        yield return new WaitForSeconds(m_timeDelay);
        for (int i = 0; i < ArrayLetterPrefabs.Length; i++)
        {
            ArrayLetterPrefabs[i].transform.DOScale(m_valueScaleTo, m_timeScale).SetEase(_easeTypeScale);
            AudioManager.Instance.PlayAudioByTypeName(eAudioName.TICK);
            yield return new WaitForSeconds(m_timeDelay);
        }
        if (GamePlayConfig.Instance.ModeLevel == eModeLevel.LEARN_LETTER && m_valueScaleTo.x > 0)
        {
            EffectUpLetterGood(0);
        }
    }

    public void EffectScaleOpenAllLetterGood()
    {
        StartCoroutine(EffectScaleWhenSpawn(Vector3.one,Ease.OutBack));
    }
    public void EffectScaleSCloseAllLetterGood()
    {
        StartCoroutine(EffectScaleWhenSpawn(Vector3.zero,Ease.OutCubic));
    }
    #endregion

    #region EFFECT WITH HELP...
    [Header("effect with some letter prefabs")]
    private float m_timeScaleLetterHelp = 0.6f;
    //call for help
    public void ScaleAllLetterGoodWithLoop()
    {
        Vector3 scaleToValue = Vector3.one * 1.25f;
        for (int i = 0; i < ArrayLetterPrefabs.Length; i++)
        {
            ArrayLetterPrefabs[i].transform.DOScale(scaleToValue, m_timeScaleLetterHelp).SetEase(m_easeType).SetLoops(2, LoopType.Yoyo).SetAutoKill(true);
        }
    }
    public void EffectUpLetterGood(List<int> _listIndex)
    {
        for (int i = 0; i < _listIndex.Count; i++)
        {
            Transform trfChoised = GetTranformLetterByIndex(_listIndex[i]);
            trfChoised.DOMoveY(m_letterGoodParent.position.y + 1.15f, 0.45f)
                .OnComplete(() => CallBackAutoShow(trfChoised));
        }
    }
    public void EffectUpLetterGood(int _index)
    {
        Transform trfChoised = GetTranformLetterByIndex(_index);
        trfChoised.DOMoveY(m_letterGoodParent.position.y + 1.15f, 0.45f)
            .OnComplete(() => CallBackAutoShow(trfChoised));
    }

    public void EffectWithLetterExist(string _letter)
    {
        if(m_gridLetterGood.enabled)
        {
            m_gridLetterGood.enabled = false;
        }
        List<int> listIndexOfLetter = GameController.Instance.m_keyWord.GetIndexLetterInWord(_letter);
        for (int i = 0; i < listIndexOfLetter.Count; i++)
        {
            EffectAutoShow(listIndexOfLetter[i],_letter);
        }
    }

    public void EffectAutoShow(int _index,string _letter)
    {
        if(m_gridLetterGood.enabled)
        {
            m_gridLetterGood.enabled = false;
        }
        Transform trfChoised = GetTranformLetterByIndex(_index);
        trfChoised.DOMoveY(m_letterGoodParent.position.y + 1.15f, 0.45f)
            .OnComplete(() => CallBackAutoShow(trfChoised));
        trfChoised.GetComponent<MyLetter>().SetUpStart(_letter, eBaseTeamType.NONE);
    }
    private void CallBackAutoShow(Transform _trf)
    {
        _trf.DOMoveY(m_letterGoodParent.position.y, 0.35f).SetEase(Ease.OutBounce).OnComplete(CallBackMoveDown);
    }
    private void CallBackMoveDown()
    {
        if(GameController.Instance.CheckWordIsFinish())
        {
            GameController.Instance.OnFinishWord(true,true);
        }
    }
    #endregion
}
