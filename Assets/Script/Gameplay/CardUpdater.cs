using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardUpdater
{
    public Card m_card;

    public STATE_TYPE m_currentStateType { get; private set; }
    private StateBase m_stateStale;
    private StateBase m_stateOpen;
    private StateBase m_stateClose;
    private StateBase m_stateInitialReveil;

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
        m_stateInitialReveil = new CardStateInitialReveil(this);
    }

    public void CustomUpdate()
    {
        m_currentState.CustomUpdate();
    }

    public void OnCardClick()
    {
        m_currentState.OnCardClick();
    }

    //Called only for Initialisation
    public void ForceSwitchState(STATE_TYPE stateType)
    {
        if (m_currentState != null)
        {
            m_currentState.Reset();
        }

        m_currentStateType = stateType;
        m_currentState = GetState(m_currentStateType);
        m_currentState.Reset();
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
            m_currentState = GetState(m_currentStateType);
            m_currentState.OnEnter();
        }
    }

    private StateBase GetState(STATE_TYPE stateType)
    {
        StateBase retVal = m_stateStale;
        switch (stateType)
        {
            case STATE_TYPE.STALE:
                retVal = m_stateStale;
                break;

            case STATE_TYPE.INITIAL_REVEIL:
                retVal = m_stateInitialReveil;
                break;

            case STATE_TYPE.OPEN:
                retVal = m_stateOpen;
                break;

            case STATE_TYPE.CLOSE:
                retVal = m_stateClose;
                break;

            case STATE_TYPE.MATCHED:
                retVal = m_stateStale;
                break;
        }
        return retVal;
    }

    public bool IsFlippingFinished()
    {
        bool retVal = false;
        if (m_currentStateType == STATE_TYPE.OPEN)
        {
            CardStateOpen current = (CardStateOpen)m_currentState;
            if (current != null && current.IsFinishedFlipping())
            {
                retVal = true;
            }
        }
        return retVal;
    }
}