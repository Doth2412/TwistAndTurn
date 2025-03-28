using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonLevelMenu : AButton
{
    public override void Start()
    {
        base.Start();
        button.interactable = true;
    }
    public override void OnButtonClick()
    {
        base.OnButtonClick();   
        LevelMenuManager.instance.ChangePage(1);
    }
}
