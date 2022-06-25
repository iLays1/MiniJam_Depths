using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/Weapon")]
public class IE_Weapon : ItemEffect
{
    public int damage;
    public int range;

    public bool gunSound = false;

    public override void Use(PlayerGridOccupant player, Vector2Int targetGridPos)
    {
        var set = GridManager.GetPositionSet(targetGridPos);
        
        Vector2 dir = (targetGridPos - player.gridPos);
        dir.Normalize();

        player.transform.DOComplete();
        player.transform.DOPunchPosition(dir * 0.6f, 0.2f, 0, 0).SetEase(Ease.InBack);

        player.playerSprite.flipX = (dir.x < 0) ? false : true;

        AudioManager.Instance.Play((gunSound) ? "GunShot": "MeleeSwing");

        if (set != null)
        {
            foreach (var o in set)
            {
                if (o is IDamageTaker)
                {
                    (o as IDamageTaker).TakeDamage(damage);
                }
            }
        }
    }
}
