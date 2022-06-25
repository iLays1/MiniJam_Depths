using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridOccupant : GridOccupant
{
    public ItemData itemData;
    public SpriteRenderer rend;
    
    private void Start()
    {
        rend.sprite = itemData.sprite;
    }
}
