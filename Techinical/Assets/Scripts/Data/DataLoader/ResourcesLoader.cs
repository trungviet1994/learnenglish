using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ResourcesLoader : MonoBehaviour
{
    public const string SPITE_PATH = "Image/MainImage/"; //path to image directory
    public const string SPITE_PATH_IOS = "TexturePacker/"; //path to image directory
    public const string AUDIO_PATH = "Audio/"; //path to audio directory
    public const string AUDIO_PATH_LETTER = "Audio/LetterClip/"; //path to audio directory
    public const string CATEGORY_PATH = "Image/Cate";

    public List<Sprite> m_arraySpriteImage ;
    public List<Sprite> m_arraySpriteCategory;
    void Awake()
    {
        // get all category
        //m_arraySpriteCategory = Resources.LoadAll<Sprite>(CATEGORY_PATH);
        m_arraySpriteCategory = new List<Sprite>(Resources.LoadAll<Sprite>(CATEGORY_PATH));
        //get all image 
        //m_arraySpriteImage = Resources.LoadAll<Sprite>(SPITE_PATH);
        m_arraySpriteImage = new List<Sprite>(Resources.LoadAll<Sprite>(SPITE_PATH));
    }
    // Load sprite by word
    public Sprite LoadSpriteByWord(WordObject _word, bool _local = false)
    {
        return LoadSpriteByName(_word.m_image, _local);
    }

    //load audio by word
    public AudioClip LoadAudioClipByWord(WordObject _word, bool _local = false)
    {
        return LoadAudioClipByName(_word.m_sound, _local);
    }

    //Load sprite from local memory or unity resources by name
    public Sprite LoadSpriteByName(string _word, bool _local = false)
    {
        Sprite result = null;
        if (!_local)
        {
            result = GetSprite(_word);
            //Debug.Log(result.name);
        }
        return result != null? result : Resources.Load<Sprite>(SPITE_PATH + "null");
    }
    private Sprite GetSprite(string textureName)
    {
        //return Resources.Load<Sprite>(SPITE_PATH+textureName);
        return m_arraySpriteImage.Find(x => x.name.Equals(textureName));
        //return m_arraySpriteImage.Where(t => t.name == textureName).First<Sprite>();
    }
    private Sprite GetSpriteInCategory(string textureName)
    {
        return m_arraySpriteCategory.Find(x => x.name.Equals(textureName));
        //return m_arraySpriteCategory.Where(t => t.name == textureName).First<Sprite>();
    }
    //Load Audio from local memory or unity resources by name
    public AudioClip LoadAudioClipByName(string _word, bool _local = false)
    {
        AudioClip result = null;
        if (!_local)
        {
            result = Resources.Load<AudioClip>(AUDIO_PATH + _word);
        }
        //return if result is not null
        return result;
    }

    //
    public Sprite LoadCategorySpriteByCategory(CategoryObject _category, bool _local = false)
    {
        return LoadCategorySpriteByName(_category.m_photo, _local);
    }

    //Load category image
    public Sprite LoadCategorySpriteByName(string _category, bool _local = false)
    {
        Sprite result = null;
        if (!_local)
        {
            //result = Resources.Load<Sprite>(CATEGORY_PATH + _category);
            result = GetSpriteInCategory(_category);
        }
        return result?? Resources.Load<Sprite>(SPITE_PATH + "null");
    }

#region CODE_OF_TOBI
    public AudioClip GetAudioClipFromResourses(string _letter,bool _isLocal)
    {
        AudioClip result = null;
        if (!_isLocal)
        {
            result = Resources.Load<AudioClip>(AUDIO_PATH_LETTER + _letter);
        }
        else
        {
            //load audio from local memory
        }

        //return if result is not null
        return result;
    }
#endregion
}
