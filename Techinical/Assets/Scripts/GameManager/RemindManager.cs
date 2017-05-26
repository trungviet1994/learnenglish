using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class RemindManager : MonoBehaviour {
    //public Transform m_trfArm;

    //private Vector3 m_positionArmStart;
    //private const float m_distanceMove = 1.5f;
    //private float m_timeMove = 1.35f;
    //private Vector3 m_postionLetterStart;

    //public Transform m_trfLetter;
    //// Use this for initialization
    //void Awake()
    //{
    //    m_positionArmStart = m_trfArm.position;
    //    m_postionLetterStart = m_trfLetter.position;
    //}
    //void OnEnable()
    //{
    //    SetupStartMove();
    //}
    //public void SetupStartMove()
    //{
    //    m_trfLetter.position = m_postionLetterStart;
    //    m_trfLetter.localScale = Vector3.zero;
    //    m_trfArm.position = m_positionArmStart;
    //    DOTween.ToAlpha(() => m_trfArm.GetComponent<Image>().color, x => m_trfArm.GetComponent<Image>().color = x, 1, 0.25f);
    //    m_trfArm.DOMoveY(m_positionArmStart.y + m_distanceMove, m_timeMove).OnStepComplete(MoveLetterBegin).SetDelay(0.75f);
       
    //}
     
    //public void MoveLetterBegin()
    //{
    //    m_trfLetter.position = m_postionLetterStart;
    //    Sequence sequence = DOTween.Sequence();
    //    sequence.Append(m_trfLetter.DOScale(Vector3.one * 1.35f, 0.5f));
    //    sequence.Join(m_trfLetter.DOMoveY(m_postionLetterStart.y + 1.25f, 0.5f));
    //    sequence.InsertCallback(1.0f, HideArm);
    //}

    //private void HideArm()
    //{
    //    DOTween.ToAlpha(() => m_trfArm.GetComponent<Image>().color, x => m_trfArm.GetComponent<Image>().color = x, 0, 0.5f)
    //        .OnComplete(SetupStartMove);
    //}

    //void OnDisable()
    //{
    //    m_trfArm.DOKill(true);
    //    m_trfLetter.DOKill();
    //}
}
