using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WealthManager : Singleton<WealthManager>
{
    public float Wealth;

    public Action OnWealthUpdated;

    public void IncreaseWealth(float amount)
    {
        Wealth += amount;
        OnWealthUpdated?.Invoke();
    }

    public void DecreaseWealth(float amount)
    {
        Wealth = Mathf.Clamp(Wealth - amount, 0f, Wealth);
        OnWealthUpdated?.Invoke();
    }

    public bool DoesHaveEnoughWealth(float amount)
    {
        return Wealth >= amount;
    }
}
