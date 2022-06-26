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
        SetPositionInGrid(gridPos, true);
    }

    public void SetPositionInGrid(Vector2Int targetPosition, bool setInstantly = false)
    {
        if(setInstantly)
        {
            transform.position = (Vector2)targetPosition;
        }
        else
        {
            transform.DOKill();
            transform.DOMove((Vector2)targetPosition, 0.1f);
        }

        GridManager.RemoveOccupantFromDictionary(this);
        gridPos = targetPosition;
        GridManager.AddPosToDictionary(this);
    }

    public virtual void MoveInDir(Vector2Int dir)
    {
        var finalPos = gridPos + dir;
        var set = GridManager.GetPositionSet(finalPos);
        bool blocked = false;

        if (set != null)
        {
            foreach (var o in set)
            {
                if (o.blocksTile)
                {
                    blocked = true;
                    break;
                }
            }
        }

        if (blocked)
        {
            //Cant move into blocked tile

            transform.DOComplete();
            transform.DOPunchPosition((Vector2)dir * 0.6f, 0.25f);
            return;
        }

        AudioManager.Instance.PlayRandomPitch("Move", 0.7f, 1f);
        SetPositionInGrid(finalPos);
    }
}
