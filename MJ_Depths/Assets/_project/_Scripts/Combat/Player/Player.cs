using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>
{
    public UnityEvent OnValueChange = new UnityEvent();
    public int fuel;
    public int maxFuel;
    
    protected override void Awake()
    {
        base.Awake();

        fuel = maxFuel;
        OnValueChange.Invoke();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.OnPlayerAct.Invoke();
        }
    }

    public bool EnoughFuel(int cost)
    {
        if (cost > fuel)
            return false;
        return true;
    }

    public void GainFuel(int amount)
    {
        fuel += amount;

        if (fuel > maxFuel)
            fuel = maxFuel;

        OnValueChange.Invoke();
    }
    public void SpendFuel(int amount)
    {
        fuel -= amount;

        if(fuel <= 0)
        {
            fuel = 0;
            Debug.Log("OUT");
        }

        OnValueChange.Invoke();
    }
}
