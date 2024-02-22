using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuJeuControlleur : MonoBehaviour
{
    public Button BTNJouer;
    public Button BTNMenu;

    public Canvas Menu;
    public Canvas Chrono;

    public void Start()
    {
        try
        {
            this.BTNJouer.onClick.AddListener(GoToJeu);
            this.BTNMenu.onClick.AddListener(GoToMenu);
        }
        catch
        { }
    }

    public void ChargeScene(string _NameScene)
    {
        SceneManager.LoadScene(_NameScene);
    }

    public void QuitMyGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        Timer.TimerActive = true;
    }

    public void GoToJeu()
    {
        this.Menu.gameObject.SetActive(false);
        this.Chrono.gameObject.SetActive(true);
        Timer.TimerActive = true;
        Time.timeScale = 1;
    }
}
