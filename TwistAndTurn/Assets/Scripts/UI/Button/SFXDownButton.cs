using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXDownButton : AButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SFXManager.instance.DecreaseVolume();
    }
}
