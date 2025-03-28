public class ReplayButton : AButton
{
    public static bool isReplayClicked;

    public override void Start()
    {
        base.Start();
        isReplayClicked = false;
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        if (!isReplayClicked)
        {
            isReplayClicked = true;
        }
    }
}
