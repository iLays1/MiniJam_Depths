using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite sprite;

    [Space]
    public int cost;
    public ItemEffect effect;
}
