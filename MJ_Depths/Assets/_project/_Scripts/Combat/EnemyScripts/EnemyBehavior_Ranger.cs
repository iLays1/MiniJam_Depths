using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior_Ranger : EnemyBehavior
{
    public Sprite idleSprite;
    public Sprite aimingSprite;

    protected override void Awake()
    {
        base.Awake();

        GameEvents.OnPlayerMove.AddListener(() =>
        {
            FacePlayer();
        });
    }

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
        FacePlayer();
    }

    void FacePlayer()
    {
        var diff = player.gridPos - self.gridPos;
        
        if((diff.x < 0))
            rend.flipX = true;
        if ((diff.x > 0))
            rend.flipX = false;

        if (player.gridPos.x == self.gridPos.x ||
            player.gridPos.y == self.gridPos.y)
        {
            rend.sprite = aimingSprite;
        }
        else
        {
            rend.sprite = idleSprite;
        }
    }
}
