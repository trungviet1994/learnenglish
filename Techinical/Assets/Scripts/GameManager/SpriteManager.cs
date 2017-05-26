using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum eSpriteName
{
    ButtonDefaultIcon = 0,
    Lock =1,
    UnlockedSprite=2,
    Pass = 3,
    New =4,
    Fail =5,
    Icon_OK= 6,
    Icon_Back = 7,
    UnCheckSprite = 8,
    Bar_Yellow = 9,
    Sound_mute = 10,
    Music_mute = 11,
    Sound =12,
    Music = 13
}
[System.Serializable]
public class SpriteConfig
{
    public eSpriteName m_spriteName;
    public Sprite m_spriteValue;
}
public class SpriteManager : MonoSingleton<SpriteManager> {
    public SpriteConfig[] m_arraySpriteConfig;
    private Dictionary<eSpriteName, Sprite> m_dicSprite = new Dictionary<eSpriteName, Sprite>();

    void Awake()
    {
        Initiate();
    }

	public void Initiate()
    {
        for(int i=0;i< m_arraySpriteConfig.Length;i++)
        {
            m_dicSprite.Add(m_arraySpriteConfig[i].m_spriteName,m_arraySpriteConfig[i].m_spriteValue);
        }
    }

    public Sprite GetSpriteByTypeName(eSpriteName _typeName)
    {
        if(m_dicSprite.ContainsKey(_typeName))
        {
            return m_dicSprite[_typeName];
        }
#if UNITY_EDITOR
        Debug.Log("khong co sprite :"+_typeName.ToString());
#endif
        return null;
    }
}
