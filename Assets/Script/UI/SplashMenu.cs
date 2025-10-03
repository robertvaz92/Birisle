using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMenu : UIMenuBase
{
    public CanvasGroup m_splashObject;
    public float m_speed = 1;

    private float m_alpha = 0;
    private bool m_canUpdate = false;

    public override void OnEnter()
    {
        base.OnEnter();
        m_alpha = 0;
        m_canUpdate = true;
        m_splashObject.alpha = m_alpha;
    }

    private void Update()
    {
        if (m_canUpdate)
        {
            m_alpha += Time.deltaTime * m_speed;
            m_splashObject.alpha = Mathf.Lerp(0, 1, m_alpha);

            if (m_alpha > 2)
            {
                m_canUpdate = false;
                GameManager.Instance.SwitchMenu(MenuScreenTypes.MAIN_MENU_SCREEN);
            }
        }
    }
}
