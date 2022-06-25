using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIContainer : MonoBehaviour
{
    public ItemUISlot[] slots;

    private void Awake()
    {
        foreach(var s in slots)
        {
            s.currentContainer = this;
        }
    }
}
