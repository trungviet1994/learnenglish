using UnityEngine;
using System.Collections;

public class GameConfig : MonoSingleton<GameConfig> {
    // const variable
    public const string PATH_IMAGE = "Texture/";
    public const string SPLIT_LETTER = " ";
    public const int MAX_LENGHT_WORD = 11;
    public const float TIME_DELAY_START_PLAY = 1.25f;
}
//enum variable
public enum eBaseTeamType
{
    TEAM_RED = 1,
    TEAM_BLUE = 2,
    NONE = 0
}
public enum eModeLevel
{
    LEARN_LETTER = 0,
    EASY = 1,
    NORMAL = 2,
    HARD = 3
}

public enum eUserPlayMode
{
    SINGLE_PLAY = 0,
    MULTI_PLAY = 1,
    OPTION = 2,
    //LEARN_LETTER = 3
}

public enum eLanguageMode
{
    ENGLISH = 0,
    VIETNAMESE = 1
}

public enum eSceneName
{
    Language = 0,
    Level = 1,
    Category = 2,
    MainScene = 3,
    UserPlayMode = 4,
    GetInABC = 5
}
public enum QuestionType
{
    QS_IMAGE,
    QS_SOUND
}

public enum eAudioName
{
    TOUCH = 0,
    GOOD_ANSWER = 1,
    FAIL_ANSWER = 2,
    FINISH_WORD = 3,
    POPUP_CONGRATOLATION = 4,
    HELP_SOUND = 5,
    TICK = 6,
    AUTO_OPEN_LETTER = 7,

    APPEAR = 8
}
