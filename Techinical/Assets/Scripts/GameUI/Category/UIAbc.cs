using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAbc : MonoBehaviour
{
    [SerializeField]
    private CategoryObject m_category;
    [SerializeField]
    private int m_categoryID;

    public Text m_score;
    
    void Awake()
    {
        m_category = DataLoader.Instance.m_categoryList[m_categoryID];
    }

    void OnEnable()
    {
        if(m_score && m_category != null)
        {
            m_score.text = m_category.m_score.ToString();
        }
    }
}
