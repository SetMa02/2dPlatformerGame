using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager Instance { get; private set; }
    #endregion

    [SerializeField] private GameObject inventoryPanel;
    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciver> buffReciveContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    public PlayerInventory inventory;
    public ItemBase itemDataBase;

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffReciveContainer = new Dictionary<GameObject, BuffReciver>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
    }

    public void OnPauseClick()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else
        { 
    
        Time.timeScale = 1;
        }
    }

    public void OnInventoryClick()
    {
        if (Time.timeScale > 0)
        {
            inventoryPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            inventoryPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
