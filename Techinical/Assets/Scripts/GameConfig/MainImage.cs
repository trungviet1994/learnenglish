using UnityEngine;
using System.Collections;

public class MainImage : MonoBehaviour {
    public Camera m_mainCamera;
	public void ConfigScale()
    {
        float valueScaleTo = (m_mainCamera.aspect * transform.localScale.x)/0.75f;
        this.transform.localScale = Vector3.one * valueScaleTo;
    }
}
