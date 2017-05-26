using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PopupCategory : MonoBehaviour {
    public Text m_txtContent;// score

    private bool m_isOk = true; // ok or continue
    [Header("effect Pop up")]
    private float m_timeScalePopup = 0.35f;
    public Transform m_parentScale;
    private Ease m_easeScalePopup = Ease.OutBack;

    void OnEnable()
    {
        EffectPopupScreen();
        AudioManager.Instance.PlayAudioByTypeName(eAudioName.HELP_SOUND);
        ManagerObject.Instance.SpawnObjectByType(eObjectType.par_cogratulation,ePoolName.ObjectPool).transform.position = Vector3.zero;
    }
    public void EffectPopupScreen()
    {
        m_parentScale.transform.localScale = Vector3.one * 0.25f;
        m_parentScale.DOScale(Vector3.one, m_timeScalePopup).SetEase(m_easeScalePopup);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_isOk">Choi tiep</param>
    /// <param name="_score"></param>
    public void SetUpShow(bool _isOk = true,int _score = 0)
    {
        m_isOk = _isOk;
        //IsShowSingleScore(true);
        AudioManager.Instance.PlayAudioByTypeName(eAudioName.POPUP_CONGRATOLATION);
        m_txtContent.text = _score.ToString();
    }

    // show popup with multi player
    public void SetUpShow(int _scoreRed,int _scoreBlue)
    {
        AudioManager.Instance.PlayAudioByTypeName(eAudioName.POPUP_CONGRATOLATION);
        //IsShowSingleScore(false);
        //if(m_txtscoreRed)
        //{
        //    m_txtscoreRed.text = _scoreRed.ToString();
        //}
        //if (m_txtscoreBlue)
        //{
        //    m_txtscoreBlue.text = _scoreBlue.ToString();
        //}
    }

    private void IsShowSingleScore(bool _isSingle = true)
    {
        //m_singleScore.SetActive(_isSingle);
        //m_multiScore.SetActive(!_isSingle);
    }
    public void Click()
    {
        if(m_isOk)
        {
            ScreenManager.Instance.HideCurrentPopup();
            ScreenManager.Instance.ShowScreenPrev();
            // set lai thong tin cua category
            GameController.Instance.m_categoryManager.SetUpCategory();
        }
        else // click continue
        {
            GameController.Instance.PlayWithNewWord();
            ScreenManager.Instance.HideCurrentPopup();
        }
    }

}
