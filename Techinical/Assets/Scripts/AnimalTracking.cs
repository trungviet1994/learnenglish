using UnityEngine;
using System.Collections;
using Vuforia;
public class AnimalTracking : DefaultTrackableEventHandler {

    public GameObject m_child;
    void Awake()
    {
        m_child = transform.GetChild(0).gameObject;
        m_child.SetActive(false);
    }

    protected override void OnTrackingFound()
    {
        m_child.SetActive(true);
        base.OnTrackingFound();
    }
    protected override void OnTrackingLost()
    {
        m_child.SetActive(false);
        base.OnTrackingLost();
    }
}
