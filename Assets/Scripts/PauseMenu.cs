using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Text soundText;
    private int soundPhase;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("Sound_index"))
        {
            soundPhase = PlayerPrefs.GetInt("Sound_index");
            if(soundPhase == 0)
                soundText.text = "Звук: выкл";
            if(soundPhase == 1)
                soundText.text = "Звук: вкл";
        }
        else
        {
            PlayerPrefs.SetInt("Sound_index", 1);
            soundText.text = "Звук: вкл";
        }


    }

    public void OnSoundClick()
    {
        soundPhase = PlayerPrefs.GetInt("Sound_index");

            if (soundPhase == 0)
            { 
                PlayerPrefs.SetInt("Sound_index", 1);
                soundText.text = "Звук: вкл";
            }

            else if (soundPhase == 1)
            {
                PlayerPrefs.SetInt("Sound_index", 0);
                soundText.text = "Звук: выкл";
            }
        
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }
    public void OnExitClick()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
