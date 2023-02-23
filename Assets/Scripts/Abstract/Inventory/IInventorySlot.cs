using System;

public interface IInventorySlot
{
    IInventory inventory { get; }
    bool isFull { get; }
    bool isEmpty { get; }
    IInventoryItem item { get; }
    string itemId { get; }
    int Amount { get; set; }
    int Capacity { get; }
    bool isChanged { get; set; }
    void SetItem(IInventoryItem item);
    void ReplaceItem(IInventoryItem item);
    IInventoryItem ReplaceItem(IInventoryItem item, bool returnItem = true);
    void Clear();
}
