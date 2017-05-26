using UnityEngine;
using System.Collections;

public class NextWordButton : BaseClickButton {
    public override void OnClicked()
    {
        if (GamePlayConfig.Instance.GameStart)
        {
            GameController.Instance.DoNextQuestion();
        }
        base.OnClicked();
    }
}
