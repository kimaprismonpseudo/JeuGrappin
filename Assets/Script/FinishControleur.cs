using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishControleur : MonoBehaviour
{
    public Canvas Loose;
    public Canvas Win;
    public Canvas Chrono;
    public Canvas Menu;

    public Button Rejouer;
    public Button MenuPrincipal;

    public static int Finish = 0; // 0: Rien , 1: Win, 2: Loose

    public void Start()
    {
        try
        {
            this.Rejouer.onClick.AddListener(RejouerGame);
            this.MenuPrincipal.onClick.AddListener(RetourMenu);
        }
        catch
        { }
    }

    public void Update()
    {
        if(Finish == 1)
        {
            SetWin();
        }
        else if (Finish == 2)
        {
            SetLoose();
        }


    }

    public void SetLoose()
    {
        this.Loose.gameObject.SetActive(false);
        this.Win.gameObject.SetActive(true);
        FermeReste();
    }

    public void SetWin()
    {
        this.Loose.gameObject.SetActive(false);
        this.Win.gameObject.SetActive(true);
        FermeReste();
    }

    private void FermeReste()
    {
        this.Menu.gameObject.SetActive(false);
        this.Chrono.gameObject.SetActive(false);
        Timer.TimerActive = false;
        Time.timeScale = 0;
    }

    public void RejouerGame()
    {
        SceneManager.LoadScene("LVL1");
    }

    public void RetourMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = Timer.TimeScaleTimer;
        Timer.TimerActive = true;
    }
}
