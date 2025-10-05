using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateInitialReveil : StateBase
{
    CardUpdater m_updater;
    Card m_card;
    float m_waitDuration;

    public CardStateInitialReveil(CardUpdater updater)
    {
        m_updater = updater;
        m_card = m_updater.m_card;
    }

    public override void OnEnter()
    {
        m_card.m_manager.m_menu.ShowCloseButton(false);
        m_waitDuration = m_card.m_manager.m_cardData.m_initialWaitDuration;
        m_card.m_icon.sprite = m_card.m_iconSprite;
        m_card.m_canvasGroup.alpha = 1;

        GameManager.Instance.m_audioManager.PlaySFX(GameManager.Instance.m_audioManager.m_data.m_gameCountdown);
    }

    public override void OnExit()
    {
        m_card.m_manager.m_menu.ShowCloseButton(true);
    }


    public override void CustomUpdate()
    {
        m_waitDuration -= Time.deltaTime;

        if (m_waitDuration <= 0)
        {
            m_updater.SwitchState(STATE_TYPE.CLOSE);
        }
    }
}
