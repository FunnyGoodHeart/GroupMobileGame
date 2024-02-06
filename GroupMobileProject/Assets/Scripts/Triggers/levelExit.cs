using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] bool isLastLevel = false;
    [Tooltip("Get scene number through build settings")]
    [SerializeField] int lastLevel = 1;
    [Tooltip("A small delay before loading into the next level")]
    [SerializeField] float loadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
