using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private Text coinsText;
    public int coinsCount;
    private List<Item> items;
    public BuffReciver buffReciver;
   
    public List<Item> Items
    {
        get { return items; }
    }
   

    private void Start()
    {
        GameManager.Instance.inventory = this;
        coinsText.text = coinsCount.ToString();
        items = new List<Item>(); 
        
    }

    /*
    private void Start()
    {
        Instance = this;
    }

    public static PlayerInventory Instance { get; set; }
 
     */
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))
        { 
        coinsCount++;
            coinsText.text = coinsCount.ToString();
        var coin = GameManager.Instance.coinContainer[col.gameObject];
        coin.StartDestroy();
        }

        if(GameManager.Instance.itemsContainer.ContainsKey(col.gameObject))
        {
            var itemComponent = GameManager.Instance.itemsContainer[col.gameObject];
            items.Add(itemComponent.Item);
            itemComponent.Destroy(col.gameObject);
        }
    }
        
    
}
