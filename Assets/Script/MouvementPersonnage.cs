using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouvementPerosnnage : MonoBehaviour
{

    // Public 
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer FlipCheck;


    // Private 
    private Vector3 Velocity = Vector3.zero;
    private bool DebugC = false;
    private bool isFlip;
    


    // Start is called before the first frame update
    void Start()
    {
        //this.isFlip = false;
        Debug.Log("MouvementPerosnnage - Debut");
    }

    // Update is called once per frame
    void Update()
    {
        MAJDebugConsole();
        MAJPositionPlayer();
        MAJFlipPlayer();
    }


    // Fonction Jimmy
    private void MAJPositionPlayer()
    {
        float Axis = Input.GetAxis("Horizontal");
        float horizontalMovement = Axis * this.moveSpeed;

        if (Axis >= 0)
            this.isFlip = false;
        else
            this.isFlip = true;

        Vector2 TargetVelocity = new Vector2(horizontalMovement, this.rb.velocity.y);

        this.rb.velocity = TargetVelocity;

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
        this.FlipCheck.flipX = this.isFlip;
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
