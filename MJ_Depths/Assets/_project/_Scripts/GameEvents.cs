using UnityEngine.Events;

public static class GameEvents
{
    //Combat
    public static UnityEvent OnEndTurn = new UnityEvent();
    public static UnityEvent OnPlayerMove = new UnityEvent();
}
