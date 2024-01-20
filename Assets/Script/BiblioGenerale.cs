using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiblioGenerale
{
    public static float Ralentissement;
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
}
