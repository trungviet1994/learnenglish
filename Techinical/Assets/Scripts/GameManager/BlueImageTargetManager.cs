using UnityEngine;
using System.Collections;

public class BlueImageTargetManager : BaseImageTarget
{
    public static BlueImageTargetManager instance;
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
