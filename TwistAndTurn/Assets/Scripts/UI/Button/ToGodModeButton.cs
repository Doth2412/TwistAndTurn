using UnityEngine.SceneManagement;

public class ToGodModeButon : AButton
{
    public override void OnButtonClick()
    {
        // Load the "GodMod" scene
        SceneManager.LoadScene("GodMode");
    }
}
