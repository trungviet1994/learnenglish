//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using DG.Tweening;

//public enum eMoveToDirection
//{
//    LEFT_TO_RIGHT = 0,
//    RIGHT_TO_LEFT = 1
//}
//public class EffectManager : MonoSingleton<EffectManager> {
//    [SerializeField]
//    private bool isActive = false;

//    #region MOVE_UP_AND_MOVE_DOWN
//    public void DoMoveUp(Transform _tranformTarget,Vector3 _fromValues,Vector3 _toValues)
//    {
//        if(!isActive)
//        {
//            return;
//        }
//        _tranformTarget.position = _fromValues;
//        Sequence mySequence = DOTween.Sequence();
//        mySequence.Append(_tranformTarget.DOScale(Vector3.one * 2.5f, EffectConfig.Instance.m_timeMoveUp).SetEase(EffectConfig.Instance.m_easeTypeMoveUp));
//        Tween myTween = _tranformTarget.transform.DOMove(Vector3.zero, EffectConfig.Instance.m_timeMoveUp).SetEase(EffectConfig.Instance.m_easeTypeMoveUp);
//        mySequence.Join(myTween);
//        mySequence.InsertCallback(EffectConfig.Instance.m_timeWaitMoveDown, () => DoMoveDown(_tranformTarget, _toValues));
//    }

//    public void DoMoveDown(Transform _tranformTarget,Vector3 _toValue)
//    {
//        if (!isActive)
//        {
//            return;
//        }
//        _tranformTarget.DOMove(_toValue, EffectConfig.Instance.m_timeMoveDown).SetEase(EffectConfig.Instance.m_easeTypeMoveDown);
//        _tranformTarget.DOScale(Vector3.one, EffectConfig.Instance.m_timeMoveDown).SetEase(EffectConfig.Instance.m_easeTypeMoveDown);
//    }

//    #endregion 

//    #region SCALE_WITH_ARRAY_&_LIST_CALL_COROUTINE
//    // scale array object
//    public void StartDoScaleArray(GameObject[] _arrayGameObject)
//    {
//        if (!isActive)
//        {
//            return;
//        }
//        StartCoroutine(DoScaleArray(_arrayGameObject));
//    }

//    // scale list object
//    public void StartDoScaleList(List<GameObject> _listGO)
//    {
//        if (!isActive)
//        {
//            return;
//        }
//        StartCoroutine(DoScaleList(_listGO));
//    }
//    // scale array object 2 hang
//    public void StartDoScaleArrayDivideTwo(GameObject[] _arrayGO)
//    {
//        if (!isActive)
//        {
//            return;
//        }
//        StartCoroutine(DoScaleArrayDivideTwo(_arrayGO));
//    }

//    #endregion

//    #region DO_THIS_WITH_ENUMRATOR
//    public IEnumerator DoScaleArrayDivideTwo(GameObject[] _arrayGO)
//    {
//        int i = 0;
//        int j = _arrayGO.Length / 2;
//        DoScaleToZeroArrayGO(_arrayGO);
//        while(true)
//        {
//            if(i >= _arrayGO.Length/2||j>=_arrayGO.Length)
//            {
//                yield break;
//            }
//            _arrayGO[i].transform.DOScale(Vector3.one, EffectConfig.Instance.m_timeScaleTwoArray).SetEase(EffectConfig.Instance.m_easeScaleTwoArray);
//            _arrayGO[j].transform.DOScale(Vector3.one, EffectConfig.Instance.m_timeScaleTwoArray).SetEase(EffectConfig.Instance.m_easeScaleTwoArray);
//            i++;
//            j++;
//            yield return new WaitForSeconds(EffectConfig.Instance.m_timeDelayScaleTwoArray);
//        }
//    }

//    private IEnumerator DoScaleArray(GameObject[] _arrayGameObject)
//    {
//        int i = 0;
//        if(_arrayGameObject == null)
//        {
//            yield break;
//        }
//        else
//        {
//            DoScaleToZeroArrayGO(_arrayGameObject);
//        }
//        while(true)
//        {
//            if(_arrayGameObject[i] == null)
//            {
//                yield break;
//            }
//            else
//            {
//                _arrayGameObject[i].transform.DOScale(Vector3.one, EffectConfig.Instance.m_timeScaleArray).SetEase(EffectConfig.Instance.m_easeScaleArray);
//                if (++i >= _arrayGameObject.Length)
//                {
//                    yield break;
//                }
//            }
//            yield return new WaitForSeconds(EffectConfig.Instance.m_timeDelayScaleArray);
//        }
//    }

