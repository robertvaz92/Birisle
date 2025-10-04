using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateStale : StateBase
{
    CardUpdater m_updater;
    public CardStateStale(CardUpdater updater)
    {
        m_updater = updater;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        m_updater.m_card.m_canvasGroup.alpha = 0;
    }
}
