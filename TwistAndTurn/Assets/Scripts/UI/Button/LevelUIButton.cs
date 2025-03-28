using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class LevelUIButton : AButton
{
	public LevelData levelData;
	
	public override void Start()
	{
	    base.Start();
		UpdateData();
	}	

	public override void OnButtonClick()
	{
		base.OnButtonClick();
		// Store the map path in a static variable in GridSystem
		string mapPath = Application.streamingAssetsPath	+ "/Maps/" + levelData.levelID.ToString() + ".json";
		//string mapPath = Path.Combine("Assets/Maps", levelData.levelID.ToString() + ".json");
		GridSystem.currentMapPath = mapPath;
		GridSystem.levelData = levelData;
		// Load the "Level" scene
		SceneManager.LoadScene("Level");
	}
	
	public void UpdateData()
	{
		int preLevelID = levelData.levelID - 1;
		bool isPreLevelCompleted;
		if (preLevelID == 0)
		{
			isPreLevelCompleted = true;
		}
		else
		{
			string preLevelPath = Application.streamingAssetsPath + "/Maps/" + preLevelID.ToString() + ".json";
			//string preLevelPath = Path.Combine("Assets/Maps", preLevelID.ToString() + ".json");
			string json = File.ReadAllText(preLevelPath);
			GridSystem.SerializableMap preMap = JsonUtility.FromJson<GridSystem.SerializableMap>(json);
			isPreLevelCompleted = preMap.levelCompleted;
		}
		button.interactable = isPreLevelCompleted;
		Transform childImage = transform.GetChild(0);
		Transform childText = transform.GetChild(1);
		if (childImage != null)
		{
			childImage.gameObject.SetActive(!isPreLevelCompleted);
		}
		if (childText != null)
		{
			childText.GetComponent<TextMeshProUGUI>().text = levelData.levelID.ToString();
			childText.gameObject.SetActive(isPreLevelCompleted);
		}
	}
}