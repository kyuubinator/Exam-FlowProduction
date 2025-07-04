using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlots[] _inventoryArray;      // Holds the inventory slots to that we can buy items from the vendor without them returning to the vendor

    public void AddItemFromVendor(Item vendorItem)
    {
        foreach (var itemSlot in _inventoryArray)
        {
            if (itemSlot.GetComponentInChildren<Item>() == null)                    //
            {                                                                       // Assigns the vendor item to an available item slot
                vendorItem.transform.SetParent(itemSlot.gameObject.transform);      //
                vendorItem.ParentAfterDrag = itemSlot.gameObject.transform;         //
            }
        }
    }
}
