using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
    public ItemUISlot handSlot;
    public TargettingIcon[] targetingIcons;
    PlayerGridOccupant player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGridOccupant>();
        GameEvents.OnItemMoved.AddListener(UpdateTargetIcons);
        GameEvents.OnPlayerMove.AddListener(UpdateTargetIcons);

        GameEvents.OnTargetIconClicked.AddListener(TargetClicked);
        GameEvents.OnLevelEnd.AddListener(HideTargetIcons);
    }

    private void TargetClicked(Vector2Int pos)
    {
        if (handSlot.data != null)
        {
            if (!Player.Instance.EnoughFuel(handSlot.data.cost))
                return;

            Player.Instance.SpendFuel(handSlot.data.cost);

            if(handSlot.data.effect != null)
            {
                handSlot.data.effect.Use(player, pos);

                if(handSlot.data.oneTimeUse)
                {
                    handSlot.LoadData(null);
                    UpdateTargetIcons();
                }
                
                GameEvents.OnPlayerAct.Invoke();
            }
        }
    }

    private void UpdateTargetIcons()
    {
        HideTargetIcons();

        if (Player.Instance.fuel <= 0)
            return;
        
        if (handSlot.data != null && !(handSlot.data.effect is IE_Weapon))
        {
            var t = targetingIcons[0];
            t.gameObject.SetActive(true);
            t.transform.position = (Vector2)player.gridPos;
            return;
        }

        if (handSlot.data != null && handSlot.data.effect is IE_Weapon)
        {
            var weapon = handSlot.data.effect as IE_Weapon;
            var validTargets = GetTilesInRange(weapon.minRange, weapon.maxRange);

            int count = 0;
            foreach(var pos in validTargets)
            {
                if(count >= targetingIcons.Length)
                    break;

                var t = targetingIcons[count];
                t.gameObject.SetActive(true);
                t.transform.position = (Vector2)pos;
                count++;
            }
            return;
        }
    }

    private List<Vector2Int> GetTilesInRange(int minRange, int maxRange)
    {
        var square = new List<Vector2Int>();
        for (int i = 0; i <= maxRange; ++i)
        {
            for (int j = 0; j <= maxRange; ++j)
            {
                if (i == 0 && j == 0) continue;
                square.Add(new Vector2Int(player.gridPos.x + i, player.gridPos.y + j));
                square.Add(new Vector2Int(player.gridPos.x - i, player.gridPos.y - j));
                if (i == 0 || j == 0) continue;
                square.Add(new Vector2Int(player.gridPos.x + i, player.gridPos.y - j));
                square.Add(new Vector2Int(player.gridPos.x - i, player.gridPos.y + j));
            }
        }

        var finalTargets = new List<Vector2Int>();
        foreach (var pos in square)
        {
            var diff = player.gridPos - pos;
            var dis = Mathf.Abs(diff.x) + Mathf.Abs(diff.y);
            if (dis <= maxRange && dis >= minRange)
            {
                finalTargets.Add(pos);
            }
        }

        return finalTargets;
    }

    private void HideTargetIcons()
    {
        foreach (var t in targetingIcons)
        {
            t.gameObject.SetActive(false);
        }
    }
}
