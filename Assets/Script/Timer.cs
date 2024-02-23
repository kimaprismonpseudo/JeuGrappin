using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Text TempsGraphique;
    private const float TempsParDefaut = 300;
    public static float TempsCalcul = TempsParDefaut;
    public static float TimeScaleTimer = 1;


    public static bool TimerActive = true;


    public static float TempsCalculP
    {
        get => TempsCalcul; 
        set
        {
            TempsCalcul -= value;
            if (value == 0)
                TempsCalcul = TempsParDefaut;

            TempsGraphique.text = TimerVersString();
            if (TempsCalcul <= 0)
            {
                FinishControleur.Finish = 2; // Loose temps écoulé 
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        TempsGraphique = GetComponent<Text>();
        ResetChrono();
    }

    // Update is called once per frame
    void Update()
    {
        TempsCalculP = TimerActive ? Time.deltaTime : 0;

   }

    public static void ResetChrono()
    {
        TempsGraphique.text = TimerVersString();
        TempsCalculP = 0;
    }

    public static string TimerVersString()
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
