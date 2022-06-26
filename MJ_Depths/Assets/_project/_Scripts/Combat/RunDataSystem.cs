using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDataSystem : SingletonPersistent<RunDataSystem>
{
    public int startingFuel;
    public int remainingFuel;
    public List<ItemData> startingItems = new List<ItemData>();
    public List<ItemData> items = new List<ItemData>();

    protected override void Awake()
    {
        base.Awake();
        ResetData();
    }

    public void ResetData()
    {
        remainingFuel = startingFuel;
        items.Clear();
        foreach(var i in startingItems)
        {
            items.Add(i);
        }
        SceneSystem.Instance.currentLevel = 0;
    }

    public void LoadData()
    {
        if (Player.Instance == null) return;

        Player.Instance.fuel = remainingFuel - 1;
        Player.Instance.GainFuel(1);

        for (int i = 0; i < Player.Instance.inventory.slots.Length; i++)
        {
            var slot = Player.Instance.inventory.slots[i];
            slot.data = items[i];
        }

        GameEvents.OnItemMoved.Invoke();
    }

    public void SaveData()
    {
        if (Player.Instance == null) return;

        remainingFuel = Player.Instance.fuel;

        items.Clear();
        foreach(var slot in Player.Instance.inventory.slots)
        {
            items.Add(slot.data);
        }
    }
}
