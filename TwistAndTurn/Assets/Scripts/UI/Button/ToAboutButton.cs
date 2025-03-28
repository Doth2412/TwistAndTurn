using UnityEngine.SceneManagement;

public class ToAboutButton : AButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SceneManager.LoadScene("About");
    }
}
