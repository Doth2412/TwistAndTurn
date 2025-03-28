using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicUpButton : AButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SoundTrackManager.instance.IncreaseVolume();
    }
}
