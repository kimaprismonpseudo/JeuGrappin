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
    private float hauteurdebase;


    [SerializeField]
    private Renderer bgRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0, 0.5f);
        hauteurdebase = rb.transform.position.y;
        Debug.Log(hauteurdebase);
        Debug.Log(rb.transform.position.y);
        Debug.Log(rb.transform.position.y - hauteurdebase);
    }

    // Update is called once per frame
    void Update()
    {
        if (MouvementPersonnage.isClimb == false)
        {
            bgRenderer.material.mainTextureOffset = new Vector2(bgRenderer.material.mainTextureOffset.x + rb.velocity.x * speed * Time.deltaTime, (rb.transform.position.y - hauteurdebase) * speed2);

            Debug.Log($"T : {bgRenderer.material.mainTextureOffset.y}");
        } 
        else
        {
            bgRenderer.material.mainTextureOffset += Vector2.zero;
            Debug.Log("climb");
        }
    }
}
