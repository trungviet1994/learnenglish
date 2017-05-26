using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EffectDefine : EffectManagerAbstract
{
    // move letter prefabs delegate
    //private delegate void d_ScaleArrayLetter(GameObject[] _arrLetterPrefab);
    //d_ScaleArrayLetter m_myScaleArrayLetter;

    #region OVERRIDE_FUNCTION...
    //public delegate void d_CompleteMoveDown(Transform _trfLetter, string _letter, GameConfig.eBaseTeamType _team);
    //public event d_CompleteMoveDown onCompleteMoveDown;
    //public override void MoveLetterPrefabsTrackable(Transform _trfLetterPrefab,Transform _trfMoveTo)
    //{
    //    //MoveUpLetterPrefab(_trfLetterPrefab, _trfMoveTo);
    //}
   
    //public override void ScaleArrayLetter(GameObject[] _arrLetterFail, GameObject[] _arrLetterGood)
    //{
    //    //m_myScaleArrayLetter = StartScaleArrayLetterFail;
    //    //m_myScaleArrayLetter(_arrLetterFail);
    //    //m_myScaleArrayLetter = StartScaleArrayLetterGood;
    //    //m_myScaleArrayLetter(_arrLetterGood);
    //}
    //public override void PauseAllEffectInGamePlay()
    //{
    //    DOTween.PauseAll();
    //}
    //public override void PlayAllEffectInGamePlay()
    //{
    //    DOTween.PlayAll();
    //}

    //public override void KillAllEffectInGamePlay()
    //{
    //    DOTween.KillAll();
    //}

    //public override void DoScaleLetterTracked(GameObject[] _arrayLetterTracked)
    //{
    //    for (int i = 0; i < _arrayLetterTracked.Length; i++)
    //    {
    //        _arrayLetterTracked[i].transform.DOScale(m_valueScaleToLetterTracked, m_timeScaleLetterTracked)
    //             .SetEase(m_easeScaleLetterTracked).SetLoops(2, LoopType.Yoyo).SetAutoKill();
    //    };
    //}
    //public override void DoColorToMainImage()
    //{
    //    Sequence mySequence = DOTween.Sequence();
    //    mySequence.Append(DoAlphaTo(m_sprMainImage,0.25f,m_timeAlphaToMainImage));
    //    mySequence.Insert(m_timeAlphaToMainImage, DoAlphaTo(m_sprMainImage, 1, m_timeAlphaToMainImage));
    //    mySequence.SetAutoKill();
    //}

    //public override void EffectPopupScreen(GameObject _screenPopup)
    //{
    //    _screenPopup.transform.DOScale(Vector3.zero,m_timeScalePopup).From().SetEase(m_easeScalePopup);
    //}
    #endregion

    #region DEFINE_FUNCTION
    //private Tween DoAlphaTo(SpriteRenderer _spriteRender,float _valueTo,float _timer)
    //{
    //    return DOTween.ToAlpha(() => _spriteRender.color, x => _spriteRender.color = x, _valueTo, _timer);
    //}

    //private void MoveUpLetterPrefab(Transform _trfLetterPrefab, Transform _trfMoveTo)
    //{
    //    _trfLetterPrefab.position = new Vector3(0,-1*(GamePlayConfig.Instance.ScreenPosition.y-1.0f));
    //    Sequence mySequence = DOTween.Sequence();
    //    Tween tweenMoveUp = _trfLetterPrefab.DOMove(Vector3.zero,m_timeMoveUpLetterPrefab)
    //        .SetEase(m_easeTypeMoveUpLetterPrefab);
    //    Tween tweenScale = _trfLetterPrefab.DOScale(Vector3.one*3.0f,m_timeMoveUpLetterPrefab)
    //        .SetEase(m_easeTypeScaleLetterPrefab);

    //    mySequence.Append(tweenMoveUp);
    //    mySequence.Join(tweenScale);
    //    mySequence.InsertCallback(m_timeMoveUpLetterPrefab+m_timeWaitMoveDown, () => MoveDownLetterPrefab(_trfLetterPrefab, _trfMoveTo));
    //    mySequence.Play();
    //}

    //private void MoveDownLetterPrefab(Transform _trfLetterPrefab, Transform _trfMoveTo)
    //{
    //    Sequence mySequence = DOTween.Sequence();

    //    Tween tweenMoveUp = _trfLetterPrefab.DOMove(_trfMoveTo.position, m_timeMoveUpLetterPrefab)
    //        .SetEase(m_easeTypeMoveUpLetterPrefab);
    //    Tween tweenScale = _trfLetterPrefab.DOScale(Vector3.one,m_timeMoveUpLetterPrefab)
    //        .SetEase(m_easeTypeScaleLetterPrefab);

    //    mySequence.Append(tweenMoveUp);
    //    mySequence.Join(tweenScale);
    //    mySequence.SetAutoKill(true);
    //    mySequence.InsertCallback(m_timeMoveUpLetterPrefab,()=>DespawnWhenComplete(_trfLetterPrefab,_trfMoveTo));
    //    mySequence.Play();
    //}
    //private void DespawnWhenComplete(Transform _trfLetter,Transform _trfRootLetter)
    //{
    //    if(_trfLetter)
    //    {
    //        UILetter myLetter = _trfLetter.GetComponent<UILetter>();
    //        if (myLetter)
    //        {
    //            onCompleteMoveDown(_trfRootLetter, myLetter.MyLetter, myLetter.Team);
    //            ManagerObject.Instance.DespawnObject(_trfLetter.gameObject, ePoolName.pool);
    //        }
    //    }
    //}

    //private void StartScaleArrayLetterFail(GameObject[] _arrLetterFail)
    //{
    //    StartCoroutine(ScaleArrayLetterFailWithCoroutine(_arrLetterFail));
    //}
    //private void StartScaleArrayLetterGood(GameObject[] _arrLetterGood)
    //{
    //    StartCoroutine(ScaleArrayLetterGoodWithCoroutine(_arrLetterGood));
    //}

    //private IEnumerator ScaleArrayLetterFailWithCoroutine(GameObject[] _arrLetterFail)
    //{
    //    int i = 0;
    //    int j = _arrLetterFail.Length / 2;
    //    while (true)
    //    {
    //        if (i >= _arrLetterFail.Length / 2 || j >= _arrLetterFail.Length)
    //        {
    //            yield break;
    //        }
    //        _arrLetterFail[i].transform.DOScale(Vector3.zero, m_timeScaleArrayLetter).SetEase(m_easeScaleArrayLetter).From();
    //        _arrLetterFail[j].transform.DOScale(Vector3.zero, m_timeScaleArrayLetter).SetEase(m_easeScaleArrayLetter).From();
    //        i++;
    //        j++;
    //        yield return new WaitForSeconds(m_timeWaitCoroutine);
    //    }
    //}

    //private IEnumerator ScaleArrayLetterGoodWithCoroutine(GameObject[] _arrayLetterGood)
    //{
    //    for(int i=0;i<_arrayLetterGood.Length;i++)
    //    {
    //        _arrayLetterGood[i].transform.DOScale(Vector3.zero, m_timeScaleArrayLetter).SetEase(m_easeScaleArrayLetter).From();
    //        yield return new WaitForSeconds(m_timeWaitCoroutine);
    //    }
    //}
    #endregion

    #region AVAIRABLE
  
    //[Header("score general")]
    //public float m_timeMoveScore = 0.5f;
    //public Ease m_easeMoveScore = Ease.OutCirc;
    //public float m_distanceMove = 2f;
    //private Vector2 m_screenPosition = new Vector2();
    //public Vector2 ScreenPosition
    //{
    //    get { return m_screenPosition; }
    //    set { m_screenPosition = value; }
    //}
    //[Space(5)]
    //[Header("Single Score")]
    //[SerializeField]
    //private Transform m_trfScoreWordParent;
    //[SerializeField]
    //private Transform m_trfScoreUser;
    //[SerializeField]
    //private Vector3 m_positionMoveToScoreWord = Vector3.zero;
    //[SerializeField]
    //private Vector3 m_positionMoveToScoreUser = Vector3.zero;

    //// position start
    //private Vector3 m_positionStartMoveToScoreWord = Vector3.zero;
    //private Vector3 m_positionStartMoveToScoreUser = Vector3.zero;
    //[Space(5)]
    //[Header("Multi Score")]
    //public Transform m_trfScoreRed;
    //public Transform m_trfScoreBlue;
    //private Vector3 m_positionMoveToScoreRed;
    //private Vector3 m_positionMoveToScoreBlue;

    //private Vector3 m_positionStartMoveToScoreRed = Vector3.zero;
    //private Vector3 m_positionStartMoveToScoreBlue = Vector3.zero;
    //[Space(5)]
    //[Header("Letter Prefabs")]
    //public float m_timeMoveUpLetterPrefab = 0.5f;
    //public float m_timeMoveDownLetterPrefab = 0.65f;
    //public float m_timeWaitMoveDown = 0.75f;
    //public float m_valueScaleLetterPrefab = 3.0f;

    //public Ease m_easeTypeMoveUpLetterPrefab = Ease.OutCirc;
    //public Ease m_easeTypeMoveDownLetterPrefab = Ease.OutCirc;
    //public Ease m_easeTypeScaleLetterPrefab = Ease.OutCirc;

    //[Space(10)]
    //[Header("Letter Fail & Good in Scene")]
    //public float m_timeScaleArrayLetter = 0.35f;
    //public float m_timeWaitCoroutine = 0.1f;
    //public Ease m_easeScaleArrayLetter = Ease.OutBack;
    //[Space(5)]
    //[Header("Scale List Letter Tracked")]
    //[SerializeField]
    //private float m_timeScaleLetterTracked = 0.25f;
    //[SerializeField]
    //private Ease m_easeScaleLetterTracked = Ease.Flash;
    //[SerializeField]
    //private Vector3 m_valueScaleToLetterTracked = Vector3.one * 2;
    //[Header("Main Image")]
    //[SerializeField]
    //private float m_timeAlphaToMainImage = 1.0f;
    //[SerializeField]
    //private Ease m_easeAlphaToMainImage = Ease.Linear;
    //[SerializeField]
    //private SpriteRenderer m_sprMainImage;
    //[Header("Pop up")]
    //[SerializeField]
    //private float m_timeScalePopup = 0.75f;
    //[SerializeField]
    //private Ease m_easeScalePopup = Ease.OutBack;
    #endregion
}
