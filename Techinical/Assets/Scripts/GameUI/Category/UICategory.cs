using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UICategory : MonoBehaviour
{
    public List<GameObject> m_listCategoryTilePrefabs = new List<GameObject>();
    public GameObject m_btnStart;
    // click choise level handler TODO: Quang commet. viet theo chuyen scene
   // public GameObject loadMore;
    public Transform grid;
    public ScrollRect scrollRect;
    public GameObject m_containerTotalScore;
    public Text m_txtTotalScore;
    public bool isInit = false;
    void OnEnable()
    {
        if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.SINGLE_PLAY)
        {
            m_btnStart.SetActive(false);
            m_containerTotalScore.SetActive(true);
        }
        else
        {
            m_btnStart.SetActive(true);
            m_containerTotalScore.SetActive(false);
            m_btnStart.GetComponent<BtnStartControl>().DeactiveButton();
        }

        DataManager.instance.m_choosenCategories.Clear();
    }

    private int GetTotalScoreOfSingleMode()
    {
        int totalScore = 0;
        for(int i=0;i<m_listCategoryTilePrefabs.Count;i++)
        {
            totalScore += m_listCategoryTilePrefabs[i].GetComponent<UICategoryTile>().GetScore();
        }
        return totalScore;
    }

    public void SetUpCategory()
    {
        scrollRect.normalizedPosition = Vector2.up;
        List<CategoryObject> _categories = DataManager.instance.m_listCategoryObject;
        //DespawnAllCategoryTile();
        if (_categories.Count <= 0)
        {
#if UNITY_EDITOR
            Debug.Log("khong co data category!");
            return;
#endif
        }
        if (!isInit)
        {
            for (int i = 0; i < _categories.Count; i++)
            {
                GameObject categoryPrefabs = ManagerObject.Instance.SpawnObjectByType(eObjectType.ui_category, grid, ePoolName.pool);
                categoryPrefabs.transform.localScale = Vector3.one;
                categoryPrefabs.GetComponent<UICategoryTile>().SetCategoryInfo(_categories[i]);

                m_listCategoryTilePrefabs.Add(categoryPrefabs);
            }
            m_txtTotalScore.text = GetTotalScoreOfSingleMode().ToString();
            isInit = true;
        }
        else
        {
            if (m_listCategoryTilePrefabs.Count > 0)
            {
                for (int i = 0; i < _categories.Count; i++)
                {
                    GameObject categoryPrefabs = null;
                    if (i >= m_listCategoryTilePrefabs.Count)
                    {
                        //Thieu slot item
                        categoryPrefabs = ManagerObject.Instance.SpawnObjectByType(eObjectType.ui_category, grid, ePoolName.pool);
                        categoryPrefabs.transform.localScale = Vector3.one;
                        m_listCategoryTilePrefabs.Add(categoryPrefabs);
                    }
                    else
                    {
                        //Khong thieu slot item
                        categoryPrefabs = m_listCategoryTilePrefabs[i];
                    }
                    categoryPrefabs.GetComponent<UICategoryTile>().SetCategoryInfo(_categories[i]);
                    
                    //Du thua slot item
                    if(i == _categories.Count)
                    {
                        for (int j = _categories.Count; j < m_listCategoryTilePrefabs.Count; j++)
                        {
                            Destroy(m_listCategoryTilePrefabs[j]);
                        }
                    }
                }
                m_txtTotalScore.text = GetTotalScoreOfSingleMode().ToString();
            }
            else
            {
                isInit = false;
                SetUpCategory();
            }
        }
    }


//    public void LoadMore()
//    {
//        List<CategoryObject> _categories = DataManager.instance.m_listCategoryObject;
//        DespawnAllCategoryTile();
//        if (_categories.Count <= 0)
//        {
//#if UNITY_EDITOR
//            Debug.Log("khong co data category!");
//            return;
//#endif
//        }
//        for (int i = 0; i < _categories.Count; i++)
//        {
//            GameObject categoryPrefabs = ManagerObject.Instance.SpawnObjectByType(eObjectType.ui_category, grid,ePoolName.pool);
//            categoryPrefabs.transform.localScale = Vector3.one;
//            categoryPrefabs.GetComponent<UICategoryTile>().SetCategoryInfo(_categories[i]);

//            m_listCategoryTilePrefabs.Add(categoryPrefabs);
//        }
//        GameObject go = Instantiate(loadMore, grid, false) as GameObject;
//        go.GetComponent<LoadMoreButton>().m_category = this;
//        m_listCategoryTilePrefabs.Add(go);
//        DefaultCategory();
//    }

    public void UpdateCategoryTile()
    {
        List<CategoryObject> _categories = DataManager.instance.m_listCategoryObject;
        if (_categories.Count <= 0)
        {
#if UNITY_EDITOR
            Debug.Log("khong co data category!");
            return;
#endif
        }
        for (int i = 0; i < m_listCategoryTilePrefabs.Count; i++)
        {
            // test
            //_categories[i].m_unlocked = true;
            m_listCategoryTilePrefabs[i].GetComponent<UICategoryTile>().SetCategoryInfo(_categories[i]);
        }
        //DefaultCategory();
    }

    private void DespawnAllCategoryTile()
    {
        if (m_listCategoryTilePrefabs.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < m_listCategoryTilePrefabs.Count; i++)
        {
            //ManagerObject.Instance.DespawnObject(m_listCategoryTilePrefabs[i], ePoolName.CategoryPool);
            Destroy(m_listCategoryTilePrefabs[i]);
        }
        m_listCategoryTilePrefabs = new List<GameObject>();
    }

    //void DefaultCategory()
    //{
    //    DataManager.instance.m_choosenCategories.Clear();
    //    if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.MULTI_PLAY)
    //    {
    //        if (m_listCategoryTilePrefabs.Count > 0)
    //        {
    //            m_listCategoryTilePrefabs[0].GetComponentInChildren<CategoryButton>().OnClicked();
    //        }
    //    }
    //}
}
