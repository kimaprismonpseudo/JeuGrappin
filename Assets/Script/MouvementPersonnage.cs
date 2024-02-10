using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouvementPersonnage : MonoBehaviour
{

    // Public 
    public float moveSpeed = 3;
    public Rigidbody2D rb;
    public SpriteRenderer Graphics;
    public static bool isGrapplingPlayer = false;
    public Animator Animator;

    // Private 
    private Vector3 Velocity = Vector3.zero;
    private bool DebugC = false;
    private bool isFlip;

    public GrappinSystem Grappin;

    public Transform GroundCheckL;
    public Transform GroundCheckR;

    public static bool IsGrounded = true;
    private static bool isRespawn = false;
    public static bool isClimb;

    private bool SafePosSave = false;
    private Vector2 PosBeforeDeath;


    // Start is called before the first frame update
    void Start()
    {
        //this.isFlip = false;
        Debug.Log("MouvementPersonnage - Debut");

        this.rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
      //  MAJDebugConsole();
        MAJPositionPlayer();
        MAJFlipPlayer();
       
        IsGrounded = Physics2D.OverlapArea(this.GroundCheckL.position, this.GroundCheckR.position);

        if (IsGrounded == false && this.SafePosSave == false && isRespawn == false)
        {
            //Debug.Log("Nouvelle Co");
            this.SafePosSave = true;
            this.PosBeforeDeath = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal")*-2, transform.position.y + 2);
        }
        else if (IsGrounded == true && isRespawn == false)
            this.SafePosSave = false;

        this.Animator.SetFloat("Speed", this.rb.velocity.x < 0 ? this.rb.velocity.x * -1 : this.rb.velocity.x);
        //Debug.Log(this.rb.velocity.x < 0 ? this.rb.velocity.x * -1 : this.rb.velocity.x);


        if (isGrapplingPlayer)
            moveSpeed = 10;
        else
            moveSpeed = 3;
    }



    //public GameObject ObjectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "DeathZone":
                isRespawn = true;
                StartCoroutine(GoToSafePos());
                break;

            case "Climbable":
                isClimb = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Climbable":
                isClimb = false;
                break;
        }
    }



    private IEnumerator GoToSafePos()
    {
        StartCoroutine(InvincibilityFlash());
        GrappinSystem.GrappinSysChild.SetOFFGrapples();
        float t = 0;
        float time = 10;

        Vector2 newPos;

        Collider2D[] RB_Collider = new Collider2D[1];
        this.rb.GetAttachedColliders(RB_Collider);
        RB_Collider[0].enabled = false;

        for (; t < time && Vector2.Distance(transform.position,this.PosBeforeDeath) > .5f; t += Time.deltaTime)
        {
            GrappinSystem.GrappinSysChild.SetOFFGrapples();
            newPos = Vector2.Lerp(transform.position, this.PosBeforeDeath, t / time);

            transform.position = newPos;
            yield return null;
        }

        RB_Collider[0].enabled = true;
        this.SafePosSave = false;
        isRespawn = false;
        this.rb.velocity = Vector2.zero;
        GrappinSystem.GrappinSysChild.SetOFFGrapples();
    }


    public IEnumerator InvincibilityFlash()
    {
        for (; isRespawn;)
        {
            this.Graphics.color = new Color(1f, 1f, 1f, 0.3f);

            yield return new WaitForSeconds(.15f);

            this.Graphics.color = new Color(1f, 1f, 1f, 1f);

            yield return new WaitForSeconds(.30f);
        }
    }


    // Fonction Jimmy
    private void MAJPositionPlayer()
    {
        float Axis = Input.GetAxis("Horizontal");
        float horizontalMovement = Axis * this.moveSpeed;

        if (Axis > 0)
            this.isFlip = GrappinSystem.GrappinSysChild.isSuperGrapple;
        else if (Axis < 0)
            this.isFlip = !GrappinSystem.GrappinSysChild.isSuperGrapple;


        Vector2 TargetVelocity = new Vector2(horizontalMovement, this.rb.velocity.y);

        if ((IsGrounded || isGrapplingPlayer || isClimb) && GrappinSystem.GrappinSysChild.isSuperGrapple == false)
        {
            this.rb.velocity = TargetVelocity;
        }

        if (this.DebugC)
            Debug.Log(string.Format("Axis : {0} , DeltaTime : {1} , horizontalMovement : {2}", Input.GetAxis("Horizontal"), Time.deltaTime, horizontalMovement));

    }

    private void MAJDebugConsole()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            this.DebugC = !this.DebugC;
    }


    private void MAJFlipPlayer()
    {
        this.Graphics.flipX = this.isFlip;
    }






    private void QuelTouche()
    {
        foreach (KeyCode k in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(k))
            {
                Debug.Log("Key : " + k);
            }
        }
    }

}
