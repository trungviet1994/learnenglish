using UnityEngine;
using System.Collections;

public class PlayAnimalButton : BaseClickButton{
    public override void OnClicked()
    {
        GameController.Instance.SetMode(true);
        SwapCamera.Instance.UseBackCamera();
        base.OnClicked();
    }
}
