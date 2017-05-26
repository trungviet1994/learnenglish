using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class JsonVersion
{
    public string error_code;
    public string message;
    public int data;
}

public class UpdateManager : MonoSingleton<UpdateManager>
{
    private int VERSION_CODE = 1;
    private string url = "http://api.armplay.com/get_version_code";
    public Button Upgrade;

    //public Animator UpgradeAnimator;
    private bool updated = true;

    public static bool haveNewVersion = false;
    void Start()
    {
        CheckForUpdate();
    }

    public void CheckForUpdate()
    {    
        StartCoroutine(CheckUpdate());
    }

    IEnumerator CheckUpdate()
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            //var result = JsonUtility.FromJson<JsonVersion>(www.text);
            //bool isNewVersion = result.data > VERSION_CODE;
            //if (isNewVersion && updated && PlayerPrefs.HasKey("letterCards"))
            //{
            //    ScreenManager.Instance.ShowPopupScreen(ePopupType.UPDATE);
            //    haveNewVersion = true;
            //    updated = false;
            //}
            //else
            //{
            //    ScreenManager.Instance.DestroyPopupByType(ePopupType.UPDATE);
            //}
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
