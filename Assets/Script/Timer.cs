using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TempsGraphique;
    public float TempsCalcul;


    public float TempsCalculP
    {
        get => this.TempsCalcul; 
        set
        {
            this.TempsCalcul += value;
            if (value == 0)
                this.TempsCalcul = 0;

            this.TempsGraphique.text = TimerVersString();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ResetChrono();
    }

    // Update is called once per frame
    void Update()
    {
        this.TempsCalculP = Time.deltaTime;
    }

    public void ResetChrono()
    {
        this.TempsGraphique.text = "00:00";
        this.TempsCalculP = 0;
    }

    public string TimerVersString()
    {
        string sMinutes, sSecondes;
        int Minutes, Secondes;

        Minutes = (int)TempsCalculP / 60;
        Secondes = (int)TempsCalculP % 60;

        sMinutes = Minutes > 9 ? Minutes.ToString() : $"0{Minutes}" ;
        sSecondes = Secondes > 9 ? Secondes.ToString() : $"0{Secondes}";

        return $"{sMinutes}:{sSecondes}";
    }
}
