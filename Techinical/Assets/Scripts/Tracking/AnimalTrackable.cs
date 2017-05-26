using UnityEngine;
using System.Collections;
using Vuforia;
using DG.Tweening;

public class AnimalTrackable : DefaultTrackableEventHandler {
    [SerializeField]
    private GameObject m_objectChild;
    private float m_scaleStartValue;
    void Awake()
    {
        m_objectChild = transform.GetChild(0).gameObject;
        m_scaleStartValue = m_objectChild.transform.localScale.x;
        m_objectChild.transform.localScale = Vector3.zero;
        m_objectChild.SetActive(false);
    }
    protected override void OnTrackingFound()
    {
        m_objectChild.SetActive(true);
        m_objectChild.transform.DOScale(Vector3.one*m_scaleStartValue,0.5f).SetEase(Ease.OutBack);
        //AnimalObject animalScripts = m_objectChild.GetComponentInChildren<AnimalObject>();
        //if(animalScripts)
        //{
        //    //animalScripts.Appear(ContentManager.Instance.animalAppearEffectPrefab);
        //}
        //GetComponentInChildren<AnimalObject>().Appear(ContentManager.Instance.animalAppearEffectPrefab);
    }
    protected override void OnTrackingLost()
    {
        m_objectChild.SetActive(false);
    }
}
