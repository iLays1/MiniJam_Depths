using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public EnemyBehavior[] possibleEnemies;

    private void Awake()
    {
        var enemy = Instantiate(possibleEnemies.GetRandomElement());
        enemy.self.SetPositionInGrid(GridManager.WorldToGridPos(transform.position), true);
    }
}
