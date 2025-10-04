using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUpdater
{
    public Card m_card;

    private STATE_TYPE m_currentStateType;
    private StateBase m_stateStale;
    private StateBase m_stateOpen;
    private StateBase m_stateClose;

    private StateBase m_currentState;

    public CardUpdater(Card c)
    {
        m_card = c;
        Initialize();
    }

    public void Initialize()
    {
        m_currentStateType = STATE_TYPE.NONE;
        m_stateStale = new CardStateStale(this);
        m_stateOpen = new CardStateOpen(this);
        m_stateClose = new CardStateClose(this);
    }

    public void CustomUpdate()
    {
        m_currentState.CustomUpdate();
    }

    public void OnCardClick()
    {
        m_currentState.OnCardClick();
    }

    public void SwitchState(STATE_TYPE stateType)
    {
        if (m_currentStateType != stateType)
        {
            if (m_currentState != null)
            {
                m_currentState.OnExit();
            }

            m_currentStateType = stateType;
            switch (stateType)
            {
                case STATE_TYPE.STALE:
                    m_currentState = m_stateStale;
                    break;
                case STATE_TYPE.OPEN:
                    m_currentState = m_stateOpen;
                    break;
                case STATE_TYPE.CLOSE:
                    m_currentState = m_stateClose;
                    break;
            }
            m_currentState.OnEnter();
        }
    }
}