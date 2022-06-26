using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int damage = 1;
    public int ticksToAct = 2;
    int ticks = 0;
    protected EnemyGridOccupant gridSelf;
    PlayerGridOccupant playerOccupant;

    private void Awake()
    {
        gridSelf = GetComponent<EnemyGridOccupant>();
        playerOccupant = FindObjectOfType<PlayerGridOccupant>();

        GameEvents.OnPlayerAct.AddListener(() =>
        {
            ticks++;

            if (ticks >= ticksToAct)
            {
                Act();
                ticks = 0;
            }
        });
    }

    protected virtual void Act()
    {
        List<Vector2Int> preferedMoves = new List<Vector2Int>();

        if(playerOccupant.gridPos.x > gridSelf.gridPos.x)
            preferedMoves.Add(new Vector2Int(1,0));

        if (playerOccupant.gridPos.x < gridSelf.gridPos.x)
            preferedMoves.Add(new Vector2Int(-1, 0));

        if (playerOccupant.gridPos.y > gridSelf.gridPos.y)
            preferedMoves.Add(new Vector2Int(0, 1));

        if (playerOccupant.gridPos.y < gridSelf.gridPos.y)
            preferedMoves.Add(new Vector2Int(0, -1));

        var dir = preferedMoves[Random.Range(0, preferedMoves.Count)];
        gridSelf.MoveInDir(dir);
    }
}
