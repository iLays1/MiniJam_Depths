using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorItemUI : MonoBehaviour
{
    public ItemUIContainer UIContainer;
    public ItemGridOccupant itemPrefab;

    PlayerGridOccupant player;
    Vector2Int gridPos;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGridOccupant>();
        if (player == null)
            Destroy(this);

        GameEvents.OnPlayerMove.AddListener(UpdateFloorItems);
        GameEvents.OnItemMoved.AddListener(UpdateMovedItems);
    }
    private void Start()
    {
        UpdateFloorItems();
    }

    public void UpdateFloorItems()
    {
        if (player == null)
        {
            Destroy(this);
            return;
        }
        
        for (int i = 0; i < UIContainer.slots.Length; i++)
            UIContainer.slots[i].LoadData(null);

        var set = GridManager.GetPositionSet(player.gridPos);
        if (set == null) return;

        var count = 0;
        foreach(var groundItem in set)
        {
            if (count >= UIContainer.slots.Length)
                break;

            if(groundItem is ItemGridOccupant)
            {
                UIContainer.slots[count].LoadData((groundItem as ItemGridOccupant).itemData);
                count++;
            }
        }

        gridPos = player.gridPos;
    }

    void UpdateMovedItems()
    {
        var set = GridManager.GetPositionSet(player.gridPos);
        if (set == null) return;

        List<GridOccupant> list = new List<GridOccupant>();

        foreach (var groundItem in set)
            if (groundItem is ItemGridOccupant)
                list.Add(groundItem);

        foreach(var groundItem in list.ToArray())
        {
            GridManager.RemoveOccupantFromDictionary(groundItem);
            Destroy(groundItem.gameObject);
        }

        for (int i = 0; i < UIContainer.slots.Length; i++)
        {
            var slot = UIContainer.slots[i];

            if(slot.data != null)
            {
                var itemGo = Instantiate(itemPrefab);
                var item = itemGo.GetComponent<ItemGridOccupant>();
                item.SetPositionInGrid(gridPos, true);
                item.itemData = slot.data;
            }
        }
    }
}
