using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Informer : MonoBehaviour
{
    [HideInInspector] public Sublevel myLevel;

    public virtual void Awake()
    {
        myLevel = GetComponentInParent<Sublevel>();
        myLevel.AddInformer(this);
    }

    public virtual void Start()
    {
    }

    public virtual void SetActivation(bool status)
    {
    }

    public virtual void OnDestroy()
    {
        myLevel.RemoveInformer(this);
    }
}