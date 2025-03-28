using UnityEngine;
using System.IO;

public class MapInitializer : MonoBehaviour
{
    // The folder name for your maps
    private string mapsFolderName = "Maps";

    void Start()
    {
        // Copy maps on startup if needed
        CopyMapsIfNeeded();
    }

    // Call this method on startup to copy maps if they don't already exist
    void CopyMapsIfNeeded()
    {
        string persistentMapsPath = Path.Combine(Application.persistentDataPath, mapsFolderName);
        string streamingMapsPath = Path.Combine(Application.streamingAssetsPath, mapsFolderName);

        // If the folder doesn't exist, copy all files
        if (!Directory.Exists(persistentMapsPath))
        {
            Directory.CreateDirectory(persistentMapsPath);
            CopyAllMaps(streamingMapsPath, persistentMapsPath);
        }
        else
        {
            Debug.Log("Maps already exist in persistentDataPath.");
        }
    }

    // Synchronously copies all map files from the source to destination folder
    void CopyAllMaps(string sourceFolder, string destinationFolder)
    {
        for (int i = 1; i <= 16; i++)
        {
            string fileName = i.ToString() + ".json";
            string sourceFilePath = Path.Combine(sourceFolder, fileName);
            string destinationFilePath = Path.Combine(destinationFolder, fileName);

            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
                Debug.Log("Copied " + fileName + " to persistentDataPath.");
            }
            else
            {
                Debug.LogError("File not found: " + sourceFilePath);
            }
        }
    }

    // OnApplicationQuit is called when the game is about to close.
    void OnApplicationQuit()
    {
        // Re-copy the files from StreamingAssets to persistentDataPath upon game exit.
        string persistentMapsPath = Path.Combine(Application.persistentDataPath, mapsFolderName);
        string streamingMapsPath = Path.Combine(Application.streamingAssetsPath, mapsFolderName);

        // Ensure the persistent folder exists
        if (!Directory.Exists(persistentMapsPath))
        {
            Directory.CreateDirectory(persistentMapsPath);
        }

        // Overwrite the persistent files with the versions from StreamingAssets.
        CopyAllMaps(streamingMapsPath, persistentMapsPath);
        Debug.Log("Re-copied maps from StreamingAssets to persistentDataPath on exit.");
    }
}
