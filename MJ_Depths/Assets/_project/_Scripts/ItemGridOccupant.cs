using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridOccupant : GridOccupant
{
    public ItemData itemData;
    public SpriteRenderer rend;

    public override void Awake()
    {
        base.Awake();
        rend.sprite = itemData.sprite;
    }

    public void PickUp(Player player)
    {
        player.AddItem(itemData);
        GridManager.RemoveOccupantFromDictionary(this);
        Destroy(gameObject);
    }
}
