using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior_Ranger : EnemyBehavior
{
    protected override void Act()
    {
        List<Vector2Int> preferedMoves = new List<Vector2Int>();

        if (player.gridPos.x == self.gridPos.x ||
            player.gridPos.y == self.gridPos.y)
        {
            AttackPlayer();
            return;
        }

        var diff = player.gridPos - self.gridPos;
        var xdis = Mathf.Abs(diff.x);
        var ydis = Mathf.Abs(diff.y);

        Vector2Int dir;
        dir = (xdis < ydis) ? 
            ((diff.x < 0) ? Vector2Int.left : Vector2Int.right) : 
            ((diff.y < 0) ? Vector2Int.down : Vector2Int.up);
        
        MoveInDir(dir);
    }
}
