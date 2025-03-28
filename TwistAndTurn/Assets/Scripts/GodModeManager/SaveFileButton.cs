using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveFileButton : MonoBehaviour
{
	const string saveDirectory = "Assets/Maps/";
	public InputField inputField;
	public GameObject inputFieldContainer; // Container for the input field to show/hide

	void Start()
	{
		// Hide the input field container initially
		inputFieldContainer.SetActive(false);

		// Add listener to the button
		GetComponent<Button>().onClick.AddListener(OnSaveButtonClick);

		// Add listener to the input field to detect when Enter is pressed
		inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
	}

	void OnSaveButtonClick()
	{
		// Show the input field container
		if(inputFieldContainer.activeInHierarchy)
		{
			inputFieldContainer.SetActive(false);
		}
		else
		{
			inputFieldContainer.SetActive(true);
			inputField.text = GridSystem.currentMapPath; // Current map name
			inputField.ActivateInputField(); // Focus on the input field
		}
		
	}

	void OnInputFieldEndEdit(string input)
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			//string filePath;
			// Construct the full file path
			string mapPath = Application.streamingAssetsPath + "/Maps/" + input + ".json";

			//filePath = Path.Combine(saveDirectory, input + ".json");

			// Ensure the directory exists
			if (!Directory.Exists(saveDirectory))
			{
				Directory.CreateDirectory(saveDirectory);
			}

			// Call SaveMap with the constructed file path
			GridSystem.instance.SaveMap(mapPath);

			// Hide the input field container
			inputFieldContainer.SetActive(false);
		}
	}
}