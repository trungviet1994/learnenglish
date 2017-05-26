using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public enum eObjectType
{
    ui_Letter_Good = 1,
    Letter_Prefabs = 2,
    ui_category = 3,
    ui_Click = 4,
    ui_ScoreAdd=5,
    par_pickup = 6,
    par_cogratulation = 7,
    par_scorePickUp = 8,
    par_animalAppear = 9
}

[System.Serializable]
public class ObjectConfig
{
    public eObjectType _type;
    public GameObject _object;
}

public enum ePoolName
{
    pool = 0,
    ObjectPool = 1
    //LetterGoodPool =1,
    //CategoryPool = 2
}

public class ManagerObject : MonoSingleton<ManagerObject>
{
    public ObjectConfig[] listObjectConfig;
    public Dictionary<eObjectType, GameObject> dicListObject;

    void Awake()
    {
        InitDictionary();
    }

    public void InitDictionary()
    {
        dicListObject = new Dictionary<eObjectType, GameObject>();

        for (int i = 0; i < listObjectConfig.Length;i++)
        {
            dicListObject.Add(listObjectConfig[i]._type, listObjectConfig[i]._object);
        }
    }
    public GameObject GetObjectByType(eObjectType type)
    {
        if (dicListObject.ContainsKey(type))
        {
            return dicListObject[type];
        }
#if UNITY_EDITOR
        Debug.Log("khong co trong list !!");
#endif
        return null;
    }

    public GameObject SpawnObjectByType(eObjectType type, ePoolName poolName)
    {
        GameObject objSpawn = GetObjectByType(type);
        SpawnPool pool = PoolManager.Pools[poolName.ToString()];
        if (pool != null && objSpawn != null)
        {
            return pool.Spawn(objSpawn).gameObject;
        }
#if UNITY_EDITOR
        Debug.Log("khong spawn duoc!");
#endif
        return null;
    }

    public GameObject SpawnObjectByType(eObjectType type, Transform parent,ePoolName poolName)
    {
        GameObject objSpawn = GetObjectByType(type);
        SpawnPool pool = PoolManager.Pools[poolName.ToString()];
        if (pool != null && objSpawn != null)
        {
            return pool.Spawn(objSpawn, parent).gameObject;
        }
#if UNITY_EDITOR
        Debug.Log("khong spawn duoc!");
#endif
        return null;
    }

    public void DespawnObject(GameObject obj, ePoolName poolName)
    {
        if (PoolManager.Pools.ContainsKey(poolName.ToString()))
        {
            SpawnPool pool = PoolManager.Pools[poolName.ToString()];
            if (pool.IsSpawned(obj.transform))
            {
                pool.Despawn(obj.transform);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(poolName + "khong co trong pool");
#endif
        }
    }

    public void DespawnObjectAfter(GameObject obj, ePoolName poolName, float time)
    {
        if (PoolManager.Pools.ContainsKey(poolName.ToString()))
        {
            SpawnPool pool = PoolManager.Pools[poolName.ToString()];
            pool.Despawn(obj.transform, time);
            //if (pool.IsSpawned(obj.transform))
            //{
            //    Debug.Log("despawm");
            //    pool.Despawn(obj.transform,time);
            //}
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(poolName + "khong co trong pool");
#endif
        }
    }

    public void DespawnAllObject(string poolName)
    {
        if (PoolManager.Pools.ContainsKey(poolName))
        {
            SpawnPool pool = PoolManager.Pools[poolName];
            pool.DespawnAll();
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(poolName + "khong co trong pool");
#endif
        }
    }
}
