using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

public class FileLoader : MonoBehaviour
{
    public static List<List<string>> LoadLineFromFile(string _filePath, bool _local = false)
    {
        List<List<string>> output = new List<List<string>>();
        TextAsset textAsset;
        string[] temStrings;
        string localPath = Application.persistentDataPath + "/" + _filePath +".txt";
        if (!File.Exists(localPath))
        {
            textAsset = (TextAsset)Resources.Load(_filePath, typeof(TextAsset));
            temStrings = textAsset.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        }
        else
        {
            temStrings = File.ReadAllLines(localPath);
        }
        if (temStrings.Length > 0)
        {
            List<string> dataObj;
            for (int i = 1; i < temStrings.Length; i++)
            {
                string[] obj = temStrings[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                dataObj = new List<string>(obj);
                if (dataObj.Count != 0)
                {
                    output.Add(dataObj);
                }
            }
        }
        else
        {
            Debug.Log("Can't read file");
        }
        return output;
    }

    public static void WriteFIleToLocalStore(string _filePath, List<string> _data, bool _local = false)
    {
        _filePath = Application.persistentDataPath + "/" + _filePath + ".txt";
        CheckDirectory(_filePath);
        if (!File.Exists(_filePath))
        {
            Debug.Log("Create FIle");
            File.Create(_filePath).Close();
        }
        File.WriteAllLines(_filePath, _data.ToArray());
    }

    static void CheckDirectory(string _path)
    {
        string[] path = _path.Split(new string[] {"/"}, StringSplitOptions.None);
        string existPath = "";
        for (int i = 0; i < path.Length-1; i++)
        {
            existPath += path[i] + "/";
            if (!Directory.Exists(existPath))
            {
                Directory.CreateDirectory(existPath);
            }
        }
    }
}
