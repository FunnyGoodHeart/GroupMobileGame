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
        SceneManager.LoadScene(1);
    }

    public void Normal()
    {
        SceneManager.LoadScene(4);
    }

    public void Hard()
    {
        SceneManager.LoadScene(7);
        Debug.Log("hard");
    }
}
