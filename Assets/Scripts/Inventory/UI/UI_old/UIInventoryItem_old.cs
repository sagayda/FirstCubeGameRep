using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem_old : UIItem_old
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI textAmount;

    public IInventoryItem item { get; private set; }

    public void Refresh (IInventorySlot slot)
    {
        if (slot.isEmpty)
        {
            CleanUp();
            return;
        }

        item = slot.item;
        imageIcon.sprite = item.info.spriteIcon;
        imageIcon.gameObject.SetActive(true);
        textAmount.gameObject.SetActive(true);
        textAmount.text = $"{slot.Amount}/{slot.Capacity}";
    }


    private void CleanUp()
    {
        textAmount.gameObject.SetActive(false);
        imageIcon.gameObject.SetActive(false);
    }
}
