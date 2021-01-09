using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCooldown : MonoBehaviour
{
    public Image FireImage;
    public float cooldown = 2;
    bool isCooldown = false;
    [SerializeField] private Button fireButton;

    void Start()
    {
        FireImage.fillAmount = 1;
    }
   
    public void OnFireClick()
    {
        if (isCooldown == true)
            return;
        else
            isCooldown = true;
            
        
    }
    private void Update()
    {
        if(isCooldown)
        {
            FireImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (FireImage.fillAmount <= 0)
            {
                FireImage.fillAmount = 1;
                isCooldown = false;
            }
        }
    }
}
