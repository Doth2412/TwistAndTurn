using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDownButton : AButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SoundTrackManager.instance.DecreaseVolume();
    }
}
