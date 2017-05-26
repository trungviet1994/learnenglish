
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITabset : MonoSingleton<UITabset>
{
    //public Slider sound;
    //public Slider music;

    public DropDownMenu ddMenu;
    //public float lastSound;
    //public float lastMusic;
    //public float lastLanguage;

    //void Start()
    //{
    //    //sound.value = PlayerPrefs.GetFloat("sound", 1);
    //    //music.value = PlayerPrefs.GetFloat("music", 1);
    //    //AudioManager.Instance.SettingSoundEffect(sound.value);
    //    //AudioManager.Instance.SettingMusicBackground(music.value);
    //    //AudioManager.Instance.SettingSoundEffect(sound.value);
    //    //sound.onValueChanged.AddListener(delegate
    //    //{
    //    //    AudioManager.Instance.SettingSoundEffect(sound.value);
    //    //});
    //    //music.onValueChanged.AddListener(delegate
    //    //{
    //    //    AudioManager.Instance.SettingMusicBackground(music.value);
    //    //});
    //}

    public void Language()
    {
        ddMenu.dd.value = ddMenu.setLanguage._Language == "English" ? 0 : 1;
    }

    void OnEnable()
    {
        //sound.value = PlayerPrefs.GetFloat("sound", 1);
        //music.value = PlayerPrefs.GetFloat("music", 1);
        ddMenu.dd.value = PlayerPrefs.GetInt("language", 0);
        ddMenu.setLanguage._Language = ddMenu.dd.value == 0 ? "English" : "Vietnamese";
    }

    //public void ChooseTab(int index)
    //{
    //    switch (index)
    //    {
    //        case 1:
    //            m_setting.SetActive(false);
    //            m_about.SetActive(true);
    //            m_howTo.SetActive(false);
    //            setting.enabled = false;
    //            about.enabled = true;
    //            howto.enabled = false;
    //            break;
    //        case 2:
    //            m_setting.SetActive(true);
    //            m_about.SetActive(false);
    //            m_howTo.SetActive(false);
    //            setting.enabled = true;
    //            about.enabled = false;
    //            howto.enabled = false;
    //            break;
    //        case 3:
    //            m_setting.SetActive(false);
    //            m_about.SetActive(false);
    //            m_howTo.SetActive(true);
    //            setting.enabled = false;
    //            about.enabled = false;
    //            howto.enabled = true;
    //            break;
    //    }
    //}

    [ContextMenu("save")]
    public void CloseSetting()
    {
        //PlayerPrefs.SetFloat("sound", sound.value);
        //PlayerPrefs.SetFloat("music", music.value);
        PlayerPrefs.SetInt("language", ddMenu.dd.value);
        //audioManager.m_audioSource.volume = sound.value;
        //audioManager.m_audioBG.volume = music.value;
        ddMenu.ChangeLanguage();
    }

    public void OnDisable()
    {
        CloseSetting();
    }

    public void SetLanguage()
    {
        ddMenu.ChangeLanguage();
    }
}
