using Assets.Scripts.Inventory;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIInventorySlot_old : UISlot_old
{
    [SerializeField] private UIInventoryItem_old uiInventoryItem;
    public IInventorySlot slot { get; private set; }

    private UIInventory_old uiInventory;

    private void Awake()
    {
        uiInventory= GetComponentInParent<UIInventory_old>();
    }

    public void SetSlot(IInventorySlot newSlot)
    {
        slot = newSlot;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem_old>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot_old>();
        var otherSlot = otherSlotUI.slot;
        var inventory = uiInventory.inventory;

        InventoryHandler.TransitFromSlotToSlot(this, otherSlot, slot);
        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (slot != null)
            uiInventoryItem.Refresh(slot);
    }

}
