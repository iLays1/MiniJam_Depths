using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    public static Dictionary<Vector2Int, HashSet<GridOccupant>> occupantDictionary = new Dictionary<Vector2Int, HashSet<GridOccupant>>();

    public static void AddPosToDictionary(GridOccupant occupant)
    {
        if (occupantDictionary.ContainsKey(occupant.gridPos))
        {
            occupantDictionary.TryGetValue(occupant.gridPos, out var set);
            set.Add(occupant);
            return;
        }

        var newSet = new HashSet<GridOccupant>();
        newSet.Add(occupant);
        occupantDictionary.Add(occupant.gridPos, newSet);
    }
    public static void RemoveOccupantFromDictionary(GridOccupant occupant)
    {
        occupantDictionary.TryGetValue(occupant.gridPos, out var set);

        if (set == null)
        {
            occupantDictionary.Remove(occupant.gridPos);
            return;
        }

        set.Remove(occupant);
        if (set.Count <= 0)
        {
            occupantDictionary.Remove(occupant.gridPos);
        }
    }

    public static HashSet<GridOccupant> GetPositionSet(Vector2Int position)
    {
        if (occupantDictionary.ContainsKey(position))
        {
            occupantDictionary.TryGetValue(position, out var set);
            return set;
        }

        return null;
    }

    public static Vector2Int WorldToGridPos(Vector3 worldPos)
    {
        var gridPos = new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
        return gridPos;
    }
}

