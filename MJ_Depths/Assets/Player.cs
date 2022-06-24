using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public List<ItemData> inventory = new List<ItemData>();
    PlayerGridOccupant gridOccupant;

    protected override void Awake()
    {
        base.Awake();

        gridOccupant = GetComponent<PlayerGridOccupant>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var set = GridManager.GetPositionSet(gridOccupant.gridPos);
            if (set != null)
            {
                foreach (var o in set)
                {
                    if (!(o is ItemGridOccupant)) continue;

                    (o as ItemGridOccupant).PickUp(this);
                    break;
                }
            }
        }
    }

    public void AddItem(ItemData item)
    {
        inventory.Add(item);
    }
}
