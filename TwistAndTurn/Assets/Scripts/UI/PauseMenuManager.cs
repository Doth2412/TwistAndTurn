using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : AButton
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        if(pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            return;
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
