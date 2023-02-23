

using System;

public interface IInventory
{
    int capacity { get; }
    public bool isOpen { get; }
    public bool isFull { get; }
    public void OpenInventoryInUi(UIInventory uiInventory);
    public void CloseInventoryInUi(UIInventory uiInventory);
}
