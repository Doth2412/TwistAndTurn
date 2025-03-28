using UnityEngine;

public class QuitButton : AButton
{
    public override void OnButtonClick()
    {
        Application.Quit();
    }
}
