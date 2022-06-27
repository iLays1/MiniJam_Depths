using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    int enemyCount;
    private void Awake()
    {
        GameEvents.OnPlayerMove.AddListener(PlayerMoved);
        MusicSystem.Instance.PlaySong(Song.BattleTheme);
        GameEvents.OnEnemyDeath.AddListener(EnemyKilled);
    }

    private void Start()
    {
        enemyCount = FindObjectsOfType<EnemyGridOccupant>().Length;
    }

    void EnemyKilled()
    {
        enemyCount--;

        if(enemyCount <= 0)
        {
            GameEvents.OnLevelWin.Invoke();
            AudioSystem.Instance.Play("Victory");
            MusicSystem.Instance.PlaySong(Song.BattleClearTheme);
        }
    }

    public void PlayerMoved()
    {
        GameEvents.OnEndTurn.Invoke();
    }
}
