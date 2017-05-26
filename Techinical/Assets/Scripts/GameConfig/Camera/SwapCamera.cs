using UnityEngine;
using System.Collections;
using Vuforia;

[RequireComponent(typeof(VuforiaBehaviour))]
public class SwapCamera : MonoSingleton<SwapCamera>
{
    public VuforiaBehaviour vuforiaBehaviour;
    private bool m_isFrontCamera = true;

    void Start()
    {
//#if UNITY_EDITOR
//        UseBackCamera();
//#endif
//#if UNITY_ANDROID
//        UseFrontCamera();
//#endif
    }

    public void OnClicked()
    {
        m_isFrontCamera = !m_isFrontCamera;
        if (m_isFrontCamera)
        {
            // using back camera
            UseBackCamera();
        }
        else
        {
            UseFrontCamera();
        }
    }

    public void UseBackCamera()
    {
        if(m_isFrontCamera)
        {
            m_isFrontCamera = !m_isFrontCamera;
            RestartCamera(CameraDevice.CameraDirection.CAMERA_DEFAULT);
        }
    }

    public void UseFrontCamera()
    {
        if(!m_isFrontCamera)
        {
            m_isFrontCamera = !m_isFrontCamera;
            RestartCamera(CameraDevice.CameraDirection.CAMERA_FRONT);
        }
    }

    private void RestartCamera(CameraDevice.CameraDirection direction)
    {
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        if (CameraDevice.Instance.Init(direction))
        {

        }
        CameraDevice.Instance.Start();
    }
}