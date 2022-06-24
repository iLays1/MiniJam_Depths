using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupant : MonoBehaviour
{
    public bool blocksTile = false;

    public Vector2Int gridPos;

    public virtual void Awake()
    {
        gridPos = GridManager.WorldToGridPos(transform.position);
        transform.position = (Vector2)gridPos;
        SetPositionInGrid(gridPos);
    }

    public void SetPositionInGrid(Vector2Int targetPosition)
    {
        transform.DOKill();
        transform.DOMove((Vector2)targetPosition, 0.1f);
        GridManager.RemovePosFromDictionary(this);
        gridPos = targetPosition;
        GridManager.AddPosToDictionary(this);
    }
}
