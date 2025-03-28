using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GridSystem : MonoBehaviour
{
	// Grid size
	static public GridSystem instance;
	public int gridWidth = 10;
	public int gridHeight = 10;
	public float cellSize = 1f;
	//

	//Grid base
	public IPlaceable[,] levelMatrix;
	public IMoveable[,] moveables;
	public GameObject currentBuildableObject;
	private Vector3 playerStartCoordinate;
	private Vector3 playerStartDirection;
	[SerializeField] private GameObject[] buildablePrefabs; // Array to hold different tile prefabs
	public GameObject floatingBlock;
	[SerializeField] ButtonManager buttonManager;

	public bool isGameStart;
	public bool canPlaceTile;
	public int starCollected;
	public int tilePlaced;
	public bool levelCompleted = false;
	public int goalsAchieved;
	
	public static string currentMapPath;
	public static LevelData levelData;
	
	private AudioClip placeTileSound;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			levelMatrix = new IPlaceable[gridWidth, gridHeight];
			moveables = new IMoveable[gridWidth, gridHeight];
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		playerStartDirection = new Vector3(1f, 0, 0);
		playerStartCoordinate = new Vector3(1.5f, 1.5f, 0);
		isGameStart = false;
		canPlaceTile = true;
		tilePlaced = 0;
		placeTileSound = Resources.Load<AudioClip>("Sounds/place");
		LoadMap(currentMapPath);
	}

	void Update()
	{
		// if (Input.GetKeyDown(KeyCode.X))
		// {
		// 	isGameStart = !isGameStart;
		// 	canPlaceTile = !canPlaceTile;
		// }
		// if (Input.GetKeyDown(KeyCode.R))
		// {
		// 	LoadMap(currentMapPath);
		// }
		if(Input.GetKeyDown(KeyCode.Z))
		{
		    ResetProgress();
		}
		if(!canPlaceTile)
		{
			floatingBlock.SetActive(false);
		}
		
		if(canPlaceTile)
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 onGridPosition = GetGridPosition(mousePosition);
			// Check if in bound or occupied or selecte tile yet
			if (onGridPosition.x < 0 || onGridPosition.x >= gridWidth || onGridPosition.y < 0 || onGridPosition.y >= gridHeight
			|| levelMatrix[(int)onGridPosition.x, (int)onGridPosition.y] != null || moveables[(int)onGridPosition.x, (int)onGridPosition.y] != null
			|| !currentBuildableObject)
			{
				floatingBlock.SetActive(false);
				if (Input.GetMouseButton(1))
				{
					if (SceneManager.GetActiveScene().name == "Level")
					{
						IPlaceable placeable = levelMatrix[(int)onGridPosition.x, (int)onGridPosition.y];
						if (placeable != null)
						{
							MonoBehaviour monoBehaviour = placeable as MonoBehaviour;
							if (monoBehaviour.GetComponent<RotateLeftTile>() != null || monoBehaviour.GetComponent<RotateRightTile>() != null)
							{
								tilePlaced--;
								DeleteObjectOnGrid(onGridPosition);
							}
						}
					}
					else if (levelMatrix[(int)onGridPosition.x, (int)onGridPosition.y] != null || moveables[(int)onGridPosition.x, (int)onGridPosition.y] != null)
					{
						DeleteObjectOnGrid(onGridPosition);
					}
				}
				return;
			}
			
			floatingBlock.SetActive(true);
			UpdateFloatingBlockPosition(onGridPosition);
	
			if (Input.GetMouseButton(0))
			{
				PlaceObjectOnGrid(onGridPosition, currentBuildableObject);
			}
			
		}
		
	}

	void InitGrid()
	{
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				levelMatrix[x, y] = null;
			}
		}
	}

	public void SelectBuildablePrefab(GameObject prefab)
	{
		if(prefab == currentBuildableObject)
		{
			currentBuildableObject = null;
			floatingBlock.SetActive(false);
			return;
		}
		currentBuildableObject = prefab;
		floatingBlock.SetActive(true);
		floatingBlock.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
	}

	//static 
	public Vector3 GetGridPosition(Vector3 mousePosition)
	{
		int x = (int)(mousePosition.x  / cellSize);
		int y = (int)(mousePosition.y / cellSize);
		return new Vector3(x * cellSize + 0.5f, y * cellSize + 0.5f	, 0);
	}

	void UpdateFloatingBlockPosition(Vector3 gridPosition)
	{
		floatingBlock.transform.position = gridPosition;
	}

	void PlaceObjectOnGrid(Vector3 onGridPosition, GameObject objectToPlace)
	{
		int x =	(int) onGridPosition.x;
		int y = (int) onGridPosition.y;
		if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight && levelMatrix[x, y] == null && moveables[x, y] == null) 
		{	
			GameObject instantiatedObject = Instantiate(objectToPlace, onGridPosition, Quaternion.identity);
			IPlaceable newTile = instantiatedObject.GetComponent<IPlaceable>();
			IMoveable moveable = instantiatedObject.GetComponent<IMoveable>();
			if (moveable != null)
			{
				moveables[x, y] = moveable;
			}
			else levelMatrix[x, y] = newTile;
			if(currentBuildableObject != null && floatingBlock.activeInHierarchy)
			{
				tilePlaced++;
				//SFXManager.instance.PlaySoundSFXClip(placeTileSound);
			}
		}
	}
	
	public void UpdateMoveables(Vector3 startPosition, Vector3 endPosition)
	{
		int xStart = (int)startPosition.x;
		int yStart = (int)startPosition.y;	
		int xEnd = (int)endPosition.x;
		int yEnd = (int)endPosition.y;

		// Swap the moveables
		IMoveable tempMoveable = moveables[xStart, yStart];
		moveables[xStart, yStart] = moveables[xEnd, yEnd];
		moveables[xEnd, yEnd] = tempMoveable;
	}
	
	void DeleteObjectOnGrid(Vector3 onGridPosition)
	{
		
		int x = (int)onGridPosition.x;
		int y = (int)onGridPosition.y;
		if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
		{
			if(levelMatrix[x, y] != null)
			{
				DestroyImmediate(((MonoBehaviour)levelMatrix[x, y]).gameObject);
				levelMatrix[x, y] = null;
				return;
			}
			if (moveables[x, y] != null)
			{
				DestroyImmediate(((MonoBehaviour)moveables[x, y]).gameObject);
				moveables[x, y] = null;
				return;
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.gray;
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				Vector3 gridPosition = new Vector3(x * cellSize, y * cellSize, 0);
				Gizmos.DrawWireCube(gridPosition + new Vector3(0.5f, 0.5f, 0 ), new Vector3(cellSize, cellSize, 0));
			}
		}
	}

	public void SaveMap(string filePath)
	{
		List<SerializableTile> tiles = new List<SerializableTile>();
		List<SerializableTile> moves = new List<SerializableTile>();
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				if (levelMatrix[x, y] != null)
				{
					GameObject originalPrefab = null;
					foreach (var prefab in buildablePrefabs)
					{
						if (prefab.name == ((MonoBehaviour)levelMatrix[x, y]).gameObject.name.Replace("(Clone)", "").Trim())
						{
							originalPrefab = prefab;
							break;
						}
					}

					int prefabIndex = originalPrefab != null ? System.Array.IndexOf(buildablePrefabs, originalPrefab) : -1;
					tiles.Add(new SerializableTile
					{
						x = x,
						y = y,
						prefabIndex = prefabIndex
					});
				}
				if (moveables[x, y] != null)
				{
					GameObject originalPrefab = buildablePrefabs[6];
					int prefabIndex = originalPrefab != null ? System.Array.IndexOf(buildablePrefabs, originalPrefab) : -1;
					moves.Add(new SerializableTile
					{
						x = x,
						y = y,
						prefabIndex = prefabIndex
					});
				}
			}
		}
		playerStartCoordinate = Player.instance.gameObject.transform.position;
		playerStartDirection = Player.instance.moveDirection;

		string json = JsonUtility.ToJson(new SerializableMap
		{ 
			tiles = tiles,
			moves = moves,
			playerStartCoordinate = playerStartCoordinate,
			playerStartDirection = playerStartDirection,
			levelCompleted = false,
			goalsAchieved = 0,
			});
		File.WriteAllText(filePath, json);

		// Update the current map path
		currentMapPath = filePath;
	}

	public void LoadMap(string filePath)
	{
		if (File.Exists(filePath))
		{
			currentMapPath = filePath;
			string json = File.ReadAllText(filePath);
			SerializableMap map = JsonUtility.FromJson<SerializableMap>(json);

			// Clear the current map
			for (int x = 0; x < gridWidth; x++)
			{
				for (int y = 0; y < gridHeight; y++)
				{
					if (levelMatrix[x, y] != null)
					{
						DestroyImmediate(((MonoBehaviour)levelMatrix[x, y]).gameObject);
						levelMatrix[x, y] = null;
					}
					if (moveables[x, y] != null)
					{
						DestroyImmediate(((MonoBehaviour)moveables[x, y]).gameObject);
						moveables[x, y] = null;
					}
				}
			}
			// Load the new map
			foreach (SerializableTile tile in map.tiles)
			{
				Vector3 position = new Vector3(tile.x * cellSize + 0.5f, tile.y * cellSize + 0.5f, 0);
				PlaceObjectOnGrid(position, buildablePrefabs[tile.prefabIndex]);
			}
			foreach (SerializableTile move in map.moves)
			{
				Vector3 position = new Vector3(move.x * cellSize + 0.5f, move.y * cellSize + 0.5f, 0);
				PlaceObjectOnGrid(position, buildablePrefabs[move.prefabIndex]);
			}
			levelCompleted = map.levelCompleted;
			if(!Player.instance.gameObject.activeInHierarchy)
			{
				Player.instance.gameObject.SetActive(true);
			}
			Player.instance.ResetPlayer(map.playerStartCoordinate, map.playerStartDirection);
			
			isGameStart = false;
			tilePlaced = 0;
			canPlaceTile = true;
			goalsAchieved = 0;
			starCollected = 0;
			floatingBlock.SetActive(false);
			currentBuildableObject = null;
			buttonManager.ResetChoice();
		}
		else InitGrid();
	}

	public void UpdateMap()
	{
		if (File.Exists(currentMapPath))
		{
			string json = File.ReadAllText(currentMapPath);
			SerializableMap map = JsonUtility.FromJson<SerializableMap>(json);

			// Update the levelCompleted and goalsAchieved fields
			map.levelCompleted = true;
			if(map.goalsAchieved < goalsAchieved)
			{
				map.goalsAchieved = goalsAchieved;
			}
			// Write the updated map back to the file
			json = JsonUtility.ToJson(map);
			File.WriteAllText(currentMapPath, json);
		}
	}

	public void ResetProgress()
	{
		// Get all JSON files in the Maps folder
		string[] allLevels = Directory.GetFiles(Application.streamingAssetsPath, "*.json");

		// Iterate through each file and reset the progress
		foreach (string filePath in allLevels)
		{
			if (File.Exists(filePath))
			{
				string json = File.ReadAllText(filePath);
				SerializableMap map = JsonUtility.FromJson<SerializableMap>(json);

				// Reset the levelCompleted and goalsAchieved fields
				map.levelCompleted = false;
				map.goalsAchieved = 0;

				// Write the updated map back to the file
				json = JsonUtility.ToJson(map);
				File.WriteAllText(filePath, json);
			}
		}
	}

	[System.Serializable]
	public class SerializableTile
	{
		public int x;
		public int y;
		public int prefabIndex;
	}

	[System.Serializable]
	public class SerializableMap
	{
		public List<SerializableTile> tiles;
		public List<SerializableTile> moves;
		public Vector3 playerStartCoordinate;
		public Vector3 playerStartDirection;
		public bool levelCompleted;
		public int goalsAchieved;
	}
}