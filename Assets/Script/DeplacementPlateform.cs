using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPlateform : MonoBehaviour
{

    public enum Direction { GaucheDroite, HautBas };
    public enum Depart { Normal, Inverser, Milieu };

    public Depart ModeDeplacement;
    public Direction Deplacements;
    public float Ecart;
    public float Avancement;
    public bool InversementEcart;

    private Vector2 PosInit;
    private Vector2 Pos1;
    private Vector2 Pos2;

   
    // Start is called before the first frame update
    void Start()
    {
        this.PosInit = transform.position;
        float Ecart =  this.Ecart;
        if (ModeDeplacement == Depart.Inverser)
            Ecart *= -1;

        if (ModeDeplacement == Depart.Milieu)
            Ecart /= 2;


        switch (Deplacements)
        {
            case Direction.GaucheDroite:
                this.Pos1 = this.PosInit;
                this.Pos2 = new Vector2(PosInit.x + Ecart, PosInit.y);
                this.PosInit.x -= Ecart;
                break;

            case Direction.HautBas:
                this.Pos1 = this.PosInit;
                this.Pos2 = new Vector2(PosInit.x, PosInit.y + Ecart);
                this.PosInit.y -= Ecart;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (Deplacements)
        {
            case Direction.GaucheDroite:
                VerifCoord(ref this.Pos1.x, ref this.Pos2.x);
                break;
            case Direction.HautBas:
                VerifCoord(ref this.Pos1.y, ref this.Pos2.y);
                break;
        }
        transform.position = this.Pos1;
    }



    private void VerifCoord(ref float _Actually, ref float _End)
    {
        if(Mathf.Abs(_Actually - _End) < 0.1f)
        {
            this.Pos2 = this.PosInit;
            this.PosInit = this.Pos1;
        } else
        {
            if(_Actually > _End)
                _Actually = Mathf.Max(_Actually - this.Avancement * Time.deltaTime, _End);
            else if (_Actually < _End)
                _Actually = Mathf.Min(_Actually + this.Avancement * Time.deltaTime, _End);
        }
    }
}
