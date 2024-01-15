using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouvementPerosnnage : MonoBehaviour
{

    private Vector3 Velocity = Vector3.zero;
    
    public float moveSpeed;

    public Rigidbody2D rb;

    public bool DebugC = true;
    public float Hor = -1000000;

    public float dTime = 0.007f;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bonjour");
    }

    // Update is called once per frame
    void Update()
    {
        MAJPositionPlayer();
    }


    // Fonction Jimmy
    private void MAJPositionPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            this.DebugC = !this.DebugC;



        float horizontalMovement = Input.GetAxis("Horizontal") * this.moveSpeed * Time.deltaTime;


        if (this.Hor == -1000000)
            this.Hor = horizontalMovement;

        if (Math.Abs(this.Hor - horizontalMovement) > 20)
        {
            this.DebugC = false;
            Debug.Log(string.Format("Axis : {0} , DeltaTime : {1} , horizontalMovement : {2}", Input.GetAxis("Horizontal"), Time.deltaTime, horizontalMovement));
            Debug.Log($"Hor : {this.Hor} , horizontalMovement : {horizontalMovement}");
        }


        if (Math.Abs(this.dTime - Time.deltaTime) > 0.007f)
        {
            horizontalMovement = Input.GetAxis("Horizontal") * this.moveSpeed * this.dTime;
            Debug.Log(string.Format("Axis : {0} , DeltaTime : {1} - this.dTime, horizontalMovement : {2}", Input.GetAxis("Horizontal"), Time.deltaTime, horizontalMovement));
            Debug.Log($"Hor : {this.Hor} , horizontalMovement : {horizontalMovement}");

        }

        

        this.Hor = horizontalMovement;

        Vector3 TargetVelocity = new Vector2(horizontalMovement, this.rb.velocity.y);

        if (this.DebugC)
            Debug.Log(string.Format("Axis : {0} , DeltaTime : {1} , horizontalMovement : {2}", Input.GetAxis("Horizontal"), Time.deltaTime, horizontalMovement));

        this.rb.velocity = Vector3.SmoothDamp(this.rb.velocity, TargetVelocity, ref this.Velocity, .05f);
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
