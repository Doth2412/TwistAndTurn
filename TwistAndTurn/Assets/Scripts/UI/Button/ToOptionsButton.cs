using UnityEngine.SceneManagement;

public class ToOptionsButton : AButton
{
    // Start is called before the first frame update
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SceneManager.LoadScene("Options");
    }
}
