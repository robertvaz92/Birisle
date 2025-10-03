using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuBase : MonoBehaviour
{
    public MenuScreenTypes m_menuScreenType { get; private set; }
    public void Setup(MenuScreenTypes screen)
    {
        m_menuScreenType = screen;
        Deactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        OnEnter();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        OnExit();
    }

    public virtual void OnEnter()
    { 
    }

    public virtual void OnExit()
    { 
    }
}
