using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Choose difficulty (Easy, Normal, Hard)
    [SerializeField] Canvas difficultyMenu;
    // Play or Quit Options
    [SerializeField] Canvas menuOptions;
    // Difficulty Options
    bool isEasy = false;
    bool isNormal = false;
    bool isHard = false;

    private void Start()
    {
        difficultyMenu.enabled = false;
        menuOptions.enabled = true;
    }
    public void Play()
    {
        difficultyMenu.enabled = true;
        menuOptions.enabled = false;
        
    }

   public void Quit()
    {
        Application.Quit();
    }

    public void Easy()
    {
        isEasy = true;
    }

    public void Normal()
    {
        isNormal = true;
    }

    public void Hard()
    {
        isHard = true;
    }
}
