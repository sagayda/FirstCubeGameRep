using Assets.Scripts;
using Assets.Scripts.Inventory;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public InventoryItemInfo apple;
    public InventoryItemInfo pepper;
    public void SetRandomItems(InventoryWithSlots inventory)
    {
        inventory.slots[0].SetItem(new ItemInSlot(apple, new InventoryItemState { Amount = 15} ));
        inventory.slots[0].isChanged = true;
        inventory.slots[4].SetItem(new ItemInSlot(pepper));
        inventory.slots[4].isChanged = true;
        inventory.slots[10].SetItem(new ItemInSlot(pepper, new InventoryItemState { Amount = 9 }));
        inventory.slots[10].isChanged = true;
    }
}

