using Assets.Scripts.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create new ItemInfo")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private int _maxStackSize;
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _spriteIcon;

    public int maxStackSize => _maxStackSize;

    public string id => _id;

    public string title => _title;

    public string description => _description;

    public Sprite spriteIcon => _spriteIcon;
}
