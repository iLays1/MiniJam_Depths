using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescriptionBox : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI bodyText;

    private void Awake()
    {
        ItemUISlot.OnSlotHoverEnter.AddListener(UpdateUI);
        ItemUISlot.OnSlotHoverExit.AddListener(UpdateUI); 
    }
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(ItemUISlot.hoveredSlot == null || ItemUISlot.hoveredSlot.data == null)
        {
            headerText.text = "";
            bodyText.text = "";
            return;
        }
        var data = ItemUISlot.hoveredSlot.data;

        headerText.text = data.itemName;
        bodyText.text = data.itemDesc;
    }
}
