using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VendorItemSlot : InventorySlots
{
    [SerializeField] private int _price;
    [SerializeField] private TextMeshProUGUI _priceText;
    private Item _item;
    private Image _itemImage;

    private bool _itemSold = false;

    private void OnEnable()
    {
        GameManager.Instance.OnMoneyChange += UpdateMoneyVal;                   // add a listener to the OnMoneyChange event
    }

    private void Start()
    {
        _item = GetComponentInChildren<Item>();
        _itemImage = _item.GetComponent<Image>();                               // Update the price text
        _priceText.text = $"{_price.ToString()} G";

        UpdateMoneyVal(GameManager.Instance.Player.Money);
                                                                                // update the money UI and add a listener to the OnMoneyChange event
    }

    public override void OnDrop(PointerEventData eventData)
    {
        // Overrides to empty
    }

    public void UpdateMoneyVal(int val)
    {
        if (_item != null)
        {
            if (val < _price) 
            {
                _itemImage.raycastTarget = false;
                Color newColor = _itemImage.color;                              // If the player does not have enought money, deactivate the raycastTarget
                newColor.a = 0.6f;                                              // and lowers the opacity
                _itemImage.color = newColor;
            }
            else
            {
                _itemImage.raycastTarget = true;
                Color newColor = _itemImage.color;                              // If the player does have enought money, activate the raycastTarget
                newColor.a = 1f;                                                // and increases the opacity
                _itemImage.color = newColor;
            }
        }
    }

    public void SoldItem(Item item)
    {
        if (!_itemSold)
        {
            _itemSold = true;
            _item = null;                                                       // If the player bought the item, mark the item as sold, lose the reference to the item
            GameManager.Instance.AddMoney(-_price);                             // so it can't alter the item after it was sold 
            GameManager.Instance.VendorSoldItem(item);                          // and decrease the player money accourding to the amount spent
        }
    }
}
