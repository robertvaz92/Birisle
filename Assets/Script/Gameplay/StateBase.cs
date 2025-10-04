using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE_TYPE
{
    NONE,
    STALE,
    OPEN,
    CLOSE
}


public class StateBase
{
    public virtual void OnEnter()
    {
    }

    public virtual void CustomUpdate()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void OnCardClick()
    { 
    }
}