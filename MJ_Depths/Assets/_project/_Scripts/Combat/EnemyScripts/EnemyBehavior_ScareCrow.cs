using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior_ScareCrow : EnemyBehavior
{
    public int range;

    protected override void Awake()
    {
        self = GetComponent<EnemyGridOccupant>();
        player = FindObjectOfType<PlayerGridOccupant>();

        GameEvents.OnPlayerAct.AddListener(() =>
        {
            var diff = player.gridPos - self.gridPos;
            var dis = Mathf.Abs(diff.x) + Mathf.Abs(diff.y);

            if (dis <= range)
            {
                ticks++;
            }
            else
            {
                ticks = 0;
            }

            if (ticks >= ticksToAct)
            {
                Act();
                ticks = 0;
            }
        });
    }

    protected override void Act()
    {
        AttackPlayer();
    }
}
