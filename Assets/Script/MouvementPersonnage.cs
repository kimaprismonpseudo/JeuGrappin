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


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bonjour");
    }

    // Update is called once per frame
    void Update()
    {

        MAJDebugConsole();
        MAJPositionPlayer();
    }


    // Fonction Jimmy
    private void MAJPositionPlayer()
    {
        

        float horizontalMovement = Input.GetAxis("Horizontal") * this.moveSpeed;      

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
