using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControleur : MonoBehaviour
{ 
    [SerializeField] public enum Mode { Infiltration, Infraction };

    public Button BTNParameters;

    public Canvas Menu;
    public Canvas Parameters;

    public void Start()
    {
        this.Menu.gameObject.SetActive(true);
        this.Parameters.gameObject.SetActive(false);
        try
        {
            this.BTNParameters.onClick.AddListener(GoToParameters);
        }
        catch
        { }
    }

    public void ChargeScene(string _NameScene)
    {
        SceneManager.LoadScene(_NameScene);
        Time.timeScale = Timer.TimeScaleTimer;
        GrappinSystem.InitLesGrappins = true;
    }

    public void QuitMyGame()
    {
        Application.Quit();
    }

    

    public void GoToParameters()
    {
        Debug.Log("Test");
        this.Menu.gameObject.SetActive(false);
        this.Parameters.gameObject.SetActive(true);
    }

}
