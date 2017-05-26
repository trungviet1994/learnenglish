using UnityEngine;
using System.Collections;
using DG.Tweening;
public class UserModeManager : MonoBehaviour {
    //public RectTransform m_trfTitle;
    private Vector3 m_positionStartTitle;
    private Vector3 m_positionToTitle;

    void Awake()
    {
        //m_positionStartTitle = m_trfTitle.position;
        //m_positionToTitle = new Vector3(m_positionStartTitle.x, m_positionStartTitle.y + 2.0f, m_positionStartTitle.z);
    }
    void OnEnable()
    {
        StartUpEffect();
    }

    public void StartUpEffect()
    {
        //m_trfTitle.position = m_positionToTitle;
        //m_trfTitle.DOMoveY(m_positionStartTitle.y, 0.7f).SetEase(Ease.OutBounce);
    }
}
