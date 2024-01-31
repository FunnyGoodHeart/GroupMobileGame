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
    public bool isEasy = false;
    public bool isNormal = false;
    public bool isHard = false;

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

    // Activates difficulty 
    public void Easy()
    {
        isEasy = true;
        SceneManager.LoadScene(1);
        Debug.Log("easy");
    }

    public void Normal()
    {
        isNormal = true;
        SceneManager.LoadScene(1);
        Debug.Log("normal");
    }

    public void Hard()
    {
        isHard = true;
        SceneManager.LoadScene(1);
        Debug.Log("hard");
    }
}
