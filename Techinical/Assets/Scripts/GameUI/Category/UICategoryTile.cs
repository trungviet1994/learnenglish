using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.UI;

public class UICategoryTile : MonoBehaviour
{
    public CategoryObject m_category;
    [Space(5)]
    public Text m_categoryName;
    public Text m_score;
    public Image m_lock;
    public Image m_imgBarScore;
    public Image m_categoryImage;
    public Button m_btnImage;

    public static int SCORE1 = 50;
    public static int SCORE2 = 80;

    //public void OnClick()
    //{
    //    if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
    //    {
    //        ChooseCategory();
    //        ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
    //        GameController.Instance.StartGame();
    //        GamePlayConfig.Instance.GameStart = true;
    //    }
    //    else
    //    {
    //        ChooseCategory();
    //    }
    //}
    public int GetScore()
    {
        if(m_category !=null)
        {
            return m_category.m_score;
        }
        else
        {
            return 0;
        }
    }

    public void SetCategoryInfo(CategoryObject _categoryObject)
    {
        m_category = _categoryObject;
        m_category.m_unlocked = true;
        if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            if (m_category != null)
            {
                m_lock.enabled = true;
                m_categoryName.text = m_category.m_category.ToUpper();
                m_lock.sprite = m_category.m_unlocked ? SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.UnlockedSprite) : SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Lock);
                m_imgBarScore.gameObject.SetActive(true);
                if (m_category.m_score < SCORE1)
                {
                    m_imgBarScore.color = Color.red;
                    //m_imgBarScore.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Fail);
                }
                else if (m_category.m_score >= SCORE2)
                {
                    m_imgBarScore.color = Color.green;
                    //m_imgBarScore.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Pass);
                }
                else
                {
                    m_imgBarScore.color = Color.yellow;
                   // m_imgBarScore.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Bar_Yellow);
                }

                if (m_category.m_unlocked)
                {
                    m_score.text = m_category.m_score.ToString();
                }
                else
                {
                    m_score.text = "";
                    m_imgBarScore.color = Color.blue;
                    //m_imgBarScore.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.New);
                }
                m_categoryImage.sprite = DataManager.instance.GetCategoryImage(m_category);
                m_btnImage.enabled = (m_category.m_unlocked);
            }
        }
        else
        {
            if (m_category != null)
            {
                m_categoryName.text = m_category.m_category.ToUpper();
                m_imgBarScore.gameObject.SetActive(false);
                //m_lock.enabled = false;
                m_categoryImage.sprite = DataManager.instance.GetCategoryImage(m_category);
                //m_score.text = "";
                //m_score.gameObject.SetActive(false);
                //m_imgBarScore.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.Pass);
                m_lock.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.UnCheckSprite);
                m_btnImage.enabled = (true);
            }
        }
    }
    // click choise category
    public void ChooseCategory()
    {
        if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            DataManager.instance.SetCategory(m_category);
        }
        else
        {
            bool isActive = !DataManager.instance.m_choosenCategories.Contains(m_category);
            SetSpriteLock(isActive);
            if (!DataManager.instance.m_choosenCategories.Contains(m_category))
            {
                DataManager.instance.m_choosenCategories.Add(m_category);
            }
            else
            {
                DataManager.instance.m_choosenCategories.Remove(m_category);
            }

            if (DataManager.instance.m_choosenCategories.Count > 0)
            {
                BtnStartControl.Instance.ActiveButton();
            }
            else
            {
                BtnStartControl.Instance.DeactiveButton();
            }
        }
    }

    private void SetSpriteLock(bool isLock = true)
    {
        if(isLock)
        {
            m_lock.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.UnlockedSprite);
        }
        else
        {
            m_lock.sprite = SpriteManager.Instance.GetSpriteByTypeName(eSpriteName.UnCheckSprite);
        }
    }
}
