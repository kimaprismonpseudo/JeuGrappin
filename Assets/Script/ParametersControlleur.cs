using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParametersControlleur : MonoBehaviour
{
    [SerializeField] public enum Mode { Infiltration, Infraction };

    [SerializeField] public Mode ModeJeu;
    public Button ModeInfiltration;
    public Button ModeInfraction;
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
            this.BTNRetour.onClick.AddListener(GoToMenu);
        }
        catch
        { }
        this.Menu.gameObject.SetActive(true);
        this.Parameters.gameObject.SetActive(false);

        Color c = this.BPSelect.normalColor;
        c.a = 0.3f;
        this.BPSelect.normalColor = c;

        this.Correspondance = new Dictionary<Mode, Button>()
        {
            { Mode.Infiltration, this.ModeInfiltration },
            { Mode.Infraction, this.ModeInfraction }
        };


        ChangeModeInfiltration();
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
        this.ModeJeu = Mode.Infiltration;
    }

    public void ChangeModeInfraction()
    {
        this.ModeInfiltration.colors = BPSelect;
        this.ModeInfraction.colors = BSelect;
        this.ModeJeu = Mode.Infraction;
    }
}
