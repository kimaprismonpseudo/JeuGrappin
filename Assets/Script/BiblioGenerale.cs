using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiblioGenerale : MonoBehaviour
{
    [SerializeField] public static float Ralentissement;

    public float ets;
    public static Vector2 GetVelociteGrappin(Vector2 _VelociteGrappin, Vector2 _VelocitePlayer)
    {
        float x;
        float y = _VelociteGrappin.y * Ralentissement;

        if (Mathf.Max(Mathf.Abs(_VelociteGrappin.x), Mathf.Abs(_VelocitePlayer.x)) == Mathf.Abs(_VelociteGrappin.x))
        {
            x = _VelociteGrappin.x * Ralentissement;
        }
        else
        {
            x = _VelocitePlayer.x * Ralentissement * -1;
        }

        return new Vector2(x, y);
    }


    //public void Start()
    //{
    //    StartCoroutine(Test());
    //}


    //public IEnumerator Test()
    //{
    //    Debug.Log("coucou");

    //    yield return new WaitForSeconds(5);

    //    Debug.Log("Test2");

    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.V))
    //        StartCoroutine(Test());
    //}
}
