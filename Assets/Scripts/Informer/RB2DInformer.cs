using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB2DInformer : Informer
{
    private Rigidbody2D rb2d;
    public override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
    }
    public override void SetActivation(bool status)
    {
        base.SetActivation(status);
        rb2d.simulated = status;
    }
}
