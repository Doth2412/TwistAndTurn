using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenuManager : MonoBehaviour
{
    static int MAX_BUTTONS = 8;
    public int maxPage; 
    public static LevelMenuManager instance;

    public int pageID;
    public TextMeshProUGUI pageText;
    public NextButtonLevelMenu nextButton;
    public PrevButtonLevelMenu prevButton;
    public LevelUIButton[] buttons;
    private LevelData[] allLevelData;

    // Awake is called before the first frame update
    void Awake()
    {   
        instance = this;
        pageID = 0;

        // Load all LevelData scriptable objects from the Resources folder
        allLevelData = Resources.LoadAll<LevelData>("");
        maxPage = allLevelData.Length / MAX_BUTTONS;
        pageText.text = (pageID + 1).ToString() + "/" + maxPage.ToString();
        AssignData();
        
    }
    
    void AssignData()
    {
        for (int i = 0; i < MAX_BUTTONS; i++)
        {
            buttons[i].levelData = allLevelData[i + pageID * MAX_BUTTONS];
        }
    }

    public void ChangePage(int change)
    {
        pageID += change;
        prevButton.button.interactable = pageID != 0;
        nextButton.button.interactable = (pageID + 1) != maxPage;
        pageText.text = (pageID + 1).ToString() + "/" + maxPage.ToString();
        AssignData();
        for (int i = 0; i < MAX_BUTTONS; i++)
        {
            buttons[i].Start();
        }
    }
}