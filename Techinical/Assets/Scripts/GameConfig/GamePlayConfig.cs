using UnityEngine;
using System.Collections;

public enum ModePlay
{
    PLAY_GAME ,
    PLAY_ANIMAL
}
public class GamePlayConfig : MonoSingleton<GamePlayConfig> {
    [Header("Config in Game play")]
    [SerializeField]
    private eModeLevel m_modeLevel = eModeLevel.EASY;
    [SerializeField]
    private eUserPlayMode m_userPlayMode = eUserPlayMode.SINGLE_PLAY;
    [SerializeField]
    private bool m_gameStart = false;
    [SerializeField]
    private QuestionType m_typeShowQuestion = QuestionType.QS_IMAGE;

    public QuestionType TypeShowQuestion
    {
        get { return m_typeShowQuestion; }
        set { m_typeShowQuestion = value; }
    }

    public ModePlay m_modePlay = ModePlay.PLAY_GAME;
    public bool GameStart
    {
        get { return m_gameStart; }
        set { m_gameStart = value; }
    }
   
    public eUserPlayMode UserPlayMode
    {
        get { return m_userPlayMode; }
        set { m_userPlayMode = value; }
    }
    public eModeLevel ModeLevel
    {
        get { return m_modeLevel; }
        set { m_modeLevel = value; }
    }
    // with button in level mode
    private const float m_aspectDefault = 0.5625f; // with 9x16
    private Vector2 m_sizeOfContainButton = new Vector2(640,1000);

    private Vector2 m_screenPosition = new Vector2();
    public Vector2 ScreenPosition
    {
        get { return m_screenPosition; }
        set { m_screenPosition = value; }
    }
    public Camera m_cameraMain;
    public Transform m_trfMainImage;

    public RectTransform m_rectrfOfContainButtonLM;
    void Awake()
    {
        if (m_cameraMain)
        {
            m_screenPosition = m_cameraMain.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            //config mainimage
            float valueScaleTo = (m_cameraMain.aspect * m_trfMainImage.localScale.x) / 0.75f;
            m_trfMainImage.localScale = Vector3.one * valueScaleTo;
            //config button mode level
            float newButtonWidthLM = m_sizeOfContainButton.x - (m_cameraMain.aspect - m_aspectDefault)*m_sizeOfContainButton.x +50;
            float newButtonHeightLM = m_sizeOfContainButton.y - (m_cameraMain.aspect - m_aspectDefault) * m_sizeOfContainButton.y;
            m_rectrfOfContainButtonLM.sizeDelta = new Vector2(newButtonWidthLM,newButtonHeightLM);
        }
    }
}
