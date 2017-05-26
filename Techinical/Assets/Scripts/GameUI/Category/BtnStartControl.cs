using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BtnStartControl :MonoSingleton<BtnStartControl>
{
    public Button StartButton;
    public Animator StartBtnAnimation;
    public void ActiveButton()
    {
        if (StartButton)
        {
            StartButton.interactable = true;
            StartBtnAnimation.enabled = true;
        }
    }

    public void DeactiveButton()
    {
        if (StartButton)
        {
            StartButton.interactable = false;
            StartBtnAnimation.enabled = false;
        }
    }

}
