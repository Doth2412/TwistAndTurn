using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDoals", order = 1)]
public class LevelData : ScriptableObject
{
	public int levelID;
	public int maxTiles;
	public int starToCollect;
}