using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinSystem : MonoBehaviour
{

    public LineRenderer Line;
    public DistanceJoint2D Joint;

    [SerializeField] LayerMask GrappleMask;

    public float maxdistance = 10;
    public float grappleSpeed = 3;
    public float grappleShootSpeed = 60;
    private float GrappinX = 0.2f;
    private float GrappinY = 0.4f;
    private float JointX = -0.3f;
    private float JointY = 0.7f;
    public bool isGrappling = false;
    public bool retracting = false;

    private enum Clic {Gauche, Droit };

    private Dictionary<Clic, bool> ClicMouse = new Dictionary<Clic, bool>(2) { { Clic.Gauche, false }, { Clic.Droit, false } };

    private Vector3 PosGrappinStart;

    private Vector2 target;
    private Vector2 PosGrappinFin;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Grappin on");
        this.Joint.enableCollision = true;
    }

    // Update is called once per frame
    void Update()
    {
        GrappinSystemUpdate();
    }

    private void GrappinSystemUpdate()
    {
        KeyUpdate();

        VariableUpdate();        
    }


    private void KeyUpdate()
    {
        if (this.ClicMouse[Clic.Gauche] == false)
        {
            this.ClicMouse[Clic.Gauche] = Input.GetMouseButtonDown(0);
        }
        else
        {
            this.ClicMouse[Clic.Gauche] = !Input.GetMouseButtonUp(0);
        }


        if (this.ClicMouse[Clic.Gauche] && this.isGrappling == false)
        {
            StartGrapple();
            Debug.Log("StartGrapple");
        }

        if (!this.ClicMouse[Clic.Gauche] && this.isGrappling)
        {
            EndGrapple();
        }

        if (Input.GetKeyDown(KeyCode.Space) && this.isGrappling)
        {
            this.retracting = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && this.retracting)
        {
            this.retracting = false;
        }

    }

    private void PosGrappinUpdate()
    {
        this.PosGrappinStart = new Vector3(transform.position.x + this.GrappinX, transform.position.y + this.GrappinY, -1);

        if (isGrappling)
        {
            Line.SetPosition(0, this.PosGrappinStart);
        }

        this.Joint.connectedAnchor = this.PosGrappinFin;
        this.Joint.anchor = new Vector2(this.JointX, this.JointY);
    }


    private void retractingUpdate()
    {
        if (this.retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, this.target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            Line.SetPosition(0, this.PosGrappinStart);

            if (Vector2.Distance(transform.position, this.target) < 1f)
            {
                this.retracting = false;
                this.isGrappling = false;
                Line.enabled = false;
            }
        }


        if (!this.ClicMouse[Clic.Gauche] && this.isGrappling)
        {
            if (Vector2.Distance(this.PosGrappinStart, this.PosGrappinFin) < 1f)
            {
                this.retracting = false;
                this.isGrappling = false;
                Line.enabled = false;
            }
        }
    }


    private void VariableUpdate()
    {
        this.Flip();

        this.PosGrappinUpdate();

        this.retractingUpdate();

        

        
    }


    private void StartGrapple()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, this.maxdistance, this.GrappleMask);

        if (hit.collider != null)
        {
            this.isGrappling = true;
            target = hit.point;
            Line.enabled = true;
            Line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    private void EndGrapple()
    {
        this.Joint.enabled = false;
        StartCoroutine(UnGrapple());
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        Line.SetPosition(0, this.PosGrappinStart);
        Line.SetPosition(1, this.PosGrappinStart);

        Vector2 newPos;

        for (; t < time; t += this.grappleShootSpeed * Time.deltaTime)
        {
            if (this.ClicMouse[Clic.Gauche])
            {
                newPos = Vector2.Lerp(this.PosGrappinStart, this.target, t / time);

                Line.SetPosition(0, this.PosGrappinStart);
                Line.SetPosition(1, newPos);
                this.PosGrappinFin = newPos;
            }
            yield return null;   
        }

        if (this.ClicMouse[Clic.Gauche])
        {
            Line.SetPosition(1, this.PosGrappinFin);
            this.Joint.enabled = true;
        }
    }

    IEnumerator UnGrapple()
    {
        float t = 0;
        float time = 10;

        Line.SetPosition(0, this.PosGrappinStart);
        Line.SetPosition(1, this.PosGrappinFin);

        Vector2 newPos = Vector2.zero;

        for (; t < time; t += this.grappleShootSpeed * 3 * Time.deltaTime)
        {
            newPos = Vector2.Lerp(this.PosGrappinFin, this.PosGrappinStart, t / time);

            Line.SetPosition(0, this.PosGrappinStart);
            Line.SetPosition(1, newPos);
            yield return null;
        }
        this.PosGrappinFin = newPos;
        Line.SetPosition(1, this.PosGrappinStart);
    }

    private void Flip()
    {
        float Axis = Input.GetAxis("Horizontal");
        if (Axis < 0)
        {
            this.GrappinX = Mathf.Abs(this.GrappinX);
            this.JointX = Mathf.Abs(this.JointX);
        }
        else
        {
            this.GrappinX = Mathf.Abs(this.GrappinX) * -1;
            this.JointX = Mathf.Abs(this.JointX) * -1;
        }
    }
}
