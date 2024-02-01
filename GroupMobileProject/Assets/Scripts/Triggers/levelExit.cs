using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] bool isLastLevel = false;
    [Tooltip("Get scene number through build settings")]
    [SerializeField] int lastLevel = 1;
    [SerializeField] float loadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLastLevel == true)
        {
            Invoke("LastLevel", loadDelay);
        }
        else
        {
            Invoke("NextLevel", loadDelay);
        }
    }

    void LastLevel()
    {
        SceneManager.LoadScene(lastLevel);
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
