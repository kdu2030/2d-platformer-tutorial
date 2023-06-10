using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool isLevelFinished = false;
    
    public void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !isLevelFinished)
        {
            finishSound.Play();
            isLevelFinished = true;
            Invoke("LoadNextLevel", 2f);
        }
    }
}
