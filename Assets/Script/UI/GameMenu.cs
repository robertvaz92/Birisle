using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : UIMenuBase
{
    public CardGridManager m_cardManager;
    public override void OnEnter()
    {
        base.OnEnter();
        m_cardManager.Initialize();
    }

    public override void OnExit()
    {
        base.OnExit();
        m_cardManager.OnExit();
    }

    public void OnClickClose()
    {
        GameManager.Instance.SwitchMenu(MenuScreenTypes.MAIN_MENU_SCREEN);
    }
}
