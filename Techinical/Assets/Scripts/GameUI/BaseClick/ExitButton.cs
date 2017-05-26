using UnityEngine;
using System.Collections;

public class ExitButton : BaseClickButton {
    public override void OnClicked()
    {
        Application.Quit();
        base.OnClicked();
    }
}
