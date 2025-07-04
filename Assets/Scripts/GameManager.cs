using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Inventory _inventory;
    
    private Player _player;
    public Player Player { get => _player; set => _player = value; }

    private Action<int> _onMoneyChange;
    public Action<int> OnMoneyChange { get => _onMoneyChange; set => _onMoneyChange = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
                                                                     // Singleton :)
        }
        else
            Destroy(this.gameObject);
    }

    public void AddMoney(int money)
    {
        _player.Money += money;                                     // Increase or decrease the player money and call every listener
        OnMoneyChange.Invoke(_player.Money);
    }

    public void VendorSoldItem(Item item)
    {
        _inventory.AddItemFromVendor(item);                         // Send which item was sold by the vendor to the inventory
    }

    public void SetPlayer(Player player)
    {
        _player = player;                                           // Receive the player character
    }
}
