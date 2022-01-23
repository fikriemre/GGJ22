using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider2DInformer : Informer
{
    private Collider2D collider;
    public override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider2D>();
    }

    public override void SetActivation(bool status)
    {
        base.SetActivation(status);
        collider.enabled = status;
    }
}
