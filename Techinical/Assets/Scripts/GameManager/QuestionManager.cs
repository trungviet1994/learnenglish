using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class QuestionManager : MonoSingleton<QuestionManager>
{
    private const float m_timeDelaySpeakQuestion = 5.0f;
    private int m_CurrentQuestion = 0;

    public Image m_spriteImageBG;
    public bool ActiveSpriteMainImage
    {
        set { 
            if(m_spriteImageBG)
            {
                m_spriteImageBG.enabled = value;
            }
        }
    }
    private List<WordObject> m_listWordObject = new List<WordObject>();
    [SerializeField]
    private string m_strCurrentQuestion = "";
    private string m_strImageCurrent = "";
    private bool m_isStartCoroutineSpeakQuestion = false;

    public GameObject m_learnLetterInstance;
    public Text m_txtQuestionForLearnLetter;
    public Image m_imgQuestionForLearnLetter;

    public List<WordObject> ListWordObject
    {
        get { return m_listWordObject; }
        set { m_listWordObject = value; }
    }
    public string StrCurrentQuestion
    {
        get { return m_strCurrentQuestion; }
        set { m_strCurrentQuestion = value; }
    }

    private void SetUpShowLearnLetterQuestion(bool isShowImage = true)
    {
        m_imgQuestionForLearnLetter.enabled = !isShowImage;
        m_txtQuestionForLearnLetter.enabled = isShowImage;
    }
    public void SetUpWithModeLevel()
    {
        m_learnLetterInstance.SetActive(false);
        //ActiveSpriteMainImage = true;
        switch (GamePlayConfig.Instance.ModeLevel)
        {
            case eModeLevel.LEARN_LETTER:
                m_learnLetterInstance.SetActive(true);
                if (GamePlayConfig.Instance.TypeShowQuestion == QuestionType.QS_IMAGE)
                {
                    SetUpShowLearnLetterQuestion(true);
                }
                else
                {
                    SetUpShowLearnLetterQuestion(false);
                }
                break;
            default:
                break;
        }
    }
    // get list question from data
    // return number question 
    public int GetListQuestion()
    {
        m_CurrentQuestion = 0;
        if (DataManager.instance)
        {
            m_listWordObject = DataManager.instance.m_listWord;
        }
        else
        {
            Debug.Log("Khong co data roi !");
        }
        return m_listWordObject.Count;
    }

    // Lay cau hoi tu 1 list cau hoi da xao tron
    // tra ve : key word
    public string GetQuestion()
    {
        m_strCurrentQuestion = "No Question";
        if(m_CurrentQuestion >= ListWordObject.Count)
        {
            m_CurrentQuestion = 0;
        }
        if (m_listWordObject.Count > 0) 
        {
            m_strCurrentQuestion = m_listWordObject[m_CurrentQuestion].m_word;
            m_strImageCurrent = m_listWordObject[m_CurrentQuestion].m_image;
        }
        if(m_strCurrentQuestion.Length > 11)
        {
            GameController.Instance.DoNextQuestion(); 
        }
        return m_strCurrentQuestion;
    }
    // remove question khi da complete this
    public void RemoveCurrentQuestion()
    {
        if(m_listWordObject.Count <= 0)
        {
            return;
        }
        m_listWordObject.Remove(m_listWordObject[m_CurrentQuestion]);
    }

    public void NextQuestion()
    {
        RemoveCurrentQuestion();
        m_CurrentQuestion++;
    }

    // check next question ,if list word < 0-> finish package
    public bool CheckFinisCategory()
    {
        if (m_listWordObject.Count <= 0)
        {
            // unlock new package 
            return true;
        }
        else
        {
            return false;
        }
    }

    // Hien thi hinh anh cua cau hoi
    public void ShowOfQuestion()
    {
        if (GamePlayConfig.Instance.ModeLevel == eModeLevel.LEARN_LETTER)
        {
            m_txtQuestionForLearnLetter.text = m_strCurrentQuestion.ToUpper();
            Color newColor = m_txtQuestionForLearnLetter.color;
            newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.2f, 1.0f), 1.0f);
            m_txtQuestionForLearnLetter.color = newColor;

            Sprite sprite = DataManager.instance.GetImageFromLetterHead(m_strImageCurrent);
            m_spriteImageBG.sprite = sprite;
        }
        else
        {
            Sprite sprite = DataManager.instance.GetImageByName(m_strImageCurrent);
            m_spriteImageBG.sprite = sprite;
        }
        if(GamePlayConfig.Instance.TypeShowQuestion == QuestionType.QS_SOUND)
        {
            StartCoroutine("SpeakQuestion");
            SetRespeakQuestion();
        }
    }

    #region SOUND_QUESTION
    // speak question 
    private IEnumerator SpeakQuestion()
    {
        yield return new WaitForSeconds(GameConfig.TIME_DELAY_START_PLAY - 0.1f);
        AudioManager.Instance.PlayAudioByLetter(m_strCurrentQuestion);
        yield return 0;
    }

    private IEnumerator SpeakQuestionLoop()
    {
        m_isStartCoroutineSpeakQuestion = true;
        while (true)
        {
            yield return new WaitForSeconds(m_timeDelaySpeakQuestion);
            AudioManager.Instance.PlayAudioByLetter(m_strCurrentQuestion);
        }
    }
    public void SetRespeakQuestion()
    {
        if (m_isStartCoroutineSpeakQuestion)
        {
            StopCoroutine("SpeakQuestionLoop");
            m_isStartCoroutineSpeakQuestion = false;
            SetRespeakQuestion();
        }
        else
        {
            StartCoroutine("SpeakQuestionLoop");
        }
    }

    public void SetStopSpeakQuestion()
    {
        m_isStartCoroutineSpeakQuestion = false;
        StopCoroutine("SpeakQuestionLoop");
    }

    #endregion
    #region EFFECT...
    [Header("Effect Main Image...")]
    [SerializeField]
    private float m_timeHideMainImage = 1.0f;
    [SerializeField]
    private float m_timeShowMainImage = 1.0f;

    public void DoHideMainImage()
    {
        DOTween.ToAlpha(() => m_spriteImageBG.color, x => m_spriteImageBG.color = x, 0, m_timeHideMainImage)
            .OnComplete(CallBackWithCreateNewQuestion);
    }

    public void DoShowMainImage()
    {
        Debug.Log("show main ");
        DOTween.ToAlpha(() => m_spriteImageBG.color, x => m_spriteImageBG.color = x, 1.0f, m_timeShowMainImage)
            .OnComplete(CallBackCompleteShowImage);
    }
  
    private void CallBackCompleteShowImage()
    {
        GameController.Instance.EnableTracking();
    }
    private void CallBackWithCreateNewQuestion()
    {
        GameController.Instance.CreateNewQuestion();
    }
    #endregion
}
