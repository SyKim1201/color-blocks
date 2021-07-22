using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    
    public GameObject mainMenu;
    public GameObject gameScreen;
    public GameObject gameOver;

    public void play()
    {
        mainMenu.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void over()
    {
        gameScreen.SetActive(false);
        gameOver.SetActive(true);
    }

    void Start()
    {
        instance = GetComponent<MainMenuController>();

        gameScreen.SetActive(false);
        gameOver.SetActive(false);

    }
}