//    private IEnumerator DoScaleList(List<GameObject> _listGO)
//    {
//        int i = 0;
//        if (_listGO.Count <= 0)
//        {
//            yield break;
//        }
//        else
//        {
//            DoScaleToZeroListGO(_listGO);
//        }
//        while (true)
//        {
//            if (i >= _listGO.Count)
//            {
//                yield break;
//            }
//            else
//            {
//                _listGO[i].transform.DOScale(Vector3.one, EffectConfig.Instance.m_timeScaleArray).SetEase(EffectConfig.Instance.m_easeScaleArray);
//                if (++i >= _listGO.Count)
//                {
//                    yield break;
//                }
//            }
//            yield return new WaitForSeconds(EffectConfig.Instance.m_timeDelayScaleArray);
//        }
//    }

//    private void DoScaleToZeroArrayGO(GameObject[] _arrayGO)
//    {
//        if (!isActive)
//        {
//            return;
//        }
//        int i=0;
//        while(true)
//        {
//            if(_arrayGO[i]==null)
//            {
//                break;
//            }
//            else
//            {
//                _arrayGO[i].transform.localScale = Vector3.zero;
//                if(++i >=_arrayGO.Length)
//                {
//                    break;
//                }
//            }
//        }
//    }

//    private void DoScaleToZeroListGO(List<GameObject> _listGO)
//    {
//        if (!isActive)
//        {
//            return;
//        }
//        for (int i = 0; i < _listGO.Count; i++)
//        {
//            _listGO[i].transform.localScale = Vector3.zero;
//        }
//    }

//    #endregion

//    #region EFFECT_ON_OBJECT
//    public void DoMoveTo(Transform _tranform, Vector3 _toValue,float _time)
//    {
//        _tranform.DOMove(_toValue, _time);
//    }

//    // Move object from position to position by direction
//    public void DoMoveTo(Transform _tranform,Vector3 _rootPosition,float _time,float _disMove,eMoveToDirection _direction = eMoveToDirection.LEFT_TO_RIGHT,Ease _ease = Ease.Linear)
//    {
//        Vector3 _fromValue = new Vector3();
//        if(_direction == eMoveToDirection.LEFT_TO_RIGHT)
//        {
//            _fromValue = new Vector3(_rootPosition.x - _disMove, _rootPosition.y);
//        }
//        else
//        {
//            _fromValue = new Vector3(_rootPosition.x + _disMove, _rootPosition.y);
//        }
//        _tranform.DOMove(_fromValue, _time).From().SetEase(_ease);
//    }

//    public void DoScaleScoreWord(Transform _tranform)
//    {
//        _tranform.DOScale(EffectConfig.Instance.m_valueScaleScoreWord,EffectConfig.Instance.m_timeScaleScaleWord).OnComplete(()=>DoScaleToComplete(_tranform));
//    }

//    private void DoScaleToComplete(Transform _tranform)
//    {
//        _tranform.gameObject.SetActive(false);
//    }
    
//    public void DoColorToForSprite(SpriteRenderer _sprite)
//    {
//        DOTween.ToAlpha(() => _sprite.color, x => _sprite.color = x, EffectConfig.Instance.m_valueColorBackground, EffectConfig.Instance.m_timeColorBackground).From();
//    }

//    public void DoScaleGameObject(Transform _tranform, Vector3 _toValue,float _timer,Ease _ease, int _loop)
//    {
//        _tranform.DOScale(_toValue, _timer).SetLoops(_loop,LoopType.Yoyo).SetEase(_ease);
//    }

//    #endregion

//    [ContextMenu("test!")]
//    public void PauseAllEffectInGamePlay()
//    {
//        DOTween.PauseAll();
//    }
//    public void PlayAllEffectInGamePlay()
//    {
//        DOTween.PlayAll();
//    }

//    public void KillAllEffectInGamePlay()
//    {
//        DOTween.KillAll();
//    }
//}
