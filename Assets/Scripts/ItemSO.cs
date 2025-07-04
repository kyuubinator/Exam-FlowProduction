using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Invetory/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;

    [SerializeField] private ItemType _type;          //holds the type of item for itemTypeSlots

    public string Name { get => _name; }
    public Sprite Sprite { get => _sprite; }
    public int MaxHp { get => _maxHp; }
    public int Damage { get => _damage; }
    public int Speed { get => _speed; }
    public ItemType Type { get => _type; }

    [HideInInspector]
    public enum ItemType{
        Item,
        Consumable,
        Helmet,
        Chestplate,
        Leggins,
        Boots
    }
}
