using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinSystem : MonoBehaviour
{

    public LineRenderer Line;

    [SerializeField] LayerMask GrappleMask;

    public float maxdistance = 10;
    public float grappleSpeed = 10;
    public float grappleShootSpeed = 20;

    public bool isGrappling = false;
    public bool retracting = false;

    Vector2 target;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Grappin on");
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrappling) 
            Line.SetPosition(0, transform.position);


        if (Input.GetMouseButtonDown(0) && this.isGrappling == false)
        {
            StartGrapple();
        }

        if (this.retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, this.target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            Line.SetPosition(0, transform.position);

            if (Vector2.Distance(transform.position, this.target) < 1f)
            {
                this.retracting = false;
                this.isGrappling = false;
                Line.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.retracting = true;
        }
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


    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, transform.position);

        Vector2 newPos;

        for (; t < time; t += this.grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, this.target, t / time);

            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, newPos);
            yield return null;
        }

        Line.SetPosition(1, target);
    }
}
