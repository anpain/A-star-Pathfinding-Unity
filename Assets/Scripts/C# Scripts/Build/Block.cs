using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObject/Block")]

public class Block : ScriptableObject
{
    public TileBase tile;
    public Sprite sprite;
    public ActionType actionType;
}

public enum BlockType
{
    BuildingBlock
}

public enum ActionType
{
    Dig, Mine
}