using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Option : MonoBehaviour {
    public Transform m_topParent;
    public Transform m_bottomParent;

    [Header("Effect...")]
    [SerializeField]
    private float m_timeMoveDown = 0.5f;
    [SerializeField]
    private float m_timeMoveUp = 0.75f;
    [SerializeField]
    private Ease m_easeTypeMove = Ease.InElastic;

    private Vector3 m_startTopPosition;
    private Vector3 m_startBottomPosition;

    private Vector3 m_positionSetBottom;
    private Vector3 m_positionSetTop;

    [SerializeField]
    private float m_distanceTop = 2.5f;
    [SerializeField]
    private float m_distanceBottom = 8.5f;
    void Awake()
    {
        // initiate position
        m_startTopPosition = m_topParent.position;
        m_startBottomPosition = m_bottomParent.position;

        m_positionSetBottom = new Vector3(0, m_startBottomPosition.y - m_distanceBottom);
        //m_bottomParent.position = m_positionSetBottom;
        m_positionSetTop = new Vector3(0, m_startTopPosition.y + m_distanceTop);
       // m_topParent.position = m_positionSetTop;
    }
    [ContextMenu("test")]
    void OnEnable()
    {
        TopMoveDown();
        DownMoveTop();
    }

    // move down top
    private void TopMoveDown()
    {
        m_topParent.DOMoveY(m_positionSetTop.y,m_timeMoveDown).From().SetEase(m_easeTypeMove);
    }
    private void DownMoveTop()
    {
        m_bottomParent.DOMoveY(m_positionSetBottom.y, m_timeMoveUp).From().SetEase(m_easeTypeMove);
    }
    [ContextMenu("test close")]
    public void Close()
    {
        m_topParent.DOMoveY(m_positionSetTop.y, m_timeMoveDown+0.25f).SetEase(m_easeTypeMove);
        m_bottomParent.DOMoveY(m_positionSetBottom.y, m_timeMoveUp+0.25f).SetEase(m_easeTypeMove);
    }
}
