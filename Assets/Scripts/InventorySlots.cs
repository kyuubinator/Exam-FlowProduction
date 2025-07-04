using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventorySlots : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool _requireType = false;
    [SerializeField] private ItemSO.ItemType _type;

    public bool RequireType { get => _requireType; }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Item item = eventData.pointerDrag.GetComponent<Item>();     // If the Slot does not have an item
            if (_requireType)                                           // And requires a type
            {
                if (item.Type != _type)                                 // If the type is not the same as the slot, stop the method
                {
                    return;
                }
                AddStatChanges(item);                                   // If its the same type, Add the Stat changes to the player
            }
            item.ParentAfterDrag = transform;                           // Assign the item to the slot
            return;
        }
    }

    public void AddStatChanges(Item item)
    {
        int maxHP = item.GetMaxHP();
        int damage = item.GetDamage();                              // Save stats as local variables and send them all together
        int Speed = item.GetSpeed();
        ApplyStatChanges(maxHP, damage, Speed);
    }

    public void RemoveStatChanges(Item item)
    {
        int maxHP = -item.GetMaxHP();
        int damage = -item.GetDamage();                             // Save stats as local variables and send them all together
        int Speed = -item.GetSpeed();
        ApplyStatChanges(maxHP, damage, Speed);
    }

    public void ApplyStatChanges(int maxHP, int damage, int speed)
    {
        GameManager.Instance.Player.Maxhp += maxHP;
        GameManager.Instance.Player.Damage += damage;               // Apply the stat changes and update the UI
        GameManager.Instance.Player.Speed += speed;
        UIManager.instance.UpdateAllUI();
    }
}
