using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/Gain Fuel")]
public class IE_FuelRefill : ItemEffect
{
    public int fuelGainAmount;

    public override void Use(PlayerGridOccupant player, Vector2Int targetGridPos)
    {
        AudioManager.Instance.Play("PItemUsed");

        Player.Instance.GainFuel(fuelGainAmount);
    }
}
