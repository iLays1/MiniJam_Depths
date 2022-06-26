using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public SpriteRenderer rend;
    public int damage = 1;
    public int ticksToAct = 2;

    protected int ticks = 0;
    public EnemyGridOccupant self;
    protected PlayerGridOccupant player;

    protected virtual void Awake()
    {
        self = GetComponent<EnemyGridOccupant>();
        player = FindObjectOfType<PlayerGridOccupant>();

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

        if(player.gridPos.x > self.gridPos.x)
            preferedMoves.Add(new Vector2Int(1,0));

        if (player.gridPos.x < self.gridPos.x)
            preferedMoves.Add(new Vector2Int(-1, 0));

        if (player.gridPos.y > self.gridPos.y)
            preferedMoves.Add(new Vector2Int(0, 1));

        if (player.gridPos.y < self.gridPos.y)
            preferedMoves.Add(new Vector2Int(0, -1));

        var dir = preferedMoves[Random.Range(0, preferedMoves.Count)];
        MoveInDir(dir);
    }

    protected void MoveInDir(Vector2Int dir)
    {
        var finalPos = self.gridPos + dir;

        var set = GridManager.GetPositionSet(finalPos);
        if(set != null)
        {
            bool playerFound = false;
            foreach(var o in set)
            {
                if (o is PlayerGridOccupant)
                {
                    playerFound = true;
                    break;
                }
            }

            if(playerFound)
            {
                AttackPlayer();
                return;
            }
        }

        self.MoveInDir(dir);
    }

    protected void AttackPlayer()
    {
        Invoke("AttackPlayerAction", 0.03f);
    }
    void AttackPlayerAction()
    {
        Vector2 dir = (player.gridPos - self.gridPos);
        dir.Normalize();

        transform.DOComplete();
        transform.DOPunchPosition(dir * 0.6f, 0.2f, 0, 0).SetEase(Ease.InBack);

        rend.flipX = (dir.x < 0) ? false : true;

        AudioSystem.Instance.Play("EOnAttack");
        player.TakeDamage(damage);
    }
}
