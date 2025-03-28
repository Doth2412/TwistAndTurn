using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class ToLevelMenuButton : AButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SceneManager.LoadScene("LevelMenu");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ResetProgress();
        }
    }
    public void ResetProgress()
    {
        // Get all JSON files in the Maps folder
        string streamingMapsPath = Path.Combine(Application.streamingAssetsPath, "Maps");
        string[] allLevels = Directory.GetFiles(streamingMapsPath, "*.json");

        // Iterate through each file and reset the progress
        foreach (string filePath in allLevels)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GridSystem.SerializableMap map = JsonUtility.FromJson<GridSystem.SerializableMap>(json);

                // Reset the levelCompleted and goalsAchieved fields
                map.levelCompleted = false;
                map.goalsAchieved = 0;

                // Write the updated map back to the file
                json = JsonUtility.ToJson(map);
                File.WriteAllText(filePath, json);
            }
        }
    }
}
