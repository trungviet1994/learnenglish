using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public enum ButtonType
{
    SQUARE = 0,
    CRICLE = 1,
    NONE = 2
}

public class BaseClickButton : MonoBehaviour {
    protected Transform m_myTranform;
	void Start()
    {
        m_myTranform = transform;
        GetComponent<Button>().onClick.AddListener(OnClicked);
        InitStart();
    }
    public virtual void InitStart()
    { }
    public virtual void OnClicked()
    {
        AudioManager.Instance.PlayOneShotByTypeName(eAudioName.TOUCH);
    }
}
