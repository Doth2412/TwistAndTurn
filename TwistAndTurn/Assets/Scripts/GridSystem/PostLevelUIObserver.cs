using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class PostLevelUIObserver : MonoBehaviour
{
	public GameObject postLevelUI;
	public Sprite starSprite;
	public Sprite noStarSprite;
	public Sprite checkSprite;
	public Sprite crossSprite;
	
	public Image[] stars;
	public Image[] checks;
	public TextMeshProUGUI[] missionText;
	public TextMeshProUGUI levelNameText;
	public Start_PauseGameButton startPauseGameButton;
	public ReplayGameButton replayGameButton;
	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < 3; i++)
		{
			stars[i].sprite = noStarSprite;
			checks[i].sprite = crossSprite;
		}
	}

    void Update()
    {
		startPauseGameButton.button.interactable = !postLevelUI.activeSelf;
		replayGameButton.button.interactable = !postLevelUI.activeSelf;
        if(GoalTile.isGoalReached)
        {
            CalculatePoint();
			return;
        }
		if(NextLevelButton.isNextLevelClicked)
		{
			postLevelUI.SetActive(false);
			int nextLevelID = GridSystem.levelData.levelID + 1;
			int stage = nextLevelID / 8;
			int level = nextLevelID % 8 != 0 ? nextLevelID % 8 : 8;
			if(level == 8) stage--;
			
			try{
			GridSystem.levelData = Resources.Load<LevelData>("LevelData/" + stage.ToString() + "." + level.ToString());
			}
			catch
			{
				SceneManager.LoadScene("LevelMenu");
				return;
			}
			string mapPath = Application.streamingAssetsPath + "/Maps/" + GridSystem.levelData.levelID.ToString() + ".json";

			//string mapPath = Path.Combine("Assets/Maps", GridSystem.levelData.levelID.ToString() + ".json");
			GridSystem.currentMapPath = mapPath;
			GridSystem.instance.LoadMap(GridSystem.currentMapPath);
			NextLevelButton.isNextLevelClicked = false;
			startPauseGameButton.ChangeButtonSprite();
		}
		if (ReplayButton.isReplayClicked)
		{
			if(Time.timeScale == 0)
			{
				Time.timeScale = 1;
			}
			postLevelUI.SetActive(false);
			ReplayButton.isReplayClicked = false;
			startPauseGameButton.ChangeButtonSprite();
			GridSystem.instance.LoadMap(GridSystem.currentMapPath);
		}
	}

    public void CalculatePoint()
	{
		missionText[0].text = "Reach the goal";
		missionText[1].text = "Use maximum " + GridSystem.levelData.maxTiles 
		+ " tiles: " + GridSystem.instance.tilePlaced + " tiles";
		missionText[2].text = "Collect the star(s)";
		levelNameText.text = "Level " + 
			(GridSystem.levelData.levelID <= 9 
			? "0" + GridSystem.levelData.levelID.ToString() 
			: GridSystem.levelData.levelID.ToString());
		checks[0].sprite = checkSprite;
		int diff = GridSystem.levelData.maxTiles - GridSystem.instance.tilePlaced;
		checks[1].sprite = diff >= 0 ? checkSprite : crossSprite;
		if (diff >= 0)
		{
			GridSystem.instance.goalsAchieved++;
		}
		checks[2].sprite = GridSystem.instance.starCollected == GridSystem.levelData.starToCollect ? checkSprite : crossSprite;
		if (GridSystem.instance.starCollected == GridSystem.levelData.starToCollect)
		{
			GridSystem.instance.goalsAchieved++;
		}
		for (int i = 0; i < 3; i++)
		{
			if(i <= GridSystem.instance.goalsAchieved)
				stars[i].sprite = starSprite;
			else
			stars[i].sprite = noStarSprite;
		}
		GridSystem.instance.levelCompleted = true;
		GridSystem.instance.UpdateMap();
		
		postLevelUI.SetActive(true);
		GoalTile.isGoalReached = false;
	}
}