using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    [TextArea]
    public string itemDesc;
    public Sprite sprite;

    [Space]
    public bool oneTimeUse = false;
    public int cost = 0;
    public ItemEffect effect;
}
