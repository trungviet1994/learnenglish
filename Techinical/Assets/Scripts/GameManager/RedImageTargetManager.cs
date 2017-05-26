using UnityEngine;
using System.Collections;

public class RedImageTargetManager : BaseImageTarget
{
    public static RedImageTargetManager instance;
    public override void InitAwake()
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
}
