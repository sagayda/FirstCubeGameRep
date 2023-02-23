using Unity.VisualScripting;

public interface IInventoryItemState
{
    int Amount { get; set; }
    bool IsEquipped { get; set; }

    IInventoryItemState Clone();
}

