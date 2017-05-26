using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

public class DataLoader : MonoSingleton<DataLoader>
{
    public const string WORD_PATH = "Data/Word";
    public const string CATEGORY_PATH = "Data/Category";
    public const string LEVEL_PATH = "Data/Level";

    public List<WordObject> m_wordList = new List<WordObject>();
    public List<CategoryObject> m_categoryList = new List<CategoryObject>();
    public List<LevelObject> m_levelList = new List<LevelObject>(); 

    void Start()
    {
        InitWord();
        InitCategory();
        InitLevel();
    }

    [ContextMenu("load word")]
    // Load list word from file word data
    public void InitWord()
    {
        List<List<string>> words = FileLoader.LoadLineFromFile(WORD_PATH, false);
        WordObject wordObj;

        foreach (var word in words)
        {
            if (word != null && word[0] != "")
            {
                wordObj = new WordObject
                {
                    m_id = int.Parse(word[0]),
                    m_categoryId = int.Parse(word[1]),
                    m_word = word[2].Trim(),
                    m_image = word[3].Trim(),
                    m_sound = word[4].Trim(),
                    m_wordCount = int.Parse(word[5]),
                    m_win = int.Parse(word[6]),
                    m_lose = int.Parse(word[7])
                };
                m_wordList.Add(wordObj);
            }
        }
    }

    [ContextMenu("load category")]
    // Load list category from file category data
    public void InitCategory()
    {
        List<List<string>> categories = FileLoader.LoadLineFromFile(CATEGORY_PATH, false);
        CategoryObject categoryObj;

        foreach (var category in categories)
        {
            if (category != null && category[0] != "")
            {
                categoryObj = new CategoryObject
                {
                    m_id = int.Parse(category[0]),
                    m_levelId = int.Parse(category[1]),
                    m_category = category[2].Trim(),
                    m_photo = category[3].Trim(),
                    m_score = int.Parse(category[4]),
                    m_unlocked = bool.Parse(category[5])
                };
                m_categoryList.Add(categoryObj);
            }
        }
    }

    public void InitLevel()
    {
        List<List<string>> levels = FileLoader.LoadLineFromFile(LEVEL_PATH);
        LevelObject levelObj;

        foreach (var level in levels)
        {
            levelObj = new LevelObject
            {
                m_id = int.Parse(level[0]),
                m_level = level[1]
            };
            m_levelList.Add(levelObj);
        }
    }

    //get list word in category by category name
    public List<WordObject> GetWordsInCategory(CategoryObject _category)
    {
        return GetWordsInCategoryById(_category.m_id);
    }

    //get list word in category by category ID
    public List<WordObject> GetWordsInCategoryById(int _categoryId)
    {
        IEnumerable<WordObject> result = m_wordList.Where(o => o.m_categoryId == _categoryId);
        return result.ToList();
    }

    //get list category in level
    public List<CategoryObject> GetCagtegoriesInLevel(int _level)
    {
        var result = m_categoryList.Where(o => o.m_levelId == _level);
        return result.ToList();
    }

    public void SetScoreInCategory(CategoryObject _category, int _score)
    {
        m_categoryList[m_categoryList.IndexOf(_category)].m_score = _score;
        //if (
        //    !m_categoryList[
        //        m_categoryList.IndexOf(_category) + 1 < m_categoryList.Count
        //            ? m_categoryList.IndexOf(_category) + 1
        //            : m_categoryList.IndexOf(_category)].m_unlocked && _score >= 50)
        //{
        //    m_categoryList[m_categoryList.IndexOf(_category) + 1].m_unlocked = true;
        //}
        if (_score >= 50)
        {
            var temp = GetCagtegoriesInLevel(_category.m_levelId);
            temp = temp.OrderBy(x => x.m_id).ToList();
            int index = temp.IndexOf(_category);
            CategoryObject nextCategory = index < temp.Count - 1 ? temp[index + 1] : temp[index];

            m_categoryList[m_categoryList.IndexOf(nextCategory)].m_unlocked = true;
        }

        List<string> data = new List<string>();
        data.Add("ID	levelID	name	photo	score	unlocked");
        foreach (var categoryObject in m_categoryList)
        {
            data.Add(categoryObject.ToString());
        }
        FileLoader.WriteFIleToLocalStore(CATEGORY_PATH, data);
    }
}
