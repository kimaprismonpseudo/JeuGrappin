using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinSystem : MonoBehaviour
{    
    private class GrappinSysChild
    {
        public string Name;
        public LineRenderer Line;
        public DistanceJoint2D Joint;
        public static LayerMask GrappleMask;
        public Rigidbody2D RB;
        public GameObject Origine; 

        private bool isGrapplingP = false;
        public bool retracting = false;
        public bool isUnGrappling = false;

        private static bool SomeOneGrapplingP;
        private static List<GrappinSysChild> MesGrappins = new List<GrappinSysChild>();

        public static bool SomeOneGrappling
        {
            get
            {
                return SomeOneGrapplingP;
            }
        }

        public static float maxdistance = 20;
        public static float grappleSpeed = 5;
        public static float grappleShootSpeed = 60;

        public static float GrappinX = 0.2f;
        public static float GrappinY = 0.4f;
        public static float JointX = 0;//= -0.3f;
        public static float JointY = 0;//= 0.7f;
        
        public Vector3 PosGrappinStart;
        public Vector2 target;
        public Vector2 PosGrappinFin;
        public Clic KeyAssocie;

        public bool isGrappling
        {
            get
            {
                return this.isGrapplingP;
            }
            
            set
            {
                this.isGrapplingP = value;
                SomeOneGrapplingP = GetSomeOneIsGrappling();
                MouvementPerosnnage.isGrapplingPlayer = SomeOneGrappling;
            }
        }

        private bool GetSomeOneIsGrappling()
        {
            foreach (GrappinSysChild g in MesGrappins)
            {
                if (g.isGrapplingP)
                    return true;
            }
            return false;
        }

        public GrappinSysChild GetPasCeluila()
        {
            foreach (GrappinSysChild g in MesGrappins)
            {
                if (this != g)
                    return g;
            }
            return null;
        }


        public GrappinSysChild(GameObject _Grappin, string _Name, Clic _KeyAssocie)
        {
            this.Line = _Grappin.GetComponent<LineRenderer>();
            this.Joint = _Grappin.GetComponent<DistanceJoint2D>();
            this.RB = _Grappin.GetComponent<Rigidbody2D>();
            this.Joint.autoConfigureDistance = false;
            this.Joint.enableCollision = true;
            this.Origine = _Grappin;
            this.Name = _Name;
            this.KeyAssocie = _KeyAssocie;
            MesGrappins.Add(this);
        }

        public GrappinSysChild(GameObject _Grappin, string _Name)
        {
            this.Line = _Grappin.GetComponent<LineRenderer>();
            this.Joint = _Grappin.GetComponent<DistanceJoint2D>();
            this.RB = _Grappin.GetComponent<Rigidbody2D>();
            this.Joint.autoConfigureDistance = false;
            this.Joint.enableCollision = true;
            this.Origine = _Grappin;
            this.Name = _Name;
        }

        public IEnumerator Grapple(bool _Condition)
        {
            float t = 0;
            float time = 10;

            this.Line.SetPosition(0, this.PosGrappinStart);
            this.Line.SetPosition(1, this.PosGrappinStart);

            Vector2 newPos;

            for (; t < time; t += GrappinSysChild.grappleShootSpeed * Time.deltaTime)
            {
                if (_Condition)
                {
                    newPos = Vector2.Lerp(this.PosGrappinStart, this.target, t / time);

                    this.Line.SetPosition(0, this.PosGrappinStart);
                    this.Line.SetPosition(1, newPos);
                    this.PosGrappinFin = newPos;
                }
                yield return null;
            }

            if (_Condition)
            {
                Line.SetPosition(1, this.PosGrappinFin);
                this.Joint.autoConfigureDistance = true;
                this.Joint.enabled = true;
                this.Joint.autoConfigureDistance = false;
            }
        }

        public IEnumerator UnGrapple()
        {
            float t = 0;
            float time = 10;

            this.Line.SetPosition(0, this.PosGrappinStart);
            this.Line.SetPosition(1, this.PosGrappinFin);

            Vector2 newPos = Vector2.zero;

            for (; t < time; t += GrappinSysChild.grappleShootSpeed * 3 * Time.deltaTime)
            {
                newPos = Vector2.Lerp(this.PosGrappinFin, this.PosGrappinStart, t / time);

                this.Line.SetPosition(0, this.PosGrappinStart);
                this.Line.SetPosition(1, newPos);
                yield return null;
            }
            this.PosGrappinFin = newPos;
            this.Line.SetPosition(1, this.PosGrappinStart);
        }

    };

    private GrappinSysChild GrappinSys1;
    private GrappinSysChild GrappinSys2;
    private GrappinSysChild GrappinSysTest;
    public float Ralentissement;

    private enum Clic {MGauche, MDroit, Espace, DGauche, DDroit };
    // Mouse Gauche , Mouse Droit, Espace, Deplacement Gauche, Deplacement Droit

    private Dictionary<Clic, bool> PressKey = new Dictionary<Clic, bool>() 
    { 
        { Clic.MGauche, false }, 
        { Clic.MDroit, false }, 
        { Clic.Espace, false }, 
        { Clic.DGauche, false }, 
        { Clic.DDroit, false }
    };


    public LayerMask GrappleMask;
    public GameObject Grappin1;
    public GameObject Grappin2;
    public GameObject GrappinTest;
    public Rigidbody2D Player;

    // Start is called before the first frame update
    void Start()
    {
        if (InitGrappin())
            Debug.Log("Grappin on");
        else
            Debug.Log("Grappin off");
    }

    private bool InitGrappin()
    {
        try
        {
            this.GrappinSys1 = new GrappinSysChild(this.Grappin1, "Grappin1", Clic.MGauche);
            this.GrappinSys2 = new GrappinSysChild(this.Grappin2, "Grappin2", Clic.MDroit);
            this.GrappinSysTest = new GrappinSysChild(this.GrappinTest, "GrappinTest");

            return true;
        }
        catch
        {
            return false;
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        GrappinSystemUpdate();
    }

    private void Balancement(ref GrappinSysChild _Grappin)
    {
        if (_Grappin.isGrappling)
        {
            _Grappin.RB.AddForce(BiblioGenerale.GetVelociteGrappin(_Grappin.RB.velocity, this.Player.velocity));
            PositionUpdateAll(ref _Grappin);

           // Debug.Log(_Grappin.RB.velocity);
        }
        else
        {
            PositionUpdateAll();
        }
        
    }

    private void PositionUpdateAll()
    {
        if (GrappinSysChild.SomeOneGrappling == false)
        {
            this.GrappinSys1.Origine.transform.position = this.transform.position;
            this.GrappinSys2.Origine.transform.position = this.transform.position;

            this.GrappinSys2.RB.velocity = this.Player.velocity;
            this.GrappinSys2.RB.velocity = this.Player.velocity;
        }

    }

    private void PositionUpdateAll(ref GrappinSysChild _Grappin)
    {
        this.transform.position = _Grappin.Origine.transform.position;
        this.Player.velocity = _Grappin.RB.velocity;

        _Grappin.GetPasCeluila().Origine.transform.position = _Grappin.Origine.transform.position;
        _Grappin.GetPasCeluila().RB.velocity = _Grappin.RB.velocity;
    }

    private void GrappinSystemUpdate()
    {
        GrappinSysChild.GrappleMask = this.GrappleMask;
        BiblioGenerale.Ralentissement = this.Ralentissement;


        //Balancement(ref this.GrappinSysTest);
        Balancement(ref this.GrappinSys1);
        Balancement(ref this.GrappinSys2);



        PressKeyUpdate();

        Flip();

        PosGrappinUpdate();

        GrappinUpdate();

        RetractingUpdate();

    }

    private void PressKeyUpdate()
    {
        this.PressKey[Clic.MGauche] = Input.GetMouseButton(0); 
        this.PressKey[Clic.MDroit] = Input.GetMouseButton(1);
        this.PressKey[Clic.Espace] = Input.GetKey(KeyCode.Space);

        switch (Input.GetAxisRaw("Horizontal"))
        {

            case (1):
                this.PressKey[Clic.DDroit] = true;
                this.PressKey[Clic.DGauche] = false;
                break;


            case (-1):
                this.PressKey[Clic.DDroit] = false;
                this.PressKey[Clic.DGauche] = true;
                break;


            // 2 cas possible, aucune touche n'est presser ou à l'inverse les deux le sont
            case (0):
                // Donc on verifie une seul touche pour savoir
                if (Input.GetKey(KeyCode.D))
                {
                    this.PressKey[Clic.DDroit] = true;
                    this.PressKey[Clic.DGauche] = true;
                }
                else
                {
                    this.PressKey[Clic.DDroit] = false;
                    this.PressKey[Clic.DGauche] = false;
                }
                break;
        }
    }


    private void GrappinUpdate()
    {
        this.GrappinChildUpdate(ref this.GrappinSys1);
        this.GrappinChildUpdate(ref this.GrappinSys2);
    }

    private void GrappinChildUpdate(ref GrappinSysChild _Grappin)
    {
        if(this.PressKey[_Grappin.KeyAssocie] && _Grappin.isGrappling == false)
        {
            StartGrapple(ref _Grappin);
        }

        if (!this.PressKey[_Grappin.KeyAssocie] && _Grappin.isGrappling)
        {
            EndGrapple(ref _Grappin);
        }

        if (this.PressKey[Clic.Espace] && _Grappin.isGrappling)
        {
           // Debug.Log("Pret");
            _Grappin.retracting = true;
        }

        if (this.PressKey[Clic.Espace] == false && _Grappin.retracting)
        {
            _Grappin.retracting = false;
        }
    }


    private void PosGrappinUpdate()
    {
        this.PosGrappinChildUpdate(ref this.GrappinSys1);
        this.PosGrappinChildUpdate(ref this.GrappinSys2);
    }
    
    private void PosGrappinChildUpdate(ref GrappinSysChild _Grappin)
    {
        _Grappin.PosGrappinStart = new Vector3(transform.position.x + GrappinSysChild.GrappinX, transform.position.y + GrappinSysChild.GrappinY, -1);

        if (_Grappin.isGrappling)
        {
            _Grappin.Line.SetPosition(0, _Grappin.PosGrappinStart);
        }

        _Grappin.Joint.connectedAnchor = _Grappin.PosGrappinFin;
        _Grappin.Joint.anchor = new Vector2(GrappinSysChild.JointX, GrappinSysChild.JointY);
    }


    private void RetractingUpdate()
    {
        this.RetractingChildUpdate(ref this.GrappinSys1);
        this.RetractingChildUpdate(ref this.GrappinSys2);
    }

    private void RetractingChildUpdate(ref GrappinSysChild _Grappin)
    {
        if (_Grappin.retracting)
        {

            
            //Debug.Log("Retracting");
            /*
            Vector2 grapplePos = Vector2.Lerp(transform.position, _Grappin.target, GrappinSysChild.grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            _Grappin.Line.SetPosition(0, _Grappin.PosGrappinStart);

            if (Vector2.Distance(transform.position, _Grappin.target) < 1f)
            {
                _Grappin.retracting = false;
                _Grappin.isGrappling = false;
                _Grappin.Line.enabled = false;
            }
            */
            _Grappin.Joint.distance -= 0.03f;
        }


        if (!this.PressKey[_Grappin.KeyAssocie] && _Grappin.isGrappling)
        {
            if (Vector2.Distance(_Grappin.PosGrappinStart, _Grappin.PosGrappinFin) < 1f)
            {
                _Grappin.retracting = false;
                _Grappin.isGrappling = false;
                _Grappin.Line.enabled = false;
            }
        }
    }


    private void Flip()
    {
        if (this.PressKey[Clic.DGauche])
        {
            GrappinSysChild.GrappinX = Mathf.Abs(GrappinSysChild.GrappinX);
            GrappinSysChild.JointX = Mathf.Abs(GrappinSysChild.JointX);
        }
        else
        {
            GrappinSysChild.GrappinX = Mathf.Abs(GrappinSysChild.GrappinX) * -1;
            GrappinSysChild.JointX = Mathf.Abs(GrappinSysChild.JointX) * -1;
        }

    }

    private void StartGrapple(ref GrappinSysChild _Grappin)
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, GrappinSysChild.maxdistance, GrappinSysChild.GrappleMask);

        if (hit.collider != null)
        {
            _Grappin.isGrappling = true;
            _Grappin.target = hit.point;
            _Grappin.Line.enabled = true;
            _Grappin.Line.positionCount = 2;

            StartCoroutine(_Grappin.Grapple(this.PressKey[_Grappin.KeyAssocie]));
        }
    }

    private void EndGrapple(ref GrappinSysChild _Grappin)
    {
        _Grappin.Joint.enabled = false;
        StartCoroutine(_Grappin.UnGrapple());
    }

    

    

    

}
