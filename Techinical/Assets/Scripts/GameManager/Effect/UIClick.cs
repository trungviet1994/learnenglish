using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIClick : MonoBehaviour {
    [SerializeField]
    private float m_timeScale = 0.25f;
    private Ease m_easeScale = Ease.Linear;
	void OnEnable()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.zero,m_timeScale).SetEase(m_easeScale).From().OnComplete(DespawnThis);
    }
    public void DespawnThis()
    {
        gameObject.SetActive(false);
        ManagerObject.Instance.DespawnObject(this.gameObject,ePoolName.pool);
    }
}
