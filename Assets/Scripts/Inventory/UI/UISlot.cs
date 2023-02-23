using Assets.Scripts.Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public IInventorySlot inventorySlot { get; private set; }
    private UIInventory uiInventory;
    private GameObject uiItem;
    private Image slotImage;
    private Image itemIcon;
    private TextMeshProUGUI itemText;
    private Color32 colorDefault = Color.white;
    private Color32 colorHighlighted = new(215, 215, 215, 255);
    
    private void Awake()
    {
        slotImage = GetComponent<Image>();
        uiItem = transform.GetChild(0).gameObject;
        itemText = uiItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemIcon = uiItem.transform.GetChild(0).GetComponent<Image>();
    }
    public void SetUiInventory(UIInventory inventory)
    {
        uiInventory = inventory;
    }
    public void SetSlot(IInventorySlot inventorySlot)
    {
        this.inventorySlot = inventorySlot;
        this.inventorySlot.isChanged = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiInventory.mouseSlot.Slot.isEmpty)
        {
            InventoryHandler.TransitFromSlotToSlot(this, inventorySlot, uiInventory.mouseSlot.Slot);
            uiInventory.mouseSlot.Slot.isChanged = true;
            inventorySlot.isChanged= true;
        }
        else
        {
            InventoryHandler.TransitFromSlotToSlot(this, uiInventory.mouseSlot.Slot, inventorySlot);
            uiInventory.mouseSlot.Slot.isChanged = true;
            inventorySlot.isChanged= true;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        slotImage.color = colorHighlighted;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        slotImage.color = colorDefault;
    }
    public void Refresh()
    {
        if (uiItem == null)
        {
            Debug.Log("null uiItem");
            return;
        }

        if (inventorySlot.isEmpty)
        {
            uiItem.SetActive(false);
            inventorySlot.isChanged = false;
            return;
        }
        uiItem.SetActive(true);
        itemIcon.sprite = inventorySlot.item.info.spriteIcon;
        itemText.SetText($"{inventorySlot.item.state.Amount}/{inventorySlot.item.info.maxStackSize}");
        inventorySlot.isChanged = false;
    }
}

