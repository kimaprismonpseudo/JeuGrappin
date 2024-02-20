using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    public float speed;
    public float speed2; 
    public Rigidbody2D rb;
    public float hauteurMax;
    public float velocityMax;


    [SerializeField]
    private Renderer bgRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MouvementPersonnage.isClimb == false)
        {
            //velocityMax = rb.velocity.y > 0 ? Mathf.Max(velocityMax,rb.velocity.y) : velocityMax;

            //bgRenderer.material.mainTextureOffset += new Vector2(rb.velocity.x * speed * Time.deltaTime, (rb.velocity.y > 0 ? rb.velocity.y : rb.velocity.y/2) * speed2 * Time.deltaTime);
            
            bgRenderer.material.mainTextureOffset += new Vector2(0, hauteurMax);

            Debug.Log(bgRenderer.material.mainTextureOffset.y);
        } 
        else
        {
            bgRenderer.material.mainTextureOffset += Vector2.zero;
            Debug.Log("climb");
        }
    }
}
