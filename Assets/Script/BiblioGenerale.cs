using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BiblioGenerale
{
    [SerializeField] public static float Ralentissement;
    public static bool WaitRelachement;

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


    public static bool GetInput(bool _value, int _Input)
    {
        if (WaitRelachement)
            WaitRelachement = !Input.GetMouseButtonUp(_Input);
        else
            WaitRelachement = GrappinSystem.GrappinSysChild.isSuperGrapple;

        if (!WaitRelachement)
            _value = ((Input.GetMouseButtonDown(_Input) || _value) && Input.GetMouseButton(_Input)) || (MouvementPersonnage.IsGrounded && Input.GetMouseButton(_Input) && !GrappinSystem.GrappinSysChild.isSuperGrapple);

        return _value;
    }

}
