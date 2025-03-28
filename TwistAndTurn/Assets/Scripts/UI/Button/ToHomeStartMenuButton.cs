using UnityEngine.SceneManagement;
using UnityEngine;

public class ToHomeStartMenuButton : AButton
{
    // Start is called before the first frame update
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SceneManager.LoadScene("StartMenu");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}
