using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParametersControleur : MonoBehaviour
{
    [SerializeField] public enum Mode { Infiltration, Infraction };

    [SerializeField] public static Mode ModeJeu;
    public Button ModeInfiltration;
    public Button ModeInfraction;
    public Button RalentissementAct;
    public Button RalentissementDesact;
    public Button BTNRetour;
    ColorBlock BSelect = new ColorBlock();
    ColorBlock BPSelect = new ColorBlock();

    public Canvas Menu;
    public Canvas Parameters;

    private Dictionary<Mode, Button> Correspondance;

    public void Start()
    {
        try
        {
            this.BSelect = this.ModeInfiltration.colors;
            this.BPSelect = this.ModeInfiltration.colors;
            this.ModeInfiltration.onClick.AddListener(ChangeModeInfiltration);
            this.ModeInfraction.onClick.AddListener(ChangeModeInfraction);

            this.RalentissementAct.onClick.AddListener(RalentissementActFonc);
            this.RalentissementDesact.onClick.AddListener(RalentissementDesactFonc);
            this.BTNRetour.onClick.AddListener(GoToMenu);
        }
        catch
        { }

        Color c = this.BPSelect.normalColor;
        c.a = 0.3f;
        this.BPSelect.normalColor = c;

        this.Correspondance = new Dictionary<Mode, Button>()
        {
            { Mode.Infiltration, this.ModeInfiltration },
            { Mode.Infraction, this.ModeInfraction }
        };


        ChangeModeInfiltration();
        RalentissementDesactFonc();
    }

    private Mode GetModeOfButton(Button _btn)
    {
        if (this.Correspondance[Mode.Infraction] == _btn)
            return Mode.Infraction;
        else
            return Mode.Infiltration;
    }

    public void GoToMenu()
    {
        this.Menu.gameObject.SetActive(true);
        this.Parameters.gameObject.SetActive(false);
    }

    public void ChangeModeInfiltration()
    {
        this.ModeInfiltration.colors = BSelect;
        this.ModeInfraction.colors = BPSelect;
        ModeJeu = Mode.Infiltration;
    }

    public void ChangeModeInfraction()
    {
        this.ModeInfiltration.colors = BPSelect;
        this.ModeInfraction.colors = BSelect;
        ModeJeu = Mode.Infraction;
    }

    public void RalentissementActFonc()
    {
        this.RalentissementDesact.colors = BPSelect;
        this.RalentissementAct.colors = BSelect;
        Timer.TimeScaleTimer = 0.7f;
        Time.timeScale = Timer.TimeScaleTimer;
    }

    public void RalentissementDesactFonc()
    {
        this.RalentissementDesact.colors = BSelect;
        this.RalentissementAct.colors = BPSelect;
        Timer.TimeScaleTimer = 1;
        Time.timeScale = Timer.TimeScaleTimer;
    }
}
