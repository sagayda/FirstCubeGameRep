using Assets.Scripts.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory_old : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo appleInfo;
    [SerializeField] private InventoryItemInfo pepperInfo;


    public InventoryWithSlots inventory => tester.inventory;
    private UIInventoryTester tester;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot_old>();
    }
}
