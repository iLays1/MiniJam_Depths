using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    public ItemData[] possibleItems;
    public ItemGridOccupant occapantPrefab;
    private void Awake()
    {
        var item = Instantiate(occapantPrefab);
        item.SetPositionInGrid(GridManager.WorldToGridPos(transform.position), true);
        item.itemData = possibleItems.GetRandomElement();
    }
}
