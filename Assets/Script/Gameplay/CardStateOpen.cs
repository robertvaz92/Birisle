using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardStateOpen : StateBase
{
    CardUpdater m_updater;
    Card m_card;
    AudioManager m_audioManager;

    float m_flipDuration;
    float m_autoCloseDuration;
    float m_finishWaitDuration;
    FLIP_STATE m_flipState;
    bool m_isFinishedFlipping;

    public CardStateOpen(CardUpdater updater)
    {
        m_updater = updater;
        m_updater = updater;
        m_card = m_updater.m_card;

        m_audioManager = GameManager.Instance.m_audioManager;
    }

    public override void Reset()
    {
        m_isFinishedFlipping = true;
        m_flipState = FLIP_STATE.FLIP_FINISH;
    }

    public override void OnEnter()
    {
        m_flipState = FLIP_STATE.FLIP_1;
        m_isFinishedFlipping = false;
        m_audioManager.PlaySFX(m_audioManager.m_data.m_cardFlipSfx);
        
        m_flipDuration = m_card.m_manager.m_cardData.m_flipDuration;
        m_autoCloseDuration = m_card.m_manager.m_cardData.m_autoCloseDuration;
        m_finishWaitDuration = 1f;

        if (m_card.m_compareCard == null)
        {
            m_card.m_manager.OnCardSelect -= OnCardSelect;
            m_card.m_manager.OnCardSelect += OnCardSelect;
        }
    }

    public override void OnExit()
    {
        m_card.m_manager.OnCardSelect -= OnCardSelect;
    }

    private void OnCardSelect(Card c)
    {
        if (m_card.m_compareCard == null)
        {
            m_card.m_manager.OnCardSelect -= OnCardSelect;

            m_card.SetCompareCard(c);
            c.SetCompareCard(m_card);

            if (c.m_id == m_card.m_id)
            {
                m_card.SetMatchedFlag();
                c.SetMatchedFlag();
                m_card.m_manager.OnMachSuccess(m_card, c);
            }
            else
            {
                m_card.m_manager.OnMatchFailed(m_card, c);
            }
        }
    }

    public override void CustomUpdate()
    {
        switch (m_flipState)
        {
            case FLIP_STATE.FLIP_1:
                m_card.m_holder.DOScaleX(0, m_flipDuration).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    m_flipState = FLIP_STATE.FLIP_2;
                    m_card.m_icon.sprite = m_card.m_iconSprite;
                });

                m_flipState = FLIP_STATE.WAIT;
                break;

            case FLIP_STATE.FLIP_2:
                m_card.m_holder.DOScaleX(1, m_flipDuration).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    m_flipState = FLIP_STATE.FLIP_FINISH;
                    m_isFinishedFlipping = true;
                });
                m_flipState = FLIP_STATE.WAIT;
                break;

            case FLIP_STATE.FLIP_FINISH:
                if (m_card.m_compareCard != null && m_card.m_compareCard.m_updater.IsFlippingFinished())
                {
                    m_flipState = FLIP_STATE.FINISH_WAIT;
                }
                break;

            case FLIP_STATE.FINISH_WAIT:
                m_finishWaitDuration -= Time.deltaTime;

                if (m_finishWaitDuration < 0)
                {
                    if (m_card.m_isMatched)
                    {
                        m_flipState = FLIP_STATE.MATCHED;
                    }
                    else
                    {
                        m_audioManager.PlaySFX(m_audioManager.m_data.m_mismatchSfx);
                        m_card.m_holder.DOShakePosition(0.5f,30).OnComplete(() => { m_flipState = FLIP_STATE.CLOSE; });
                        m_flipState = FLIP_STATE.WAIT;
                    }
                }

                break;

            case FLIP_STATE.MATCHED:
                m_audioManager.PlaySFX(m_audioManager.m_data.m_matchSfx);
                Sequence sequence = DOTween.Sequence();
                sequence.Append(m_card.m_holder.DOScale(1.15f, 0.25f));
                sequence.Join(m_card.m_canvasGroup.DOFade(0, 0.2f));

                sequence.Play().OnComplete(() =>
                {
                    m_updater.SwitchState(STATE_TYPE.MATCHED);
                    m_flipState = FLIP_STATE.CLOSE;
                });

                m_flipState = FLIP_STATE.WAIT;
                break;

            case FLIP_STATE.CLOSE:
                m_autoCloseDuration -= Time.deltaTime;
                if (m_autoCloseDuration < 0)
                {
                    m_card.SetCompareCard(null);
                    m_updater.SwitchState(STATE_TYPE.CLOSE);
                }
                break;
        }
    }

    public bool IsFinishedFlipping()
    {
        return m_isFinishedFlipping;
    }

    public override void OnCardClick()
    {
        if (IsFinishedFlipping())
        {
            if (m_card.m_compareCard != null)
            {
                //m_card.m_compareCard.SwitchToCloseState();
                //m_card.SetCompareCard(null);
            }
            else
            {
                m_updater.SwitchState(STATE_TYPE.CLOSE);
                m_card.m_manager.OnMatchFailed(m_card, null);
            }
        }
    }
}