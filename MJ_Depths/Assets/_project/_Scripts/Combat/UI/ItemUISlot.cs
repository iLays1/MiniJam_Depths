using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemUISlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static UnityEvent OnSlotHoverEnter = new UnityEvent();
    public static UnityEvent OnSlotHoverExit = new UnityEvent();

    public static ItemUISlot hoveredSlot;

    public ItemUIContainer currentContainer;

    public ItemData data = null;
    public Image spriteImage;
    public Sprite blankSprite;

    Camera cam;
    bool dragging = false;
    
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        LoadData(data);
    }

    public void LoadData(ItemData data)
    {
        this.data = data;

        if(data != null)
        {
            spriteImage.sprite = data.sprite;
            return;
        }

        spriteImage.sprite = blankSprite;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(data != null)
            dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging) return;

        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        spriteImage.transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!dragging) return;

        dragging = false;
        spriteImage.transform.localPosition = Vector3.zero;

        if (hoveredSlot == null || hoveredSlot == this)
        {
            return;
        }
        
        var newData = hoveredSlot.data;
        hoveredSlot.LoadData(data);
        LoadData(newData);
        GameEvents.OnItemMoved.Invoke();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveredSlot = this;
        OnSlotHoverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveredSlot = null;
        OnSlotHoverExit.Invoke();
    }
}
