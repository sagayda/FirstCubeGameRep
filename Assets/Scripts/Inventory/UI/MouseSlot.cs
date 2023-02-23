using UnityEngine;

public class MouseSlot
{
    public UISlot UISlot { get; set; }
    public InventorySlot Slot { get; set; }

    public MouseSlot(UIInventory inventory, UISlot uiSlot)
    {
        Slot = new InventorySlot();
        this.UISlot = uiSlot;
        uiSlot.SetUiInventory(inventory);
        uiSlot.SetSlot(Slot);
        Slot.isChanged= true;
    }
}

