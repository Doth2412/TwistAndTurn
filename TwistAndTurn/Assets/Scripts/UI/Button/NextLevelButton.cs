using UnityEngine;

public class NextLevelButton : AButton
{
	public static bool isNextLevelClicked;

	public override void Start()
    {
		base.Start();
		isNextLevelClicked = false;
	}

    public override void OnButtonClick()
	{
		base.OnButtonClick();
		if(!isNextLevelClicked)
		{
			isNextLevelClicked = true;
		}
	}
}