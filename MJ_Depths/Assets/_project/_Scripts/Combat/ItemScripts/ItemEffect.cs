using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public virtual void Use(PlayerGridOccupant player, Vector2Int targetGridPos)
    {
        //Nothing
    }
    public virtual void OnPickup(Vector2Int targetGridPos)
    {
        //Nothing
    }
    public virtual void OnDrop(Vector2Int targetGridPos)
    {
        //Nothing
    }
}
