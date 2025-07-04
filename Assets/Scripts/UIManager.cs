using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _playerDamageText;
    [SerializeField] private TextMeshProUGUI _playerSpeedText;
    [SerializeField] private TextMeshProUGUI _playerMoneyText;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _vendor;


    public GameObject Inventory { get => _inventory; set => _inventory = value; }
    public GameObject Vendor { get => _vendor; set => _vendor = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
                                                                     // Singleton :)
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnMoneyChange += UpdateMoneyUI;    // Update the UI on start, check if gameManager instance for the main menu
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void AddMoneyButton(int amount)
    {
        GameManager.Instance.AddMoney(amount);                  // Add or Remove money
    }

    public void InventoryButton()
    {
        _inventory.SetActive(!_inventory.activeSelf);           // Open or close Inventory
        UpdateAllUI();
    }

    public void VendorButton()
    {
        _vendor.SetActive(!_vendor.activeSelf);                 // Open or close Vendor
        UpdateAllUI();
    }

    public void UpdateAllUI()
    {
        UpdateHealthUI();
        UpdateDamageUI();                                       // UpdateAllUi
        UpdateSpeedUI();
        UpdateMoneyUI(GameManager.Instance.Player.Money);
    }

    public void UpdateHealthUI()
    {
        _playerHealthText.text = $"Health: {GameManager.Instance.Player.Hp}/{GameManager.Instance.Player.Maxhp}";           // Update Health UI
    }

    public void UpdateDamageUI()
    {
        _playerDamageText.text = $"Damage: {GameManager.Instance.Player.Damage}";                                           // Update Damage UI
    }

    public void UpdateSpeedUI()
    {
        _playerSpeedText.text = "Speed: " + GameManager.Instance.Player.Speed;                                              // Update Speed UI
    }
    public void UpdateMoneyUI(int money)
    {
        _playerMoneyText.text = "Money: " + money;                                                                          // Update Money UI
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseMenu()
    {
         _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        if (_pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
