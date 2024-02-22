using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControleur : MonoBehaviour
{ 
    [SerializeField] public enum Mode { Infiltration, Infraction };

    [SerializeField] public Mode ModeJeu;
    public Button BTNParameters;

    public Canvas Menu;
    public Canvas Parameters;

    public void Start()
    {
        try
        {
            this.BTNParameters.onClick.AddListener(GoToParameters);
        }
        catch
        { }
        this.Menu.gameObject.SetActive(true);
        this.Parameters.gameObject.SetActive(false);
    }

    public void ChargeScene(string _NameScene)
    {
        SceneManager.LoadScene(_NameScene);
    }

    public void QuitMyGame()
    {
        Application.Quit();
    }

    

    public void GoToParameters()
    {
        this.Menu.gameObject.SetActive(false);
        this.Parameters.gameObject.SetActive(true);
    }

}
