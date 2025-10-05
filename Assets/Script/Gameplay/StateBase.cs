using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE_TYPE
{
    NONE,
    INITIAL_REVEIL,
    STALE,
    OPEN,
    CLOSE,
    MATCHED
}

enum FLIP_STATE
{
    FLIP_1,
    FLIP_2,
    FLIP_FINISH,
    FINISH_WAIT,
    CLOSE,
    MATCHED,
    WAIT
}


public class StateBase
{
    public virtual void Reset()
    { 
    }

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