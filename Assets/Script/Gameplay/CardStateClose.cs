using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardStateClose : StateBase
{
    CardUpdater m_updater;
    Card m_card;
    AudioManager m_audioManager;

    float m_flipDuration;
    FLIP_STATE m_flipState;

    public CardStateClose(CardUpdater updater)
    {
        m_updater = updater;
        m_card = m_updater.m_card;
        m_flipDuration = m_card.m_manager.m_cardData.m_flipDuration;

        m_audioManager = GameManager.Instance.m_audioManager;
    }

    public override void Reset()
    {
        m_flipState = FLIP_STATE.FLIP_FINISH;
    }

    public override void OnEnter()
    {
        m_flipState = FLIP_STATE.FLIP_1;
        m_audioManager.PlaySFX(m_audioManager.m_data.m_cardFlipSfx);
    }

    public override void CustomUpdate()
    {
        switch (m_flipState)
        {
            case FLIP_STATE.FLIP_1:
                m_card.m_holder.DOScaleX(0, m_flipDuration).SetEase(Ease.InOutSine).OnComplete(() => {
                    m_flipState = FLIP_STATE.FLIP_2;
                    m_card.m_icon.sprite = m_card.m_bgSprite;
                });

                m_flipState = FLIP_STATE.WAIT;
                break;

            case FLIP_STATE.FLIP_2:
                m_card.m_holder.DOScaleX(1, m_flipDuration).SetEase(Ease.InOutSine).OnComplete(() => {
                    m_flipState = FLIP_STATE.FLIP_FINISH;
                });
                m_flipState = FLIP_STATE.WAIT;
                break;
        }
    }

    public override void OnCardClick()
    {
        if (m_flipState == FLIP_STATE.FLIP_FINISH)
        {
            m_updater.m_card.m_manager.OnCardSelect?.Invoke(m_card);
            m_updater.SwitchState(STATE_TYPE.OPEN);
        }
    }
}
