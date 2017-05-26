using UnityEngine;
using System.Collections;

public class BackHomeButton : BaseClickButton {
    public override void OnClicked()
    {
        GameController.Instance.SetMode(false);
        SwapCamera.Instance.UseFrontCamera();
        base.OnClicked();
    }
}
