//using UnityEngine;
//using System.Collections;
//using DG.Tweening;

//public class EffectConfig : MonoSingleton<EffectConfig>{
//    [Header("Move Up")]
//    public float m_timeMoveUp = 0.5f;
//    public float m_timeMoveDown = 0.6f;
//    public float m_timeWaitMoveDown = 0.75f;
//    [Space(5)]
//    public Ease m_easeTypeMoveUp = Ease.OutCubic;
//    public Ease m_easeTypeMoveDown = Ease.OutCirc;
//    [Space(5)]
//    public Ease m_easeScaleLetterTracked = Ease.OutCirc;

//    public float m_timeScaleArray = 0.25f;
//    public float m_timeDelayScaleArray = 0.1f;
//    public Ease m_easeScaleArray = Ease.OutBack;

//    public float m_timeScaleTwoArray = 0.5f;
//    public float m_timeDelayScaleTwoArray = 0.25f;
//    public Ease m_easeScaleTwoArray = Ease.OutBack;

//    //scale score word
//    public Vector3 m_valueScaleScoreWord = Vector3.zero;
//    public float m_timeScaleScaleWord = 1.0f;
//    public Ease m_easeScaleScoreWord = Ease.OutBounce;
//    // color to background
//    public float m_valueColorBackground = 0.0f;
//    public float m_timeColorBackground = 1.25f;

//    [Header("Single Score")]
//    public Transform m_trfScoreWordParent;
//    public Transform m_trfScoreUser;

//    public float m_timeMoveScoreSingle = 0.5f;
//    public Vector3 m_positionMoveToScoreWord;
//    public Vector3 m_positionMoveToScoreUser;
//    public Ease m_easeMoveScoreSingle = Ease.OutCirc;

//    void Awake()
//    {
//        m_positionMoveToScoreWord = m_trfScoreWordParent.position;
//        m_positionMoveToScoreUser = m_trfScoreUser.position;
//    }
//}
