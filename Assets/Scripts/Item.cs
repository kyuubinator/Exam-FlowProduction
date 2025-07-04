using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Scriptable Object")]
    [SerializeField] private ItemSO _item;

    [Header("Variables")]
    private string _name;
    private ItemSO.ItemType _type;
    [SerializeField] private Image _image;

    private Transform _parentAfterDrag;

    private InventorySlots _tempSlot;
    private InventorySlots _previousSlot;
    public ItemSO.ItemType Type { get => _type; }
    public Transform ParentAfterDrag { get => _parentAfterDrag; set => _parentAfterDrag = value; }

    private void Start()
    {
        InitialiseItem(_item);
    }

    private void InitialiseItem(ItemSO item)
    {
        _name = item.Name;
        _type = item.Type;                                              // Get ScriptableObject Info
        _image.sprite = item.Sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        VendorItemSlot vendorSlot = GetComponentInParent<VendorItemSlot>();
        if (vendorSlot != null)
        {
            vendorSlot.SoldItem(this);                                  // If the item is a vendor item, Mark as sold and assign it to a inventory slot
        }
        _previousSlot = GetComponentInParent<InventorySlots>();
        _image.raycastTarget = false;
        _parentAfterDrag = transform.parent;                            // Sets the _parentAfterDrag to the current slot and then deparent the object,
        transform.SetParent(transform.root);                            // this way it is always above everything and is not hidden
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;                       // Make items position follow mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetParent(_parentAfterDrag);                          // Can grab item again and set our parent to the assigned _parentAfterdrag
        _tempSlot = GetComponentInParent<InventorySlots>();
        if (_tempSlot != _previousSlot && _previousSlot.RequireType)
        {
            _tempSlot.RemoveStatChanges(this);                          // If the slot is not equal to the previous and is a RequireType, Remove the previosly applied stat changes
        }
    }

    public int GetMaxHP()   // Get MaxHP value from item
    {
        return _item.MaxHp;
    }
    public int GetDamage()  // Get Damage value from item
    {
        return _item.Damage;
    }
    public int GetSpeed()   // Get Speed value from item
    {
        return _item.Speed;
    }
}
