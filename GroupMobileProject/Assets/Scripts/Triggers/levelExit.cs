using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] bool isLastLevel = false;
    [Tooltip("Get scene number through build settings")]
    [SerializeField] int lastLevel = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLastLevel == true)
        {
            SceneManager.LoadScene(lastLevel);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
