using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargettingIcon : MonoBehaviour
{
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameEvents.OnTargetIconClicked.Invoke(GridManager.WorldToGridPos(transform.position));
        }
    }
}
