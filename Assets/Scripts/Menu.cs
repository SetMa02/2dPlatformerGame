using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameField;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("Player_name"))
            nameField.text = PlayerPrefs.GetString("Player_name");
    }

    public void OnEndEditName()
    {
        PlayerPrefs.SetString("Player_name", nameField.text);
    }

   
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickLvl1()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickLvl2()
    {
        SceneManager.LoadScene(2);
    }
}
