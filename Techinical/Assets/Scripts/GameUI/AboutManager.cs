using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AboutManager : MonoBehaviour {

    public GameObject m_buttonAbout;
    public GameObject m_buttonHelp;

    public GameObject m_contentAbout;
    public GameObject m_contentHelp;

    public BaseEffectScreen m_effectPopup;
    void OnEnable()
    {
        SetupAbout(true);
    }
	public void ClickAbout()
    {
        SetupAbout(true);
    }

    public void ClickHelp()
    {
        SetupAbout(false);
    }

    private void SetInteractableWithButtonAbout(bool isButtonAbout )
    {
        m_buttonAbout.GetComponent<Button>().interactable = !isButtonAbout;
        m_buttonHelp.GetComponent<Button>().interactable = isButtonAbout;
    }
    private void SetupContentAbout(bool _isAbout)
    {
        m_contentAbout.SetActive(_isAbout);
        m_contentHelp.SetActive(!_isAbout);
    }
    private void SetupAbout(bool _isAbout = true)
    {
        //float sizeButtonAbout = 1;
        //float sizeButtonHelp = 1;
        //if(_isAbout)
        //{
        //    sizeButtonHelp = 0.65f;
        //}
        //else
        //{
        //    sizeButtonAbout = 0.65f;
        //}
        SetupContentAbout(_isAbout);
        SetInteractableWithButtonAbout(_isAbout);
        //m_buttonAbout.transform.localScale = Vector3.one*sizeButtonAbout;
        //m_buttonHelp.transform.localScale = Vector3.one * sizeButtonHelp;
    }

    public void onClickMail()
    {
        Application.OpenURL("mailto:admin@armplay.com");
    }
    public void onClickWebsite()
    {
        Application.OpenURL("http://armplay.com");
    }
    public void onClickFacebook()
    {
        Application.OpenURL("http://www.facebook.com/armplaygames/?fref=ts");
    }
    public void onClickAddress()
    {
        Application.OpenURL("http://www.google.com/maps/place/Khu+C%C3%B4ng+Ngh%E1%BB%87+Ph%E1%BA%A7n+M%E1%BB%81m+-+%C4%90%E1%BA%A1i+H%E1%BB%8Dc+Qu%E1%BB%91c+Gia+TP+H%E1%BB%93+Ch%C3%AD+Minh+(ITP)/@10.8675581,106.7929265,17.5z/data=!4m5!3m4!1s0x3175277b9fa0eca9:0xc1b8a23f27d681b2!8m2!3d10.8676263!4d106.793976");
    }

    public void CloseAbout()
    {
        m_effectPopup.m_myDelegate = CallBack;
        m_effectPopup.CloseWindow();
    }
    public void CallBack()
    {
        ScreenManager.Instance.HideCurrentPopup();
    }
}
