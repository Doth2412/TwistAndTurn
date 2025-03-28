using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadFileButton : MonoBehaviour
{
	public GridSystem gridSystem;
	public Dropdown mapDropdown;
	public GameObject dropdownContainer; // Container for the dropdown to show/hide
	static public LoadFileButton instance;
	void Start()
	{
		// Hide the dropdown container initially
		dropdownContainer.SetActive(false);

		// Add listener to the button
		GetComponent<Button>().onClick.AddListener(OnLoadButtonClick);

		// Populate the dropdown with map names
		PopulateDropdown();
	}

	void OnLoadButtonClick()
	{
		// Show the dropdown container
		dropdownContainer.SetActive(!dropdownContainer.activeSelf);
	}

	public void PopulateDropdown()
	{
		mapDropdown.ClearOptions();
		List<string> options = new List<string>();

		string[] files = Directory.GetFiles("Assets/Maps", "*.json");
		foreach (string file in files)
		{
			options.Add(Path.GetFileNameWithoutExtension(file));
		}

		mapDropdown.AddOptions(options);
		mapDropdown.onValueChanged.RemoveAllListeners();

		// Add listener to the dropdown to load the selected map
		mapDropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(mapDropdown); });
	}

	void OnDropdownValueChanged(Dropdown dropdown)
	{
		string selectedMap = dropdown.options[dropdown.value].text;
		string mapPath = Application.streamingAssetsPath + "/Maps/" + selectedMap + ".json";

		//string filePath = Path.Combine("Assets/Maps/", selectedMap + ".json");
		GridSystem.currentMapPath = mapPath;
		

		// Call LoadMap with the selected file path
		gridSystem.LoadMap(mapPath);

		// Hide the dropdown container
		dropdownContainer.SetActive(false);
	}
}