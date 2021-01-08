using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public int CurrentHealth
    {
        get { return health; }
    }

    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }
    public void TakeHit(int damage)
    {//Получение дамага, если хелс = 0 то дестрой
        health -= damage;
        Debug.Log(health);
        if(health <= 0 ) 
            Destroy(gameObject);
    }
    
    public void setHealth(int Recovery)
    {//Восстановление ХП до 150
        health += Recovery;
        if(health > 150)
            health = 150;
    }

    
}
