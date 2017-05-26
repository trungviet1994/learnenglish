using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class MyAudio
{
    public eAudioName m_audioName;
    public AudioClip m_clipAudio;
}

public class AudioManager : MonoSingleton<AudioManager> {
    public AudioSource m_audioSource;
    public AudioSource m_audioSourceOfData;
    public AudioSource m_audioBG;
    private Dictionary<eAudioName, AudioClip> m_dictionaryAudio = new Dictionary<eAudioName, AudioClip>();

    public MyAudio[] m_arrayMyAudio;
    public GameObject m_speakerAnimation;
    //public Text m_txtMusicPercent;
    //public Text m_txtSoundPercent;

    public AudioSource m_audioBGPlayGame;
    public AudioSource m_audioBGAnimal;
    void Awake()
    {
        for(int i=0;i<m_arrayMyAudio.Length;i++)
        {
            m_dictionaryAudio.Add(m_arrayMyAudio[i].m_audioName, m_arrayMyAudio[i].m_clipAudio);
        }
    }
    void Start()
    {
        InitStart();
    }

    public void PlayAudioBackgroundWithPlayGame(ModePlay _modePlay)
    {

        if(_modePlay == ModePlay.PLAY_GAME)
        {
            m_audioBGPlayGame.Play();
            m_audioBGAnimal.Stop();
        }
        else
        {
            m_audioBGPlayGame.Stop();
            m_audioBGAnimal.Play();
        }
    }
    private AudioClip GetAudioClip(eAudioName _audioName)
    {
        if(m_dictionaryAudio.ContainsKey(_audioName))
        {
            return m_dictionaryAudio[_audioName];
        }
        return null;
    }
    public void PlayAudioBackground()
    {
        if (!m_audioBG.isPlaying)
        {
            m_audioBG.Play();
        }
    }
    public void StopAudioBackground()
    {
        m_audioBG.Stop();
    }

    public void PlayLetterWithAnswer(string _letter,bool _isGoodAnswer = true)
    {
        AudioClip clip = PlayAudioByLetter(_letter);
        if(clip == null)
        {
            return;
        }
        if(_isGoodAnswer)
        {
            Invoke("PlayAudioWithGoodAnswer", clip.length + 0.15f);
        }
        else
        {
            Invoke("PlayAudioWithFailAnswer", clip.length + 0.15f);
        }
    }
    private void PlayAudioWithGoodAnswer()
    {
        PlayOneShotByTypeName(eAudioName.GOOD_ANSWER);
    }

    private void PlayAudioWithFailAnswer()
    {
        PlayOneShotByTypeName(eAudioName.FAIL_ANSWER);
    }

    #region PLAY_AUDIO_EFFECT
    public void PlayAudioByTypeName(eAudioName _audioName)
    {
        AudioClip audioClip = GetAudioClip(_audioName);
        if (m_audioSource && audioClip)
        {
            m_audioSource.Stop();
            m_audioSource.clip = audioClip;
            m_audioSource.Play();
        }
    }

    public void PlayOneShotByTypeName(eAudioName _audioName)
    {
        AudioClip audioClip = GetAudioClip(_audioName);
        if (m_audioSource && audioClip)
        {
            m_audioSource.PlayOneShot(audioClip);
        }
    }

    public void PauseAudio()
    {
        if(m_audioSource.isPlaying)
        {
            m_audioSource.Pause();
        }
    }

    public void UnPauseAudio()
    {
        m_audioSource.UnPause();
    }
    #endregion.............................................
    #region PLAY AUDIO FROM RESOURCES
    // play audio from resources
	public void PlayAudioByName(string _name)
    {
        if (DataManager.instance && m_audioSourceOfData)
        {
            AudioClip audioClip = DataManager.instance.GetAudioByName(_name);
            m_speakerAnimation.SetActive(true);
            if (audioClip != null)
            {
                m_audioSourceOfData.PlayOneShot(audioClip);
                Invoke("DisableSpeaker", audioClip.length);
            }
            else
            {
                m_speakerAnimation.SetActive(false);
            }
        }
    }

    public void DisableSpeaker()
    {
        m_speakerAnimation.SetActive(false);
    }
    public AudioClip PlayAudioByLetter(string _letter)
    {
        if (DataManager.instance && m_audioSourceOfData)
        {
            AudioClip audioClip = DataManager.instance.GetAudioByLetter(_letter);
            m_speakerAnimation.SetActive(true);
            if (audioClip !=null)
            {
                m_audioSourceOfData.PlayOneShot(audioClip);
                Invoke("DisableSpeaker", audioClip.length);
                return audioClip;
            }
            else
            {
                m_speakerAnimation.SetActive(false);
            }
        }
        return null;
    }
    #endregion
    #region SETTING VOLUME

    private string m_strMusic = "music";
    private string m_strAudio = "audio";

    public Slider m_sldMusic;
    public Slider m_sldAudio;

    public Image m_imgIconSound;
    public Image m_imgIconMusic;

    private const float m_defaultAudio = 0.75f;
    private const float m_defaultMusic = 0.75f;

    private void InitStart()
    {
        m_sldAudio.value = m_defaultAudio;
        m_sldMusic.value = m_defaultMusic;

        SettingSoundEffect(m_sldAudio);
        SettingMusicBackground(m_sldMusic);

    }

    private void SetIconSoundMute(bool _isSoundMute )
    {
        if(_isSoundMute)
        {
            m_imgIconSound.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Sound_mute);
        }
        else
        {
            m_imgIconSound.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Sound);
        }
    }
    private void SetIconMusicMute(bool _isMusicMute)
    {
        if (_isMusicMute)
        {
            m_imgIconMusic.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Music_mute);
        }
        else
        {
            m_imgIconMusic.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Music);
        }
    }

    public void SettingSoundEffect(Slider _sldAudio)
    {
        float value = _sldAudio.value;
        m_audioSource.volume = value;
        m_audioSourceOfData.volume = value;
        //m_txtSoundPercent.text = (value*100).ToString("0")+"%";
        if(value <=0)
        {
            SetIconSoundMute(true);
        }
        else
        {
            SetIconSoundMute(false);
        }
        PlayerPrefs.SetFloat(m_strAudio,value);
    }
    public void SettingMusicBackground(Slider _sldMusic)
    {
        float value = _sldMusic.value;
        m_audioBG.volume = _sldMusic.value;
        //m_txtMusicPercent.text =(value*100).ToString("0")+"%";
        if(value<=0)
        {
            SetIconMusicMute(true);
        }
        else
        {
            SetIconMusicMute(false);
        }
        PlayerPrefs.SetFloat(m_strMusic, value);
    }

    #endregion

}
