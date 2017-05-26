using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class BaseImageTarget : MonoBehaviour {
    [SerializeField]
    protected ImageTargetBehaviour[] m_arrayImageTarget;
    //private Dictionary<string, ImageTargetBehaviour> dicImageTarget = new Dictionary<string,ImageTargetBehaviour>();
    
    void Awake()
    {
        InitAwake();
        m_arrayImageTarget = GetComponentsInChildren<ImageTargetBehaviour>();
        //InitDictionary();
    }
    void Start()
    {
        //DisableAllImageTarget();
    }

    public virtual void InitAwake()
    {

    }
    //private void InitDictionary()
    //{
    //    if(m_arrayImageTarget == null)
    //    {
    //        return;
    //    }
    //    for(int i=0;i< m_arrayImageTarget.Length;i++)
    //    {
    //        dicImageTarget.Add(m_arrayImageTarget[i].Getletter(),m_arrayImageTarget[i]);
    //    }
    //}

    public void DisableAllImageTarget()
    {
        if(m_arrayImageTarget[0].isActiveAndEnabled == false)
        {
            return;
        }
        for(int i=0;i<m_arrayImageTarget.Length;i++)
        {
            if (m_arrayImageTarget[i].isActiveAndEnabled)
            {
                m_arrayImageTarget[i].enabled = false;  
            }
        }
    }

    public void EnableAllImageTarget()
    {
        if(m_arrayImageTarget[0].isActiveAndEnabled == true)
        {
            return;
        }
        for (int i = 0; i < m_arrayImageTarget.Length; i++)
        {
            if (!m_arrayImageTarget[i].isActiveAndEnabled)
            {
                m_arrayImageTarget[i].enabled = true;
            }
        }
    }

//    private ImageTargetBehaviour GetImageTargetByLetter(string _letter)
//    {
//        if(dicImageTarget.ContainsKey(_letter))
//        {
//            return dicImageTarget[_letter];
//        }
//#if UNITY_EDITOR
//        Debug.Log("ko co tu nay trong dictionary");
//#endif
//        return null;
//    }

    //public void DisableImageTargetByLetter(string _letter)
    //{
    //    ImageTargetBehaviour imageTarget = GetImageTargetByLetter(_letter);
    //    if (imageTarget.isActiveAndEnabled)
    //    {
    //        imageTarget.enabled = false;
    //    }
    //}

    //public void EnableImageTargetByLetter(string _letter)
    //{
    //    ImageTargetBehaviour imageTarget = GetImageTargetByLetter(_letter);
    //    if (!imageTarget.isActiveAndEnabled)
    //    {
    //        imageTarget.enabled = true;
    //    }
    //}
}
