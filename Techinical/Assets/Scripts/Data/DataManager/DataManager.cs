using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public class DataManager : MonoBehaviour
{
    public ResourcesLoader m_resourcesLoader;
    public static DataManager instance;
    public int m_choosenLevel;
    public CategoryObject m_choosenCategory;
    public List<WordObject> m_listWord;
    public List<CategoryObject> m_choosenCategories = new List<CategoryObject>();
    public int m_redScore;
    public int m_blueScore;

    public List<CategoryObject> m_listCategoryObject = new List<CategoryObject>();
    void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //Get Image for word
    public Sprite GetImageByName(string _imageName)
    {
        return m_resourcesLoader.LoadSpriteByName(_imageName);
    }

    //get audio for word
    public AudioClip GetAudioByName(string _audioName)
    {
        return m_resourcesLoader.LoadAudioClipByName(_audioName);
    }

    //get list word in category
    public List<WordObject> GetWordsInCategory(CategoryObject _category, int _numberOfWord = 10)
    {
        var result = DataLoader.Instance.GetWordsInCategory(_category);
        var r = new Random();
        result= result.OrderBy(x => r.Next(int.MaxValue)).ToList();
        
        result.RemoveRange(_numberOfWord, result.Count - _numberOfWord);
        return result;
    }

    public List<WordObject> GetAllLetter()
    {
        var result = DataLoader.Instance.GetWordsInCategoryById(0);
        var r = new Random();
        result = result.OrderBy(x => r.Next(int.MaxValue)).ToList();

        return result;
    } 

    public List<WordObject> GetWordsInMulti(List<CategoryObject> _categories)
    {
        List<WordObject> result = new List<WordObject>();
        foreach (var categoryObject in _categories)
        {
            var temp = DataLoader.Instance.GetWordsInCategory(categoryObject);
            temp.ForEach(x=> {result.Add(x);});
        }
        var r = new Random();
        result = result.OrderBy(x => r.Next(int.MaxValue)).ToList();
        return result;
    }

    public List<WordObject> GetWordsInMulti()
    {
        List<WordObject> result = new List<WordObject>();
        for (int i=0;i< m_choosenCategories.Count;i++)
        {
            var temp = DataLoader.Instance.GetWordsInCategory(m_choosenCategories[i]);
            temp.ForEach(x => { result.Add(x); });
        }
        var r = new Random();
        result = result.OrderBy(x => r.Next(int.MaxValue)).ToList();
        return result;
    }

    //get all category in level
    public List<CategoryObject> GetCategoriesInLevel(int _level)
    {
        m_listCategoryObject = new List<CategoryObject>();
        m_listCategoryObject = DataLoader.Instance.GetCagtegoriesInLevel(_level);
        return m_listCategoryObject;
    }

    //Set score for category
    public void SetScoreInCategory(CategoryObject _category, int _score)
    {
        DataLoader.Instance.SetScoreInCategory(_category, _score);
    }

    public void SetScoreReadAbc(int _score)
    {
        DataLoader.Instance.SetScoreInCategory(DataLoader.Instance.m_categoryList[0], _score);
    }

    public void SetScoreHearAbc(int _score)
    {
        DataLoader.Instance.SetScoreInCategory(DataLoader.Instance.m_categoryList[1], _score);
    }

    //public void SetLevel(int _level)
    //{
    //    m_choosenLevel = _level;
    //}

    public void SetCategory(CategoryObject _category)
    {
        m_choosenCategory = _category;
        m_listWord = GetWordsInCategory(m_choosenCategory);
    }

    public Sprite GetCategoryImage(CategoryObject _category)
    {
        return m_resourcesLoader.LoadCategorySpriteByCategory(_category);
    }

    #region CODE_OF_TOBI
    //get audio of letter
    public AudioClip GetAudioByLetter(string _letter)
    {
        return m_resourcesLoader.GetAudioClipFromResourses(_letter,false);
    }
    public void SetScoreInCategory( int _score)
    {
        DataLoader.Instance.SetScoreInCategory(this.m_choosenCategory, _score);
    }

    public void GetAllLetterQuestion()
    {
        m_listWord = GetAllLetter();
    }

    // custumise code of Quang
    public void SetScoreInLearnLetter(int _score, QuestionType _learnLetterType)
    {
        int indexCategoryList = 0;
        if(_learnLetterType != QuestionType.QS_IMAGE)
        {
            indexCategoryList = 1;
        }
        DataLoader.Instance.SetScoreInCategory(DataLoader.Instance.m_categoryList[indexCategoryList], _score);
    }

    // button click start play multi
    public void GetListQuestionInMultiPlay()
    {
        m_listWord.Clear();
        this.m_listWord = GetWordsInMulti();
    }
    #endregion


    public void LoadAllWords()
    {
        SetScoreHearAbc(10);
        SetScoreReadAbc(30);
    }

    public Sprite GetImageFromLetterHead(string _letterHead)
    {
        //int listWordCount = DataLoader.Instance.m_wordList.Count;
        //string word = "";
        //for(int i=26;i< listWordCount;i++)
        //{
        //    if (_letterHead.Equals(DataLoader.Instance.m_wordList[i].m_word[0].ToString()))
        //    {             
        //        word = DataLoader.Instance.m_wordList[i].m_word;
        //        break;
        //    }
        //}
        return GetImageByName(GetWordFromLetterHead(_letterHead));
    }

    public string GetWordFromLetterHead(string _letterHead)
    {
        int listWordCount = DataLoader.Instance.m_wordList.Count;
        string word = "";
        for (int i = 26; i < listWordCount; i++)
        {
            if (_letterHead.Equals(DataLoader.Instance.m_wordList[i].m_word[0].ToString()))
            {
                word = DataLoader.Instance.m_wordList[i].m_word;
                break;
            }
        }
        if (word == "")
        {
            Debug.Log("KHONG CO CHU " + _letterHead + "TRONG RESOURCES !");
            return null;
        }
        return word;
    }
}
