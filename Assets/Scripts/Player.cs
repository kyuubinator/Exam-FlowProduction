using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxhp;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private int _money;

    public int Hp { get => _hp; set => _hp = value; }
    public int Maxhp { get => _maxhp; set => _maxhp = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public int Speed { get => _speed; set => _speed = value; }
    public int Money { get => _money; set => _money = value; }

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);                   //Give this player to the GameObject
    }

}
