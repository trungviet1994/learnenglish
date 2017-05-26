using UnityEngine;
using System.Collections;
using Vuforia;

public class VuforiaManager : MonoSingleton<VuforiaManager> {
    
    [ContextMenu("tat camera tracking")]
    public void StopCameraAR()
    {
        //CameraDevice.Instance.Deinit();
    }
    [ContextMenu("bat camera tracking")]
    public void StartCameraAR()
    {
        //if (CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT))
        //{
        //    Debug.Log("change ok!!");
        //}
        //CameraDevice.Instance.Start();
        ///CameraDevice.Instance.
    }
}
