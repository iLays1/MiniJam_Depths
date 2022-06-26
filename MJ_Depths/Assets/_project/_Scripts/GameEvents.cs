using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    //Combat
    public static UnityEvent OnEndTurn = new UnityEvent();
    public static UnityEvent OnPlayerMove = new UnityEvent();
    public static UnityEvent OnPlayerAct = new UnityEvent();
    public static UnityEvent OnItemMoved = new UnityEvent();
    public static UnityEvent OnEnemyDeath = new UnityEvent();
    
    public static UnityEvent OnLevelEnd = new UnityEvent();
    public static UnityEvent OnLevelWin = new UnityEvent();

    public class Vector2IntEvent : UnityEvent<Vector2Int> { }
    public static Vector2IntEvent OnTargetIconClicked = new Vector2IntEvent();
}
