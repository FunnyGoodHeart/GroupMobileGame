using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartOnFall : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Falling")
        {
            playerAnimator.Play("FallingWithoutHat");
        }

        if (collision.gameObject.tag == "FallDeath")
        {
            ReloadScene();
        }

        if (collision.gameObject.tag == "TooHigh")
        {
            playerAnimator.Play("FallingWithoutHat");
            Invoke("ReloadScene", loadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
