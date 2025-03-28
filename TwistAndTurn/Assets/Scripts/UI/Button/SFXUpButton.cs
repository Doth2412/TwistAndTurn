using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXUpButton : AButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SFXManager.instance.IncreaseVolume();
    }
}
